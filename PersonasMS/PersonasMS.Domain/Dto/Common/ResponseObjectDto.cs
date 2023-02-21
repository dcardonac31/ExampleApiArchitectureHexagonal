using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PersonasMS.Domain.Dto.Common
{
    public class ResponseObjectDto<TEntity>
    {
        public bool Status { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
        public TEntity? Data { get; set; }

        public ResponseObjectDto()
        {
            Status= false;
            Data = default(TEntity);
            Message= string.Empty;
        }
    }
}
