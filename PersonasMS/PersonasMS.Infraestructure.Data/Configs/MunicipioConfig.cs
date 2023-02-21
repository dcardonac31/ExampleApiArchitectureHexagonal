using PersonasMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonasMS.Infraestructure.Data.Configs
{
    public class MunicipioConfig : IEntityTypeConfiguration<Municipio>
    {
        public void Configure(EntityTypeBuilder<Municipio> builder)
        {
            builder.ToTable("Municipio");

            builder.HasKey(e => new { e.Id }).HasName("PK_Municipio");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.CodigoEstadistico)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.DepartamentoId)
                .IsRequired();

            builder.Property(e => e.NombreMunicipio)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.FechaCreacion);

            builder.Property(e => e.UsuarioCreacion)
                .IsRequired()
                .HasDefaultValue("System");

            builder.Property(e => e.FechaModificacion);

            builder.Property(e => e.UsuarioModificacion)
                .IsRequired(false);

            builder.HasOne<Departamento>().WithMany().HasForeignKey(e => e.DepartamentoId).HasConstraintName("FK_Municipio_DepartamentoId").OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
