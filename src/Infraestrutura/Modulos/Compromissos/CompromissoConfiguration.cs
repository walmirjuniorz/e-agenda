using eAgenda.Dominio.Modulos.Compromissos;
using eAgenda.Infraestrutura.Compartilhado.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class CompromissoConfiguration : IEntityTypeConfiguration<Compromisso>
{
    public void Configure(EntityTypeBuilder<Compromisso> builder)
    {
        builder.ToTable("TBCompromissos");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.Property(c => c.Assunto)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(c => c.Assunto)
            .IsUnique();

        builder.Property(c => c.DataOcorrencia)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(c => c.HoraInicio)
            .HasColumnType("time")
            .IsRequired();

        builder.Property(c => c.HoraTermino)
            .HasColumnType("time")
            .IsRequired();

        builder.Property(c => c.TipoCompromissoEnum)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(c => c.Link)
            .IsRequired(false);

        builder.Property(c => c.Local)
            .IsRequired(false);

        builder.HasOne<UsuarioIdentity>()
            .WithMany()
            .HasForeignKey(c => c.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
