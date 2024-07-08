using NuovaAPI.Commons.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.DataLayer.Manager
{
    public interface ITaxonomyManager
    {
        Task AddOrUpdateTaxonomy(List<TaxonomyDTO> taxonomies);
    }
}
