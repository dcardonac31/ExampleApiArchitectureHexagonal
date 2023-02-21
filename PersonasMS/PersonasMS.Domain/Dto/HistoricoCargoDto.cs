using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonasMS.Domain.Dto
{
    public class HistoricoCargoCreateDto
    {
        public int PersonaId { get; set; }
        public int CargoId { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime FechaDesasignacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }

    public class HistoricoCargoDto : HistoricoCargoCreateDto
    {
        public int Id { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
