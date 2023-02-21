using PersonasMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonasMS.Domain.Entities
{
    public class Municipio : BaseEntity
    {
        public string? CodigoEstadistico { get; set; }
        public int DepartamentoId { get; set; }
        public string? NombreMunicipio { get; set; }
    }
}
