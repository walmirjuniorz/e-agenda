using eAgenda.Dominio.Modulos.Contatos;
using eAgenda.Infraestrutura.Compartilhado.Orm;

public sealed class RepositorioContatoEmOrm(eAgendaDbContext dbContext) :
    RepositorioBaseEmOrm<Contato>(dbContext), IRepositorioContato
{
    public override List<Contato> SelecionarTodos()
    {
        return registros
            .OrderBy(c => c.Nome)
            .ToList();
    }
    public bool ExisteContatoComEmail(string contatoEmail, Guid? idIgnorado = null)
    {
        return registros.Any(c =>
            c.Id != idIgnorado &&
            c.Email.ToLower() == contatoEmail.ToLower());
    }
    public bool ExisteContatoComTelefone(string contatoTelefone, Guid? idIgnorado = null)
    {
        return registros.Any(c =>
            c.Id != idIgnorado &&
            c.Telefone.ToLower() == contatoTelefone.ToLower());
    }
}
