namespace PersonasMS.Domain.Dto
{
    public class PersonaCreateDto
    {
        public string? Cedula { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public int GeneroId { get; set; }
        public int CargoId { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int MunicipioNacimientoId { get; set; }
        public int MunicipioResidenciaId { get; set; }
        public string? Direccion { get; set; }
        public bool? Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }

    public class PersonaDto : PersonaCreateDto
    {
        public int Id { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
