using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.Modulos.Contatos;

public interface IRepositorioContato : IRepositorio<Contato>
{
    bool ExisteContatoComEmail(string emailContato, Guid? idIgnorado = null);
    bool ExisteContatoComTelefone(string telefoneContato, Guid? idIgnorado = null);
}
