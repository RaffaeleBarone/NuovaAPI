using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.Commons.DTO
{
    public class ClienteDTO
    {
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
        public DateTime? DataDiNascita { get; set; }
    }
}
