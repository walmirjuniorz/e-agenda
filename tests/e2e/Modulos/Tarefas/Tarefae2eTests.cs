using Microsoft.Playwright;
using Tarefae2eTests.Compartilhado;

namespace Tarefae2eTests.Modulos.Tarefas;

[TestClass]
[DoNotParallelize]
public sealed class Tarefae2eTests
{
    private TestAplicationFactory aplicationFactory = null!;
    private IPlaywright playwright = null!;
    private IBrowser browser = null!;
    private IBrowserContext browserContext = null!;
    private IPage page = null!;
    private string? baseUrl = null;

    [TestInitialize]
    public async Task InicializarTeste()
    {
        aplicationFactory = new TestAplicationFactory();
        await aplicationFactory.StartAsync();

        baseUrl = aplicationFactory.BaseUrl;

        playwright = await Playwright.CreateAsync();
        browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        browserContext = await browser.NewContextAsync();
        page = await browserContext.NewPageAsync();
    }
    [TestCleanup]
    public async Task FinalizarTeste()
    {
        await browser.DisposeAsync();
        playwright.Dispose();
    }

    [TestMethod]
    public async Task Deve_Cadastrar_Tarefa_Corretamente()
    {
        // Arranjo
        await page.GotoAsync($"{baseUrl}/Autenticacao/Login");

        await page.GetByLabel("E-mail").FillAsync("juniortestes@gmail.com");
        await page.GetByLabel("Senha").FillAsync("Teste@123");
        await page.GetByRole(AriaRole.Button, new() { Name = "Entrar" }).ClickAsync();

        await page.GotoAsync($"{baseUrl}/Tarefa/Cadastrar");

        // Ação
        await page.GetByLabel("Título").FillAsync("Lavar o cachorro");
        await page.GetByLabel("Prioridade").SelectOptionAsync("Alta");

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();

        ILocator card = page.Locator(".card").Filter(new() { HasText = "Lavar o cachorro" }).First;

        // Asserção
        Assert.IsTrue(await card.IsVisibleAsync());
    }
    [TestMethod]
    public async Task Deve_Editar_Tarefa_Corretamente()
    {
        // Arranjo
        await page.GotoAsync($"{baseUrl}/Autenticacao/Login");

        await page.GetByLabel("E-mail").FillAsync("juniortestes@gmail.com");
        await page.GetByLabel("Senha").FillAsync("Teste@123");
        await page.GetByRole(AriaRole.Button, new() { Name = "Entrar" }).ClickAsync();

        await page.GotoAsync($"{baseUrl}/Tarefa/Listar");

        // Ação
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
        await page.GotoAsync($"{baseUrl}/Autenticacao/Login");

        await page.GetByLabel("E-mail").FillAsync("juniortestes@gmail.com");
        await page.GetByLabel("Senha").FillAsync("Teste@123");
        await page.GetByRole(AriaRole.Button, new() { Name = "Entrar" }).ClickAsync();

        await page.GotoAsync($"{baseUrl}/Tarefa/Listar");

        // Ação
        ILocator card = page.Locator(".card").Filter(new() { HasText = "Limpar o cachorro" }).First;

        await card.GetByRole(AriaRole.Link, new() { Name = "Excluir" }).ClickAsync();

        await page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();

        // Asserção
        Assert.IsFalse(await card.IsVisibleAsync());

    }
}
