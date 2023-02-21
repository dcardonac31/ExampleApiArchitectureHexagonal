namespace PersonasMS.Domain.Dto
{
    public class CargoCreateDto
    {
        public string? NombreCargo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }

    public class CargoDto : CargoCreateDto
    {
        public int Id { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
