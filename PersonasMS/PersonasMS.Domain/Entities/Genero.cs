using PersonasMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonasMS.Domain.Entities
{
    public class Genero : BaseEntity
    {
        public string? NombreGenero { get; set; }
    }
}
