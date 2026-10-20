using System.Text.RegularExpressions;
using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.Modulos.Contatos;

public class Contato : EntidadeBase<Contato>
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;

    public Contato() { }

    public Contato(string nome, string email, string telefone)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }

    public override void Atualizar(Contato entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Email = entidadeAtualizada.Email;
        Telefone = entidadeAtualizada.Telefone;
    }

    public override List<ErroValidacao> Validar()
    {
        List<ErroValidacao> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome?.Length < 2 || Nome?.Length > 100)
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Nome),
                Mensagem = "O campo \"Nome\" deve conter entre 2 e 100 caracteres."
            }
            );
        }
        if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Email),
                Mensagem = "O campo \"E-mail\" deve conter um endereço de e-mail válido."
            }
            );
        }
        if (!Regex.IsMatch(Telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Telefone),
                Mensagem = "O campo \"Telefone\" deve estar no formato (00) 0000-0000 ou (00) 00000-0000."
            }
            );
        }
        return erros;
    }
}
