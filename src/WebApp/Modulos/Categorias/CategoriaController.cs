using eAgenda.Aplicacao.Modulos.Categorias;
using eAgenda.WebApp.Modulos.Categorias;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApp.Modulos.ModuloCategorias.Apresentacao;

public sealed class CategoriaController(ServicoCategoria servicoCategoria) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<CategoriaDto> categorias = servicoCategoria.SelecionarTodos();

        return View(categorias);
    }
    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarCategoriaViewModel viewModel = new("");

        return View(viewModel);
    }
    [HttpPost]
    public ActionResult Cadastrar(CadastrarCategoriaViewModel viewModel)
    {
        Result<Guid> resultado = servicoCategoria.Cadastrar(new CadastrarCategoriaDto(viewModel.Titulo));

        if (resultado.IsFailed)
        {
            string mensagemErro = resultado.Errors.Select(e => e.Message).First();

            ModelState.AddModelError(string.Empty, mensagemErro);

            return View(viewModel);
        }

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        return RedirectToAction(nameof(Listar));
    }
    [HttpPost]
    public ActionResult Editar(Guid id, EditarCategotiaViewModel viewModel)
    {
        return View();
    }
    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        return View();
    }
    [HttpPost]
    public ActionResult Excluir(ExcluirCategotiaViewModel viewModel)
    {
        return RedirectToAction(nameof(Listar));
    }
}
