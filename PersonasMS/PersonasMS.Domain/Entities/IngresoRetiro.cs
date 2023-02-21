using PersonasMS.Domain.Entities.Base;

namespace PersonasMS.Domain.Entities
{
    public class IngresoRetiro : BaseEntity
    {
        public int PersonaId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaRetiro { get; set; }
    }
}
