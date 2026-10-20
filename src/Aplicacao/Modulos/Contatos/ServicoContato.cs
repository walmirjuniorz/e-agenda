using eAgenda.Aplicacao.Modulos.Contatos;
using eAgenda.Dominio.Modulos.Contatos;

public sealed class ServicoContato(IRepositorioContato repositorioContato)
{
    public List<ContatoDto> SelecionarTodos()
    {
        List<Contato> contatos = repositorioContato.SelecionarTodos();

        return contatos.Select(c => new ContatoDto(c.Id, c.Nome, c.Email, c.Telefone)).ToList();
    }
}