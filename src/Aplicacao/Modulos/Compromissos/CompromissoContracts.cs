using static eAgenda.Dominio.Modulos.Compromissos.Compromisso;

public record CompromissoDto(
    Guid Id,
    string Assunto,
    DateOnly DataOcorrencia,
    TimeOnly HoraInicio,
    TimeOnly HoraTermino,
    TipoCompromisso TipoCompromissoEnum,
    string? Link,
    string? Local
);

public record CadastrarCompromissoDto(
    string Assunto,
    DateOnly DataOcorrencia,
    TimeOnly HoraInicio,
    TimeOnly HoraTermino,
    TipoCompromisso TipoCompromissoEnum,
    string? Link,
    string? Local
);
public record EditarCompromissoDto(
    Guid Id,
    string Assunto,
    DateOnly DataOcorrencia,
    TimeOnly HoraInicio,
    TimeOnly HoraTermino,
    TipoCompromisso TipoCompromissoEnum,
    string? Link,
    string? Local
);

public record ExcluirCompromissoDto(
    Guid Id,
    string Assunto
);
