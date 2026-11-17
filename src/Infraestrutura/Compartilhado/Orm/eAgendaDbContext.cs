using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Categorias;
using eAgenda.Dominio.Modulos.Compromissos;
using eAgenda.Dominio.Modulos.Contatos;
using eAgenda.Dominio.Modulos.Despesas;
using eAgenda.Dominio.Modulos.Tarefas;
using eAgenda.Infraestrutura.Compartilhado.Identity;
using eAgenda.Infraestrutura.Modulos.Categorias;
using eAgenda.Infraestrutura.Modulos.Contatos;
using eAgenda.Infraestrutura.Modulos.Despesas;
using eAgenda.Infraestrutura.Modulos.Tarefas;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infraestrutura.Compartilhado.Orm;

public sealed class eAgendaDbContext(
    DbContextOptions<eAgendaDbContext> options, IProvedorUsuarioAtual usuarioAtual) :
    IdentityDbContext<UsuarioIdentity,
    IdentityRole<Guid>,
    Guid>(options)
{
    public Guid UsuarioAtualId => usuarioAtual.Id;

    public DbSet<Contato> Contatos => Set<Contato>();
    public DbSet<Compromisso> Compromissos => Set<Compromisso>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Despesa> Despesas => Set<Despesa>();
    public DbSet<Tarefa> Tarefas => Set<Tarefa>();
    public DbSet<ItemTarefa> ItemTarefa => Set<ItemTarefa>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ContatoConfiguration());
        modelBuilder.ApplyConfiguration(new CompromissoConfiguration());
        modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        modelBuilder.ApplyConfiguration(new DespesaConfiguration());
        modelBuilder.ApplyConfiguration(new TarefaConfiguration());
        modelBuilder.ApplyConfiguration(new ItemTarefaConfiguration());

        modelBuilder.Entity<Contato>().HasQueryFilter(c => c.UsuarioId == UsuarioAtualId);
        modelBuilder.Entity<Compromisso>().HasQueryFilter(c => c.UsuarioId == UsuarioAtualId);
        modelBuilder.Entity<Categoria>().HasQueryFilter(c => c.UsuarioId == UsuarioAtualId);
        modelBuilder.Entity<Despesa>().HasQueryFilter(d => d.UsuarioId == UsuarioAtualId);
        modelBuilder.Entity<Tarefa>().HasQueryFilter(t => t.UsuarioId == UsuarioAtualId);
        modelBuilder.Entity<ItemTarefa>().HasQueryFilter(i => i.UsuarioId == UsuarioAtualId);
    }

    public override int SaveChanges() => SaveChanges(true);

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        DefinirUsuarioNasEntidadesNovas();

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        SaveChangesAsync(true, cancellationToken);

    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        DefinirUsuarioNasEntidadesNovas();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void DefinirUsuarioNasEntidadesNovas()
    {
        foreach (var entrada in ChangeTracker.Entries<IEntidadeComUsuario>())
        {
            if (entrada.State == EntityState.Added)
                entrada.Entity.UsuarioId = UsuarioAtualId;
        }
    }
}
