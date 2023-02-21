namespace PersonasMS.Domain.Dto
{
    public class MunicipioCreateDto
    {
        public string? CodigoEstadistico { get; set; }
        public int DepartamentoId { get; set; }
        public string? NombreMunicipio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }

    public class MunicipioDto : MunicipioCreateDto
    {
        public int Id { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
