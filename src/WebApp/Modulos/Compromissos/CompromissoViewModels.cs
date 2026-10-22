using static eAgenda.Dominio.Modulos.Compromissos.Compromisso;

public record CadastrarCompromissoViewModel(
    string Assunto,
    DateOnly Data,
    TimeOnly HoraInicio,
    TimeOnly HoraTermino,
    TipoCompromisso TipoCompromissoEnum,
    string? Link,
    string? Local
);
public record EditarCompromissoViewModel(
    Guid Id,
    string Assunto,
    DateOnly Data,
    TimeOnly HoraInicio,
    TimeOnly HoraTermino,
    TipoCompromisso TipoCompromissoEnum,
    string? Link,
    string? Local
);
public record ExcluirCompromissoViewModel(
    Guid Id,
    string Assunto,
    DateOnly Data,
    TipoCompromisso TipoCompromissoEnum
);
