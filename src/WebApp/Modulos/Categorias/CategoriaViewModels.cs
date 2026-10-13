using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eAgenda.WebApp.Modulos.Categorias;

public record CadastrarCategoriaViewModel(
    [ValidateNever]
     string Titulo);

public record EditarCategoriaViewModel(
    Guid Id,
    [ValidateNever]
     string Titulo);

public record ExcluirCategotiaViewModel(
    Guid Id,
     string Titulo);
