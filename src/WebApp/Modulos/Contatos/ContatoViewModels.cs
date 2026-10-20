using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eAgenda.WebApp.Modulos.Contatos;

public record CadastrarContatoViewModel(
    [ValidateNever] string Nome,
    [ValidateNever] string Email,
    [ValidateNever] string Telefone
);
public record EditarContatoViewModel(
    Guid Id,
    [ValidateNever] string Nome,
    [ValidateNever] string Email,
    [ValidateNever] string Telefone
);
public record ExcluirContatoViewModel(
    Guid Id, string Nome, string Email, string Telefone
);
