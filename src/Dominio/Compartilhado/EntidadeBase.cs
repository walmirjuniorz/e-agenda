namespace eAgenda.Dominio.Compartilhado;

public sealed class ErroValidacao
{
    public required string Campo { get; set; }
    public required string Mensagem { get; set; }
}

public interface IEntidadeComUsuario
{
    Guid UsuarioId { get; set; }
}

public abstract class EntidadeBase<T> : IEntidadeComUsuario
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid UsuarioId { get; set; } = Guid.Empty;
    public abstract List<ErroValidacao> Validar();
    public abstract void Atualizar(T entidadeAtualizada);
}
