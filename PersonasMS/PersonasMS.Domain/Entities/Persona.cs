using PersonasMS.Domain.Entities.Base;

namespace PersonasMS.Domain.Entities
{
    public class Persona : BaseEntity
    {
        public string? Cedula { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int GeneroId { get; set; }
        public int CargoId { get; set; }

        public int MunicipioNacimientoId { get; set; }
        public int MunicipioResidenciaId { get; set; }
        public string? Direccion { get; set; }
        public bool? Activo { get; set; }
    }
}
