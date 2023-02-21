using PersonasMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonasMS.Domain.Entities
{
    public class Departamento : BaseEntity
    {
        public string? CodigoEstadistico { get; set; }
        public int PaisId { get; set; }
        public string? NombreDepartamento { get; set; }
    }
}
