
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.Modulos.Compromissos;
using FluentResults;

public sealed class ServicoCompromisso(IRepositorioCompromisso repositorioCompromisso)
{
    public Result<Guid> Cadastrar(CadastrarCompromissoDto dto)
    {
        Compromisso compromisso = new(
            dto.Assunto,
            dto.DataOcorrencia,
            dto.HoraInicio,
            dto.HoraTermino,
            dto.TipoCompromissoEnum,
            dto.Link,
            dto.Local
            );

        List<ErroValidacao> erros = compromisso.Validar();

        if (erros.Count > 0)
        {
            List<Error> errosResultado = erros
            .Select(e => new Error(e.Mensagem).WithMetadata("Campo", e.Campo)).ToList();

            return Result.Fail(errosResultado);
        }

        repositorioCompromisso.Cadastrar(compromisso);

        return Result.Ok(compromisso.Id);
    }
    public Result Editar(EditarCompromissoDto dto)
    {
        Compromisso compromissoAtualizado = new(
            dto.Assunto,
            dto.DataOcorrencia,
            dto.HoraInicio,
            dto.HoraTermino,
            dto.TipoCompromissoEnum,
            dto.Link,
            dto.Local
            );

        List<ErroValidacao> erros = compromissoAtualizado.Validar();

        if (erros.Count > 0)
        {
            List<Error> errosResultado = erros
            .Select(e => new Error(e.Mensagem).WithMetadata("Campo", e.Campo)).ToList();

            return Result.Fail(errosResultado);
        }

        bool conseguiuEditar = repositorioCompromisso.Editar(dto.Id, compromissoAtualizado);

        if (!conseguiuEditar)
            return Result.Fail("Não foi possível editar o compromisso.");

        return Result.Ok();
    }
    public Result Excluir(Guid id)
    {
        bool conseguiuExcluir = repositorioCompromisso.Excluir(id);

        if (!conseguiuExcluir)
            return Result.Fail("Não foi possivel excluir o compromisso.");

        return Result.Ok();
    }
    public Result<CompromissoDto> SelecionarPorId(Guid id)
    {
        Compromisso? c = repositorioCompromisso.SelecionarPorId(id);

        if (c == null)
            return Result.Fail<CompromissoDto>("Compromisso não encontrado.");

        CompromissoDto dto = new(
            c.Id,
            c.Assunto,
            c.DataOcorrencia,
            c.HoraInicio,
            c.HoraTermino,
            c.TipoCompromissoEnum,
            c.Link,
            c.Local
        );
        return Result.Ok(dto);
    }
    public List<CompromissoDto> SelecionarTodos()
    {
        return repositorioCompromisso
            .SelecionarTodos()
            .Select(c => new CompromissoDto(
                c.Id,
                c.Assunto,
                c.DataOcorrencia,
                c.HoraInicio,
                c.HoraTermino,
                c.TipoCompromissoEnum,
                c.Link,
                c.Local
                ))
                .ToList();
    }
}
