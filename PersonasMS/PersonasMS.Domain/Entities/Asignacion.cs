using PersonasMS.Domain.Entities.Base;

namespace PersonasMS.Domain.Entities
{
    public class Asignacion : BaseEntity
    {
        public int PersonaId { get; set; }
        public int ClienteId { get; set; }
        public int CargoId { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime FechaDesasignacion { get; set; }
    }
}
