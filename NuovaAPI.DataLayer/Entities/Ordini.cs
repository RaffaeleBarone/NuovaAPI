using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.DataLayer.Entities
{
    public class Ordini
    {
        public Ordini()
        {
            ProdottiAcquistati = new List<Prodotto>();
        }
        public int Id { get; set; }
        public int CodiceOrdine { get; set; }
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<Prodotto> ProdottiAcquistati { get; set; }

    }
}
