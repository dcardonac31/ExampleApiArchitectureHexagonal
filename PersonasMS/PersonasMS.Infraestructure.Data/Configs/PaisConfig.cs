using PersonasMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonasMS.Infraestructure.Data.Configs
{
    public class PaisConfig : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("Pais");

            builder.HasKey(e => new { e.Id }).HasName("PK_Pais");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.CodigoDIAN)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(e => e.NombrePais)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.FechaCreacion);

            builder.Property(e => e.UsuarioCreacion)
                .IsRequired()
                .HasDefaultValue("System");

            builder.Property(e => e.FechaModificacion);

            builder.Property(e => e.UsuarioModificacion)
                .IsRequired(false);

            builder.HasIndex(e => e.CodigoDIAN).HasDatabaseName("IX_Pais_CodigoDIAN").IsUnique();
        }
    }
}
