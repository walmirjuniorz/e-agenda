using Microsoft.EntityFrameworkCore;
using eAgenda.Dominio.Modulos.Contatos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eAgenda.Infraestrutura.Compartilhado.Identity;

namespace eAgenda.Infraestrutura.Modulos.Contatos;

public class ContatoConfiguration : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("TBContatos");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.Property(c => c.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(c => new { c.UsuarioId, c.Email })
            .IsUnique();

        builder.HasIndex(c => new { c.UsuarioId, c.Telefone })
            .IsUnique();

        builder.HasOne<UsuarioIdentity>()
            .WithMany()
            .HasForeignKey(c => c.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
