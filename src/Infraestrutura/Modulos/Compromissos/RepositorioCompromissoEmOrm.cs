using eAgenda.Dominio.Modulos.Compromissos;
using eAgenda.Infraestrutura.Compartilhado.Orm;

namespace eAgenda.Infraestrutura.Modulos.Compromissos;


public sealed class RepositorioCompromissoEmOrm(eAgendaDbContext dbContext) : RepositorioBaseEmOrm<Compromisso>(dbContext), IRepositorioCompromisso
{
    public override List<Compromisso> SelecionarTodos()
    {
        return registros
            .OrderBy(c => c.Assunto)
            .ToList();
    }
}
