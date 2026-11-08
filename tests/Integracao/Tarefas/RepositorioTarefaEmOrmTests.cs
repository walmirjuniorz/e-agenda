using System.Data.Common;
using eAgenda.Dominio.Modulos.Tarefas;
using eAgenda.Infraestrutura.Compartilhado.Identity;
using eAgenda.Infraestrutura.Compartilhado.Orm;
using eAgenda.Infraestrutura.Modulos.Tarefas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace eAgenda.Testes.Integracao.Compartilhado;

[TestClass]
public sealed partial class RepositorioTarefaEmOrmTests
{
    private static string? connectionString = null;
    private Guid usuarioId = Guid.Empty;
    private eAgendaDbContext dbContext = null!;
    private RepositorioTarefaEmOrm repositorioTarefa = null!;
    private string connectionStringTeste = null!;
    [ClassInitialize]
    public static void InicializarClasse(TestContext testContext)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .AddUserSecrets("96cee23d-32f5-4420-8ec2-6e965d8f8908")
            .Build();

        connectionString = configuration.GetConnectionString("SqlServer")
            ?? Environment.GetEnvironmentVariable("ConnectionStrings__SqlServer");

        if (string.IsNullOrWhiteSpace(connectionString))
            Assert.Inconclusive("Configure ConnectionStrings:SqlServer nos User Secrets do WebApp ou na variável de ambiente ConnectionStrings__SqlServer.");
    }
    [TestInitialize]
    public void InicializarTeste()
    {
        usuarioId = Guid.CreateVersion7();
        connectionStringTeste = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder(connectionString)
        {
            InitialCatalog = $"eAgenda_Testes_{Guid.NewGuid():N}"
        }.ConnectionString;
        dbContext = CriarContexto(usuarioId, connectionStringTeste);
        repositorioTarefa = new RepositorioTarefaEmOrm(dbContext);

        dbContext.Database.EnsureCreated();

        CriarUsuario(dbContext, usuarioId);
    }
    [TestMethod]
    public void Cadastrar_DevePersistir_TarefaEItensNoBanco()
    {
        // Arranjo
        Guid tarefaId = Guid.CreateVersion7();

        Tarefa tarefa = new Tarefa("Preparar Apresentação", PrioridadeTarefa.Alta)
        {
            Id = tarefaId,
            UsuarioId = usuarioId,
            DataCriacao = new DateOnly(2026, 11, 03)
        };

        tarefa.AdicionarItem(new ItemTarefa(tarefa, "Revisar conteúdo") { UsuarioId = usuarioId });
        tarefa.AdicionarItem(new ItemTarefa(tarefa, "Montar slides") { UsuarioId = usuarioId, Concluido = true });

        // Ação
        repositorioTarefa.Cadastrar(tarefa);
        dbContext.ChangeTracker.Clear();

        Tarefa? tarefaSelecionada = repositorioTarefa.SelecionarPorId(tarefaId);

        //Asserção
        Assert.IsNotNull(tarefaSelecionada);
        Assert.AreEqual("Preparar Apresentação", tarefaSelecionada.Titulo);
        Assert.AreEqual(PrioridadeTarefa.Alta, tarefaSelecionada.Prioridade);
        Assert.AreEqual(new DateOnly(2026, 11, 03), tarefaSelecionada.DataCriacao);
        Assert.AreEqual(50, tarefaSelecionada.PercentualConcluido);
        Assert.HasCount(2, tarefaSelecionada.Itens);

        CollectionAssert.AreEqual(
            new[] { "Montar slides", "Revisar conteúdo" },
            tarefaSelecionada.Itens.Select(i => i.Titulo).ToArray()
        );
    }
    [TestCleanup]
    public void FinalizarTeste()
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Dispose();
    }

    private static eAgendaDbContext CriarContexto(Guid usuarioId, string connectionString)
    {
        DbContextOptions<eAgendaDbContext> options = new DbContextOptionsBuilder<eAgendaDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        return new eAgendaDbContext(options, new ProvedorUsuarioAtualFake(usuarioId));
    }
    private static void CriarUsuario(eAgendaDbContext dbContext, Guid usuarioId)
    {
        dbContext.Users.Add(new UsuarioIdentity
        {
            Id = usuarioId,
            UserName = $"integracao-{usuarioId:N}",
            NormalizedUserName = $"INTEGRACAO-{usuarioId:N}",
            Email = $"{usuarioId:N}@test.local",
            NormalizedEmail = $"{usuarioId:N}@TEST.LOCAL",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        });

        dbContext.SaveChanges();
    }
}
