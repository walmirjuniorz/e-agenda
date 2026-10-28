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

public sealed class eAgendaDbContext(DbContextOptions<eAgendaDbContext> options) :
    IdentityDbContext<UsuarioIdentity, IdentityRole<Guid>, Guid>(options)
{
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
    }
}
