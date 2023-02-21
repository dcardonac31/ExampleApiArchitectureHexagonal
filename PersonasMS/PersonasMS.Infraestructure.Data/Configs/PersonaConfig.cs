using PersonasMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonasMS.Infraestructure.Data.Configs
{
    public class PersonaConfig : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("Persona");

            builder.HasKey(e => new { e.Id }).HasName("PK_Persona");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Cedula)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.PrimerNombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.SegundoNombre)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(e => e.PrimerApellido)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.SegundoApellido)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(e => e.GeneroId)
                .IsRequired();

            builder.Property(e => e.CargoId)
                .IsRequired();

            builder.Property(e => e.FechaNacimiento);

            builder.Property(e => e.MunicipioNacimientoId)
                .IsRequired();

            builder.Property(e => e.MunicipioResidenciaId)
                .IsRequired();

            builder.Property(e => e.Direccion)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Activo)
                .HasDefaultValue(true);

            builder.Property(e => e.FechaCreacion);

            builder.Property(e => e.UsuarioCreacion)
                .IsRequired()
                .HasDefaultValue("System");

            builder.Property(e => e.FechaModificacion);

            builder.Property(e => e.UsuarioModificacion)
                .IsRequired(false);

            builder.HasIndex(e => e.Cedula).HasDatabaseName("IX_Persona_Cedula").IsUnique();

            builder.HasOne<Genero>().WithMany().HasForeignKey(e => e.GeneroId).HasConstraintName("FK_Persona_GeneroId").OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne<Cargo>().WithMany().HasForeignKey(e => e.CargoId).HasConstraintName("FK_Persona_CargoId").OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne<Municipio>().WithMany().HasForeignKey(e => e.MunicipioNacimientoId).HasConstraintName("FK_Persona_MunicipioNacimientoId").OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne<Municipio>().WithMany().HasForeignKey(e => e.MunicipioResidenciaId).HasConstraintName("FK_Persona_MunicipioResidenciaId").OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
