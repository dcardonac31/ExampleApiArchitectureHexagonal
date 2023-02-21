using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonasMS.Domain.Entities;

namespace PersonasMS.Infraestructure.Data.Configs
{
    public class GeneroConfig : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.ToTable("Genero");

            builder.HasKey(e => new { e.Id }).HasName("PK_Genero");

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.NombreGenero)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.FechaCreacion);

            builder.Property(e => e.UsuarioCreacion)
                .IsRequired()
                .HasDefaultValue("System");

            builder.Property(e => e.FechaModificacion);

            builder.Property(e => e.UsuarioModificacion)
                .IsRequired(false);
        }
    }
}
