using PersonasMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonasMS.Infraestructure.Data.Configs
{
    public class AsignacionConfig : IEntityTypeConfiguration<Asignacion>
    {
        public void Configure(EntityTypeBuilder<Asignacion> builder)
        {
            builder.ToTable("Asignacion");

            builder.HasKey(e => new { e.Id }).HasName("PK_Asignacion");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.PersonaId)
                .IsRequired();

            builder.Property(e => e.ClienteId)
                .IsRequired();

            builder.Property(e => e.CargoId)
                .IsRequired();

            builder.Property(e => e.FechaAsignacion);

            builder.Property(e => e.FechaDesasignacion);

            builder.Property(e => e.FechaCreacion);

            builder.Property(e => e.UsuarioCreacion)
                .IsRequired()
                .HasDefaultValue("System");

            builder.Property(e => e.FechaModificacion);

            builder.Property(e => e.UsuarioModificacion);

            builder.HasOne<Persona>().WithMany().HasForeignKey(e => e.PersonaId).HasConstraintName("FK_Asignacion_PersonaId").OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne<Cliente>().WithMany().HasForeignKey(e => e.ClienteId).HasConstraintName("FK_Asignacion_ClienteId").OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne<Cargo>().WithMany().HasForeignKey(e => e.CargoId).HasConstraintName("FK_Asignacion_CargoId").OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
