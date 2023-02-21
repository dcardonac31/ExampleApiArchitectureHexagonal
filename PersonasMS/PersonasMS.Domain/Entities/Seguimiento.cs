using PersonasMS.Domain.Entities.Base;

namespace PersonasMS.Domain.Entities
{
    public class Seguimiento : BaseEntity
    {
        public int PersonaId { get; set; }
        public int CargoId { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaSeguimiento { get; set; }
        public string? TecnologiasUsadas { get; set; }
        public string? MetodologiasAgilesUsadas { get; set; }
        public int ValoracionSatisfaccionSofkianoCliente { get; set; }
        public string? ObservacionesSofkianoCliente { get; set; }
        public int ValoracionSatisfaccionClienteSofkiano { get; set; }
        public string? ObservacionesClienteSofkiano { get; set; }
    }
}
