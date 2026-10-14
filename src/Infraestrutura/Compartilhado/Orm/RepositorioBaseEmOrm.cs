using eAgenda.Dominio.Compartilhado;
using eAgenda.Infraestrutura.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

public abstract class RepositorioBaseEmOrm<T>(eAgendaDbContext dbContext) where T : EntidadeBase<T>
{
    protected readonly DbSet<T> registros = dbContext.Set<T>();

    public void Cadastrar(T entidade)
    {
        registros.Add(entidade);

        dbContext.SaveChanges();
    }
    public bool Editar(Guid idSelecionado, T entidadeAtualizada)
    {
        T? entidadeSeleciona = SelecionarPorId(idSelecionado);

        if (entidadeSeleciona == null)
            return false;

        entidadeSeleciona.Atualizar(entidadeAtualizada);

        dbContext.SaveChanges();

        return true;
    }
    public bool Excluir(Guid idSelecionado)
    {
        T? entidadeSeleciona = SelecionarPorId(idSelecionado);

        if (entidadeSeleciona == null)
            return false;

        registros.Remove(entidadeSeleciona);
        dbContext.SaveChanges();

        return true;
    }
    public virtual T? SelecionarPorId(Guid idSelecionado)
    {
        return registros.SingleOrDefault(c => c.Id == idSelecionado);
    }
    public virtual List<T> SelecionarTodos()
    {
        return registros
            .ToList();
    }
}

