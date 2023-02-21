using PersonasMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonasMS.Infraestructure.Data.Configs
{
    public class DepartamentoConfig : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder.ToTable("Departamento");

            builder.HasKey(e => new { e.Id }).HasName("PK_Departamento");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.CodigoEstadistico)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.PaisId)
                .IsRequired();

            builder.Property(e => e.NombreDepartamento)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.FechaCreacion);

            builder.Property(e => e.UsuarioCreacion)
                .IsRequired()
                .HasDefaultValue("System");

            builder.Property(e => e.FechaModificacion);

            builder.Property(e => e.UsuarioModificacion)
                .IsRequired(false);

            builder.HasOne<Pais>().WithMany().HasForeignKey(e => e.PaisId).HasConstraintName("FK_Departamento_PaisId").OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
