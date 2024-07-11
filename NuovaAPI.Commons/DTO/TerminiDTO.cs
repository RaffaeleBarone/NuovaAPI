using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.Commons.DTO
{
    public class TerminiDTO
    {
        public int Id { get; set; }
        
        //public LabelsDTO Labels { get; set; }
        public Dictionary<string, string> Labels { get; set; }
    }
}
