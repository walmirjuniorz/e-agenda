using System;
using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.Modulos.Tarefas;

public class Tarefa : EntidadeBase<Tarefa>
{
    public string Titulo { get; set; } = string.Empty;
    public PrioridadeTarefa Prioridade { get; set; }
    public DateOnly DataCriacao { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    public Tarefa() { }
    public Tarefa(string titulo, PrioridadeTarefa prioridade)
    {
        Titulo = titulo;
        Prioridade = prioridade;
    }
    public override List<ErroValidacao> Validar()
    {
        List<ErroValidacao> erros = [];

        if (string.IsNullOrWhiteSpace(Titulo) || Titulo.Length < 2 || Titulo.Length > 100)
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Titulo),
                Mensagem = "O campo \"Título\" deve conter entre 2 e 100 caracteres."
            });
        }
        if (Enum.IsDefined(Prioridade))
        {
            erros.Add(new ErroValidacao
            {
                Campo = nameof(Prioridade),
                Mensagem = "O campo \"Prioridade\" deve ser preenchido."
            });
        }
        return erros;
    }
    public override void Atualizar(Tarefa entidadeAtualizada)
    {
        Titulo = entidadeAtualizada.Titulo;
        Prioridade = entidadeAtualizada.Prioridade;
        DataCriacao = entidadeAtualizada.DataCriacao;
    }
}
