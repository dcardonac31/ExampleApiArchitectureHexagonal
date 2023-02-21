namespace PersonasMS.Domain.Dto
{
    public class IngresoRetiroCreateDto
    {
        public int PersonaId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaRetiro { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }

    public class IngresoRetiroDto : IngresoRetiroCreateDto
    {
        public int Id { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
