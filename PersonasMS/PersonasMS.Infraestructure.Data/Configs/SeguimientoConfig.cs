using PersonasMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonasMS.Infraestructure.Data.Configs
{
    public class SeguimientoConfig : IEntityTypeConfiguration<Seguimiento>
    {
        public void Configure(EntityTypeBuilder<Seguimiento> builder)
        {
            builder.ToTable("Seguimiento");

            builder.HasKey(e => new { e.Id }).HasName("PK_Seguimiento");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.PersonaId)
                .IsRequired();

            builder.Property(e => e.CargoId)
                .IsRequired();

            builder.Property(e => e.ClienteId)
                .IsRequired();

            builder.Property(e => e.FechaSeguimiento)
                .IsRequired();

            builder.Property(e => e.TecnologiasUsadas)
                .IsRequired();

            builder.Property(e => e.MetodologiasAgilesUsadas)
                .IsRequired();

            builder.Property(e => e.ValoracionSatisfaccionSofkianoCliente)
                .IsRequired();

            builder.Property(e => e.ObservacionesSofkianoCliente)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.Property(e => e.ValoracionSatisfaccionClienteSofkiano)
                .IsRequired();

            builder.Property(e => e.ObservacionesClienteSofkiano)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(e => e.FechaCreacion);

            builder.Property(e => e.UsuarioCreacion)
                .IsRequired()
                .HasDefaultValue("System");

            builder.Property(e => e.FechaModificacion);

            builder.Property(e => e.UsuarioModificacion)
                .IsRequired(false);

            builder.HasOne<Persona>().WithMany().HasForeignKey(e => e.PersonaId).HasConstraintName("FK_Seguimiento_PersonaId").OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne<Cargo>().WithMany().HasForeignKey(e => e.CargoId).HasConstraintName("FK_Seguimiento_CargoId").OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne<Cliente>().WithMany().HasForeignKey(e => e.ClienteId).HasConstraintName("FK_Seguimiento_ClienteId").OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
