using FluentResults;
using Microsoft.AspNetCore.Mvc;

using eAgenda.Dominio.Modulos.Compromissos;

public class CompromissoController(ServicoCompromisso servicoCompromisso) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<CompromissoDto> dtos = servicoCompromisso.SelecionarTodos();

        return View(dtos);
    }
    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarCompromissoViewModel viewModel = new(
            string.Empty,
            DateOnly.FromDateTime(DateTime.Today),
            TimeOnly.MinValue,
            TimeOnly.MinValue,
            Compromisso.TipoCompromisso.Presencial,
            string.Empty,
            string.Empty
        );

        return View(viewModel);

    }
    [HttpPost]
    public ActionResult Cadastrar(CadastrarCompromissoViewModel viewModel)
    {
        CadastrarCompromissoDto dto = new(
            viewModel.Assunto,
            viewModel.Data,
            viewModel.HoraInicio,
            viewModel.HoraTermino,
            viewModel.TipoCompromissoEnum,
            viewModel.Link,
            viewModel.Local
        );

        Result<Guid> resultado = servicoCompromisso.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            foreach (IError erro in resultado.Errors)
            {
                string campo = erro.Metadata["Campo"].ToString() ?? string.Empty;

                ModelState.Remove(campo);
                ModelState.AddModelError(campo, erro.Message);
            }

            return View(viewModel);
        }

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<CompromissoDto> resultado = servicoCompromisso.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;

            return RedirectToAction(nameof(Listar));
        }

        CompromissoDto dto = resultado.Value;

        EditarCompromissoViewModel viewModel = new(
            dto.Id,
            dto.Assunto,
            dto.DataOcorrencia,
            dto.HoraInicio,
            dto.HoraTermino,
            dto.TipoCompromissoEnum,
            dto.Link,
            dto.Local
        );

        return View(viewModel);
    }
    [HttpPost]
    public ActionResult Editar(EditarCompromissoViewModel viewModel)
    {
        EditarCompromissoDto dto = new(
            viewModel.Id,
            viewModel.Assunto,
            viewModel.Data,
            viewModel.HoraInicio,
            viewModel.HoraTermino,
            viewModel.TipoCompromissoEnum,
            viewModel.Link,
            viewModel.Local
                );

        Result resultado = servicoCompromisso.Editar(dto);

        if (resultado.IsFailed)
        {
            if (!resultado.Errors.Any(e => e.HasMetadataKey("Campo")))
            {
                TempData["MensagemErro"] = resultado.Errors.First().Message;

                return RedirectToAction(nameof(Listar));
            }
            foreach (IError erro in resultado.Errors)
            {
                string campo = erro.Metadata["Campo"].ToString() ?? string.Empty;

                ModelState.Remove(campo);
                ModelState.AddModelError(campo, erro.Message);
            }

            return View(viewModel);
        }
        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<CompromissoDto> resultado = servicoCompromisso.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;

            return RedirectToAction(nameof(Listar));
        }

        CompromissoDto dto = resultado.Value;

        ExcluirCompromissoViewModel viewModel = new(
            id,
            dto.Assunto,
            dto.DataOcorrencia,
            dto.TipoCompromissoEnum
        );

        return View(viewModel);
    }
    [HttpPost]
    public ActionResult Excluir(ExcluirCompromissoViewModel viewModel)
    {
        Result resultado = servicoCompromisso.Excluir(viewModel.Id);

        if (resultado.IsFailed)
            TempData["MensagemErro"] = resultado.Errors.First().Message;

        return RedirectToAction(nameof(Listar));
    }
}

