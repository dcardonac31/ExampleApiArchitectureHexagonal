using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonasMS.Domain.Dto
{
    public class DepartamentoCreateDto
    {
        public string? CodigoEstadistico { get; set; }
        public int PaisId { get; set; }
        public string? NombreDepartamento { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }

    public class DepartamentoDto : DepartamentoCreateDto
    {
        public int Id { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
