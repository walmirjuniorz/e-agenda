using eAgenda.Aplicacao.Modulos.Contatos;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApp.Modulos.Contatos
{
    public class ContatoController(ServicoContato servicoContato) : Controller
    {
        [HttpGet]
        public ActionResult Listar()
        {
            List<ContatoDto> dtos = servicoContato.SelecionarTodos();

            return View(dtos);
        }
    }
}