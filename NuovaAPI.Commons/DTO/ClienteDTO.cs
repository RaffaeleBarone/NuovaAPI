﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NuovaAPI.Commons.DTO
{
    public class ClienteDTO
    {
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
        //[JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? DataDiNascita { get; set; }
        public IEnumerable<OrdiniDTO>? Ordini { get; set; }
    }
}
