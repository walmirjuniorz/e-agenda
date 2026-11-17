using Microsoft.Playwright;

namespace Tarefae2eTests;

[TestClass]
public sealed class Tarefae2eTests
{
    private IPlaywright playwright = null!;
    private IBrowser browser = null!;
    private IBrowserContext browserContext = null!;
    private IPage page = null!;

    [TestInitialize]
    public async Task InicializarTeste()
    {
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
        await page.GotoAsync("http://localhost:5096/Tarefa/Cadastrar");

        await page.GetByLabel("E-mail").FillAsync("juniortestes@gmail.com");

        await page.GetByLabel("Senha").FillAsync("Teste@123");

        await page.GetByRole(AriaRole.Button, new() { Name = "Entrar" }).ClickAsync();

        // Ação

        // Asserção
    }
}
