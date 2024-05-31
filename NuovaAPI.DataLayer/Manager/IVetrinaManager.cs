﻿using NuovaAPI.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuovaAPI.DataLayer.Manager
{
    public interface IVetrinaManager
    {
        Task AddVetrina(int id);
        Task<ICollection<Vetrina>> GetVetrine();
        Task<Vetrina> GetIdVetrina(int id);
        Task RemoveVetrina(int id);
    }
}