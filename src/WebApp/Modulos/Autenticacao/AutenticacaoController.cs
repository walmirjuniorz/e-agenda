using eAgenda.Infraestrutura.Compartilhado.Identity;
using eAgenda.WebApp.Compartilhado;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApp.Modulos.Autenticacao;

public class AutenticacaoController(
    UserManager<UsuarioIdentity> userManager, // resposavel pela criacao de usuarios e obtencao dos dados do mesmo
    SignInManager<UsuarioIdentity> signManager // responsavel pelo Login/Logout de usuarios ja criados
    ) : Controller
{
    [HttpGet]
    public ActionResult Cadastro()
    {
        return View(new CadastroUsuarioViewModel(string.Empty, string.Empty));
    }
    [HttpPost]
    public async Task<ActionResult> Cadastro(CadastroUsuarioViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        UsuarioIdentity usuario = new()
        {
            UserName = viewModel.Email.Trim(),
            Email = viewModel.Email.ToLower().Trim()
        };

        // Operação assíncrona
        IdentityResult resultado = await userManager.CreateAsync(usuario, viewModel.Senha);

        if (!resultado.Succeeded)
        {
            foreach (IdentityError erro in resultado.Errors)
                ModelState.AddModelError(string.Empty, erro.Description);

            return View(viewModel);
        }

        return RedirectToAction(nameof(Login));
    }
    [HttpGet]
    public ActionResult Login(string? returnUrl = null)
    {
        LoginUsuarioViewModel viewModel = new(
            string.Empty,
            string.Empty,
            false,
            returnUrl
        );

        return View(viewModel);
    }
    [HttpPost]
    public async Task<ActionResult> Login(LoginUsuarioViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        Microsoft.AspNetCore.Identity.SignInResult resultado = await signManager.PasswordSignInAsync(
            viewModel.Email.Trim(),
            viewModel.Senha,
            viewModel.LembrarMe,
            lockoutOnFailure: true
        );

        if (resultado.Succeeded)
        {
            return Url.IsLocalUrl(viewModel.ReturnUrl)
            ? LocalRedirect(viewModel.ReturnUrl!)
            : RedirectToAction(nameof(HomeController.Index), "Home");
        }

        ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");

        return View(viewModel);
    }
    [HttpPost]
    public async Task<ActionResult> Logout()
    {
        await signManager.SignOutAsync();

        return RedirectToAction(nameof(Login));
    }
}
