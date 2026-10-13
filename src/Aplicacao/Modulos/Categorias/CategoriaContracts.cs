namespace eAgenda.Aplicacao.Modulos.Categorias;

// DTO = DAta Transfer Object

public record CategoriaDto(
    Guid Id, string Titulo
);

public record CadastrarCategoriaDto(
    string Titulo
);

public record EditarCategoriaDto(
    Guid Id, string Titulo
);

public record ExcluirCategoriaDto(

);

