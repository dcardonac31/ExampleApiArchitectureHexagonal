namespace PersonasMS.Domain.Dto
{
    public class GeneroCreateDto
    {
        public string? NombreGenero { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }

    public class GeneroDto : CargoCreateDto
    {
        public int Id { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
