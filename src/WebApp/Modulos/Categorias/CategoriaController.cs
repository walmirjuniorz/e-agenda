using eAgenda.Dominio.Modulos.Categorias;
using eAgenda.WebApp.Modulos.Categorias;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApp.Modulos.ModuloCategorias.Apresentacao;

public sealed class CategoriaController(IRepositorioCategoria repositorioCategoria) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

        List<ListarCategoriasViewModel> listarVms = categorias
        .Select(c => new ListarCategoriasViewModel(c.Id, c.Titulo)).ToList();

        return View();
    }
    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarCategotiaViewModel viewModel = new("");

        return View(viewModel);
    }
    [HttpPost]
    public ActionResult Cadastrar(CadastrarCategotiaViewModel viewModel)
    {
        Categoria categoria = new Categoria(viewModel.Titulo);

        List<string> erros = categoria.Validar();

        if (repositorioCategoria.ExisteCategoriaPorTitulo(categoria.Titulo))
        {
            erros.Add("Já existe uma categoria cadastrada com o título informado.");
        }

        if (erros.Count > 0)
        {
            ModelState.AddModelError(string.Empty, erros[0]);

            return View(viewModel);
        }

        repositorioCategoria.Cadastrar(categoria);

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
