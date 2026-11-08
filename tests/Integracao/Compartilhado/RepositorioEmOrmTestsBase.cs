using eAgenda.Infraestrutura.Compartilhado.Identity;
using eAgenda.Infraestrutura.Compartilhado.Orm;
using eAgenda.Testes.Integracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

[TestClass]
public abstract class RepositorioEmOrmTestsBase
{
    private static string? connectionString = null;
    protected Guid usuarioId = Guid.Empty;
    protected eAgendaDbContext dbContext = null!;
    private string connectionStringTeste = null!;

    [AssemblyInitialize]
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
            Assert.Inconclusive("a ConnectionStrings:SqlServer não foi configurada.");
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

        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        CriarUsuario(dbContext, usuarioId);
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
