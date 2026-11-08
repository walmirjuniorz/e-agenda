using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Testes.Integracao;

public sealed class ProvedorUsuarioAtualFake(Guid id) : IProvedorUsuarioAtual
{
    public Guid Id => id;
}

