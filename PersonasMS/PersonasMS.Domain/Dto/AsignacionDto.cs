namespace PersonasMS.Domain.Dto
{
    public class AsignacionCreateDto
    {
        public int PersonaId { get; set; }
        public int ClienteId { get; set; }
        public int CargoId { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime FechaDesasignacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }

    public class AsignacionDto : AsignacionCreateDto
    {
        public int Id { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
