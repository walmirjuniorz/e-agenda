using Microsoft.Playwright;
using Tarefae2eTests.Compartilhado;

namespace Tarefae2eTests.Modulos.Tarefas;

[TestClass]
[DoNotParallelize]
public sealed class Tarefae2eTests : e2eTestsBase
{
    [TestMethod]
    public async Task Deve_Cadastrar_Tarefa_Corretamente()
    {
        // Arranjo
        await CriarUsuarioAsync();
        await page.GotoAsync($"{baseUrl}/Tarefa/Cadastrar");

        // Ação
        await page.GetByLabel("Título").FillAsync("Lavar o cachorro");
        await page.GetByLabel("Prioridade").SelectOptionAsync("Alta");

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();

        ILocator card = page.Locator(".card").Filter(new() { HasText = "Lavar o cachorro" });

        // Asserção
        await Assertions.Expect(card).ToBeVisibleAsync();
    }
    [TestMethod]
    public async Task Deve_Editar_Tarefa_Corretamente()
    {
        // Arranjo
        await CriarUsuarioAsync();

        await page.GotoAsync($"{baseUrl}/Tarefa/Cadastrar");

        // Ação
        await page.GetByLabel("Título").FillAsync("Lavar o cachorro");
        await page.GetByLabel("Prioridade").SelectOptionAsync("Alta");

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();

        await page.GetByRole(AriaRole.Link, new() { Name = "Editar" }).First.ClickAsync();

        await page.GetByLabel("Título").FillAsync("Limpar o cachorro");
        await page.GetByLabel("Prioridade").SelectOptionAsync("Baixa");

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();

        ILocator card = page.Locator(".card").Filter(new() { HasText = "Limpar o cachorro" }).First;

        // Asserção
        Assert.IsTrue(await card.IsVisibleAsync());

    }
    [TestMethod]
    public async Task Deve_Excluir_Tarefa_Corretamente()
    {
        // Arranjo
        await CriarUsuarioAsync();

        await page.GotoAsync($"{baseUrl}/Tarefa/Cadastrar");

        // Ação
        await page.GetByLabel("Título").FillAsync("Lavar o cachorro");
        await page.GetByLabel("Prioridade").SelectOptionAsync("Alta");

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();

        ILocator card = page.Locator(".card").Filter(new() { HasText = "Lavar o cachorro" }).First;

        await card.GetByRole(AriaRole.Link, new() { Name = "Excluir" }).ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();

        // Asserção
        Assert.IsFalse(await card.IsVisibleAsync());

    }
}
