using eAgenda.Aplicacao.Modulos.Contatos;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Contatos;
using FluentResults;

public sealed class ServicoContato(IRepositorioContato repositorioContato)
{
    public Result<Guid> Cadastrar(CadastrarContatoDto dto)
    {
        Contato contato = new Contato(dto.Nome, dto.Email, dto.Telefone);

        List<ErroValidacao> erros = contato.Validar();

        if (repositorioContato.ExisteContatoComEmail(contato.Email))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(dto.Email),
                Mensagem = "Já existe um contato cadastrado com o e-mail informado."
            });
        }
        if (repositorioContato.ExisteContatoComTelefone(contato.Telefone))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(dto.Telefone),
                Mensagem = "Já existe um contato cadastrado com o telefone informado."
            });
        }
        if (erros.Count > 0)
        {
            List<Error> errosResultado = erros
            .Select(e => new Error(e.Mensagem).WithMetadata("Campo", e.Campo)).ToList();

            return Result.Fail(errosResultado);
        }

        repositorioContato.Cadastrar(contato);

        return Result.Ok(contato.Id);
    }

    public Result Editar(EditarContatoDto dto)
    {
        Contato contatoAtualizado = new(dto.Nome, dto.Email, dto.Telefone);

        List<ErroValidacao> erros = contatoAtualizado.Validar();

        if (repositorioContato.ExisteContatoComEmail(contatoAtualizado.Email))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(dto.Email),
                Mensagem = "Já existe um contato cadastrado com o e-mail informado."
            });
        }
        if (repositorioContato.ExisteContatoComTelefone(contatoAtualizado.Telefone))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(dto.Telefone),
                Mensagem = "Já existe um contato cadastrado com o telefone informado."
            });
        }
        if (erros.Count > 0)
        {
            List<Error> errosResultado = erros
            .Select(e => new Error(e.Mensagem).WithMetadata("Campo", e.Campo)).ToList();

            return Result.Fail(errosResultado);
        }

        bool conseguiuEditar = repositorioContato.Editar(dto.Id, contatoAtualizado);

        if (!conseguiuEditar)
            return Result.Fail("Não foi possível editar o contato.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {


        return Result.Ok();
    }

    public Result<ContatoDto> SelecionarPorId(Guid id)
    {
        Contato? c = repositorioContato.SelecionarPorId(id);

        if (c == null)
            return Result.Fail("Categoria não encontrada");

        return Result.Ok(new ContatoDto(c.Id, c.Nome, c.Email, c.Telefone));
    }

    public List<ContatoDto> SelecionarTodos()
    {
        List<Contato> contatos = repositorioContato.SelecionarTodos();

        return contatos.Select(c => new ContatoDto(c.Id, c.Nome, c.Email, c.Telefone)).ToList();
    }
}
