namespace PersonasMS.Domain.Dto
{
    public class SeguimientoCreateDto
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
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }

    public class SeguimientoDto : SeguimientoCreateDto
    {
        public int Id { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
