using System;
using Microsoft.Playwright;

namespace Tarefae2eTests.Compartilhado;

public abstract class e2eTestsBase
{
    private TestAplicationFactory aplicationFactory = null!;
    private IPlaywright playwright = null!;
    private IBrowser browser = null!;
    private IBrowserContext browserContext = null!;
    protected IPage page = null!;
    protected string? baseUrl = null;

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

        await aplicationFactory.DisposeAsync();
    }
    protected async Task CriarUsuarioAsync()
    {
        await page.GotoAsync($"{baseUrl}/Autenticacao/Cadastro");

        await page.GetByLabel("E-mail").FillAsync("juniortestes@gmail.com");
        await page.GetByLabel("Senha", new() { Exact = true }).FillAsync("Teste@123");
        await page.GetByLabel("Confirmar Senha", new() { Exact = true }).FillAsync("Teste@123");
        await page.GetByRole(AriaRole.Button, new() { Name = "Cadastrar" }).ClickAsync();

        await page.GetByLabel("E-mail").FillAsync("juniortestes@gmail.com");
        await page.GetByLabel("Senha").FillAsync("Teste@123");
        await page.GetByRole(AriaRole.Button, new() { Name = "Entrar" }).ClickAsync();
    }
}
