﻿using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;

namespace NuovaAPI.Worker_Services
{
    public interface IVetrinaWorkerService
    {
        Vetrina MapToVetrina(VetrinaDTO vetrinaDTO);
        Task AddVetrina(VetrinaDTO vetrinaDTO);
        Task<IEnumerable<VetrinaDTO>> GetVetrina(int? codiceVetrina = null);
        Task<Vetrina> GetVetrinaId(int id);
        Task<Vetrina> PutVetrina(int id, VetrinaDTO vetrinaDTO);
        Task DeleteVetrina(int id);
    }
}
