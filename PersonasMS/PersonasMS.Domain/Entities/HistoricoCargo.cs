using PersonasMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonasMS.Domain.Entities
{
    public class HistoricoCargo : BaseEntity
    {
        public int PersonaId { get; set; }
        public int CargoId { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime FechaDesasignacion { get; set; }
    }
}
