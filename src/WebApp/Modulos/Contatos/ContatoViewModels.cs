using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eAgenda.WebApp.Modulos.Contatos;

[ValidateNever]
public record EditarContatoViewModel(
    Guid Id,
    string Nome,
    string Email,
    string Telefone
);
[ValidateNever]
public record ExcluirContatoViewModel(
    Guid Id
);