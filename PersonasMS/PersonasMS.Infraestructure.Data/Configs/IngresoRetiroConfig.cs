using PersonasMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonasMS.Infraestructure.Data.Configs
{
    public class IngresoRetiroConfig : IEntityTypeConfiguration<IngresoRetiro>
    {
        public void Configure(EntityTypeBuilder<IngresoRetiro> builder)
        {
            builder.ToTable("IngresoRetiro");

            builder.HasKey(e => new { e.Id }).HasName("PK_IngresoRetiro");

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.PersonaId)
                .IsRequired();

            builder.Property(e => e.FechaIngreso);

            builder.Property(e => e.FechaRetiro);

            builder.Property(e => e.FechaCreacion);

            builder.Property(e => e.UsuarioCreacion)
                .IsRequired()
                .HasDefaultValue("System");

            builder.Property(e => e.FechaModificacion);

            builder.Property(e => e.UsuarioModificacion);

            builder.HasOne<Persona>().WithMany().HasForeignKey(e => e.PersonaId).HasConstraintName("FK_IngresoRetiro_PersonaId").OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
