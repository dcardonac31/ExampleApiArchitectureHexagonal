namespace PersonasMS.Domain.Dto
{
    public class PaisCreateDto
    {
        public string? CodigoDIAN { get; set; }
        public string? NombrePais { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }

    public class PaisDto : PaisCreateDto
    {
        public int Id { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
