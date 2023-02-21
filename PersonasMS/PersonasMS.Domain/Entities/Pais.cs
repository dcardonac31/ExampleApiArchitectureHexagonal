using PersonasMS.Domain.Entities.Base;

namespace PersonasMS.Domain.Entities
{
    public class Pais : BaseEntity
    {
        public string? CodigoDIAN { get; set; }
        public string? NombrePais { get; set; }
    }
}
