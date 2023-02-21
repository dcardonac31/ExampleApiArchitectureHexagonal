using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonasMS.Domain.Dto
{
    public class ClienteCreateDto
    {
        public string? NombreCliente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }

    public class ClienteDto : ClienteCreateDto
    {
        public int Id { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
