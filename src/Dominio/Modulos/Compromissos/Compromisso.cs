using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.Modulos.Compromissos;

public partial class Compromisso : EntidadeBase<Compromisso>
{
    public string Assunto { get; set; } = string.Empty;
    public DateOnly DataOcorrencia { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraTermino { get; set; }
    public TipoCompromisso TipoCompromisso { get; set; }
    public string Link { get; set; } = string.Empty;
    public string Local { get; set; } = string.Empty;
    public Compromisso(string assunto, DateOnly dataOcorrencia, TimeOnly horaInicio, TimeOnly horaTermino, TipoCompromisso tipoCompromisso, string? link, string? local)
    {
        Assunto = assunto;
        DataOcorrencia = dataOcorrencia;
        HoraInicio = horaInicio;
        HoraTermino = horaTermino;
        TipoCompromisso = tipoCompromisso;
        Link = link!;
        Local = local!;
    }
    public Compromisso() { }

    public override void Atualizar(Compromisso entidadeAtualizada)
    {
        Assunto = entidadeAtualizada.Assunto;
        DataOcorrencia = entidadeAtualizada.DataOcorrencia;
        HoraInicio = entidadeAtualizada.HoraInicio;
        HoraTermino = entidadeAtualizada.HoraTermino;
        TipoCompromisso = entidadeAtualizada.TipoCompromisso;
        Link = entidadeAtualizada.Link;
        Local = entidadeAtualizada.Local;
    }

    public override List<ErroValidacao> Validar()
    {
        List<ErroValidacao> erros = [];

        if (string.IsNullOrWhiteSpace(Assunto) || Assunto.Length < 2 || Assunto.Length > 100)
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Assunto),
                Mensagem = "O campo \"Assunto\" deve conter entre 2 e 100 caracteres."
            });
        }
        if (!Enum.IsDefined(TipoCompromisso))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(TipoCompromisso),
                Mensagem = "O campo \"Tipo de Compromisso\" deve ser preenchido."
            });
        }
        if (TipoCompromisso == TipoCompromisso.Remoto && string.IsNullOrWhiteSpace(Link))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Link),
                Mensagem = "O link deve ser preenchido para compromissos remotos."
            });
        }
        if (TipoCompromisso == TipoCompromisso.Presencial && string.IsNullOrWhiteSpace(Local))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Local),
                Mensagem = "O local deve ser preenchido para compromissos presenciais."
            });
        }
        if (HoraInicio >= HoraTermino)
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(HoraTermino),
                Mensagem = "A hora de término deve ser posterior à hora de início."
            });
        }
        return erros;
    }
}
