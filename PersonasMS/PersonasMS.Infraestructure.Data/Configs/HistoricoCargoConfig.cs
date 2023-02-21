using PersonasMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonasMS.Infraestructure.Data.Configs
{
    public class HistoricoCargoConfig : IEntityTypeConfiguration<HistoricoCargo>
    {
        public void Configure(EntityTypeBuilder<HistoricoCargo> builder)
        {
            builder.ToTable("HistoricoCargo");

            builder.HasKey(e => new { e.Id }).HasName("PK_HistoricoCargo");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.PersonaId)
                .IsRequired();

            builder.Property(e => e.CargoId)
                .IsRequired();

            builder.Property(e => e.FechaAsignacion)
                .IsRequired();

            builder.Property(e => e.FechaDesasignacion);

            builder.Property(e => e.FechaCreacion);

            builder.Property(e => e.UsuarioCreacion)
                .IsRequired()
                .HasDefaultValue("System");

            builder.Property(e => e.FechaModificacion);

            builder.Property(e => e.UsuarioModificacion)
                .IsRequired(false);

            builder.HasOne<Persona>().WithMany().HasForeignKey(e => e.PersonaId).HasConstraintName("FK_HistoricoCargo_PersonaId").OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne<Cargo>().WithMany().HasForeignKey(e => e.CargoId).HasConstraintName("FK_HistoricoCargo_CargoId").OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
