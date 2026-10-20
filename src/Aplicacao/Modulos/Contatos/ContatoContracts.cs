namespace eAgenda.Aplicacao.Modulos.Contatos;

public record ContatoDto(
    Guid Id,
    string Nome,
    string Email,
    string Telefone
);