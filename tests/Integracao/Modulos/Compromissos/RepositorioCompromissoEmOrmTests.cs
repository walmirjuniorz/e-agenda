using eAgenda.Dominio.Modulos.Compromissos;
using eAgenda.Infraestrutura.Modulos.Compromissos;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;

namespace eAgenda.Testes.Integracao.Modulos.Compromissos;

[TestClass]
public sealed class RepositorioCompromissoEmOrmTests : RepositorioEmOrmTestsBase
{
    [TestMethod]
    public void Cadastrar_DevePersistir_CompromissoNoBanco()
    {
        // Arranjo
        Guid compromissoId = Guid.CreateVersion7();

        Compromisso compromisso = new(
            "Reunião Online",
            new DateOnly(2026, 11, 06),
            new TimeOnly(11, 00, 00),
            new TimeOnly(12, 00, 00),
            TipoCompromisso.Remoto,
            "www.test.com",
            null
        )
        {
            Id = compromissoId,
            UsuarioId = usuarioId
        };

        RepositorioCompromissoEmOrm repositorioCompromisso = new(dbContext);
        // Ação
        repositorioCompromisso.Cadastrar(compromisso);
        dbContext.ChangeTracker.Clear();

        Compromisso? compromissoSelecionado = repositorioCompromisso.SelecionarPorId(compromisso.Id);
        // Asserção
        Assert.IsNotNull(compromissoSelecionado);
        Assert.AreEqual("Reunião Online", compromissoSelecionado.Assunto);
        Assert.AreEqual(new DateOnly(2026, 11, 06), compromissoSelecionado.DataOcorrencia);
        Assert.AreEqual(new TimeOnly(11, 00, 00), compromissoSelecionado.HoraInicio);
        Assert.AreEqual(new TimeOnly(12, 00, 00), compromissoSelecionado.HoraTermino);
        Assert.AreEqual(TipoCompromisso.Remoto, compromissoSelecionado.TipoCompromisso);
        Assert.IsNull(compromissoSelecionado.Local);
        Assert.IsNotNull(compromissoSelecionado.Link);
        Assert.AreEqual("www.test.com", compromissoSelecionado.Link);
    }
    [TestMethod]
    public void Editar_DeveAtualizar_CompromissoNoBanco()
    {
        // Arranjo

        // Ação

        // Asserção

    }
    [TestMethod]
    public void Excluir_DeveApagar_CompromissoNoBanco()
    {
        // Arranjo

        // Ação

        // Asserção

    }
}
