using eAgenda.Infraestrutura.Compartilhado.Orm;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tarefae2eTests.Compartilhado;

public sealed class TestAplicationFactory : WebApplicationFactory<Program>
{
    public string? BaseUrl { get; set; }
    public async Task StartAsync()
    {
        StartupTestServer();

        await CleanupTestDatebaseAsync();

    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
    }

    private void StartupTestServer()
    {
        // Inicializam o servidor em uma porta aleatoria da network
        // ex: localhost:5048 porta livre
        UseKestrel(0);
        StartServer();

        // IServer traz informacoes sobre o servidor em tempo de execucao (runtime)
        IServer server = Services.GetRequiredService<IServer>();

        IServerAddressesFeature? serverAddressesFeature = server.Features.Get<IServerAddressesFeature>();

        if (serverAddressesFeature is null)
            throw new InvalidOperationException("não foi possível obter a URL do servidor");

        BaseUrl = serverAddressesFeature.Addresses.Single();
    }

    private async Task CleanupTestDatebaseAsync()
    {
        using IServiceScope scope = Services.CreateScope();

        eAgendaDbContext dbContext = scope.ServiceProvider.GetRequiredService<eAgendaDbContext>();

        // Apagamos todos os dados das tabelas do banco de testes
        dbContext.Despesas.RemoveRange(dbContext.Despesas.IgnoreQueryFilters());
        dbContext.Categorias.RemoveRange(dbContext.Categorias.IgnoreQueryFilters());
        dbContext.ItemTarefa.RemoveRange(dbContext.ItemTarefa.IgnoreQueryFilters());
        dbContext.Tarefas.RemoveRange(dbContext.Tarefas.IgnoreQueryFilters());
        dbContext.Compromissos.RemoveRange(dbContext.Compromissos.IgnoreQueryFilters());
        dbContext.Contatos.RemoveRange(dbContext.Contatos.IgnoreQueryFilters());

        dbContext.Users.RemoveRange(dbContext.Users.IgnoreQueryFilters());

        await dbContext.SaveChangesAsync();
    }
}
