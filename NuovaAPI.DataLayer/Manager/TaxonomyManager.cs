using Microsoft.EntityFrameworkCore;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Infrastructure;

namespace NuovaAPI.DataLayer.Manager
{
    public class TaxonomyManager : ITaxonomyManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public TaxonomyManager(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        //public async Task AddOrUpdateTaxonomy(List<TaxonomyDTO> taxonomies)
        //{
        //    using var transaction = _unitOfWork.BeginTransaction();

        //    try
        //    {
        //        var nomiTaxonomies = taxonomies.Select(t => t.Nome).ToList();
        //        var taxonomiesEsistenti = await _unitOfWork.TaxonomyRepository
        //            .Get(t => nomiTaxonomies.Contains(t.Nome))
        //            .Include(t => t.Termini)
        //            .ToListAsync();

        //        var listaTaxonomies = new List<Taxonomy>();
        //        var listaTermini = new List<Termini>();

        //        foreach (var taxonomyDto in taxonomies)
        //        {
        //            var taxonomyEsistente = taxonomiesEsistenti.FirstOrDefault(t => t.Nome == taxonomyDto.Nome);

        //            if (taxonomyEsistente == null)
        //            {
        //                taxonomyEsistente = new Taxonomy
        //                {
        //                    Nome = taxonomyDto.Nome
        //                };
        //                listaTaxonomies.Add(taxonomyEsistente);
        //            }

        //            foreach (var termineDto in taxonomyDto.Termini)
        //            {
        //                var labels = termineDto.Labels;

        //                var enUSTermini = taxonomyEsistente.Termini.FirstOrDefault(t => t.Lingua == "en_US" && t.Traduzione == labels.en_US);
        //                var frFRTermini = taxonomyEsistente.Termini.FirstOrDefault(t => t.Lingua == "fr_FR" && t.Traduzione == labels.fr_FR);
        //                var itITTermini = taxonomyEsistente.Termini.FirstOrDefault(t => t.Lingua == "it_IT" && t.Traduzione == labels.it_IT);

        //                if (enUSTermini == null)
        //                {
        //                    listaTermini.Add(new Termini
        //                    {
        //                        Lingua = "en_US",
        //                        Traduzione = labels.en_US,
        //                        IsDefault = true,
        //                        TaxonomyId = taxonomyEsistente.Id
        //                    });
        //                }
        //                else
        //                {
        //                    enUSTermini.Traduzione = labels.en_US;
        //                    listaTermini.Add(enUSTermini);
        //                }

        //                if (frFRTermini == null)
        //                {
        //                    listaTermini.Add(new Termini
        //                    {
        //                        Lingua = "fr_FR",
        //                        Traduzione = labels.fr_FR,
        //                        IsDefault = false,
        //                        TaxonomyId = taxonomyEsistente.Id
        //                    });
        //                }
        //                else
        //                {
        //                    frFRTermini.Traduzione = labels.fr_FR;
        //                    listaTermini.Add(frFRTermini);
        //                }

        //                if (itITTermini == null)
        //                {
        //                    listaTermini.Add(new Termini
        //                    {
        //                        Lingua = "it_IT",
        //                        Traduzione = labels.it_IT,
        //                        IsDefault = false,
        //                        TaxonomyId = taxonomyEsistente.Id
        //                    });
        //                }
        //                else
        //                {
        //                    itITTermini.Traduzione = labels.it_IT;
        //                    listaTermini.Add(itITTermini);
        //                }
        //            }
        //        }

        //        if (listaTaxonomies.Count > 0)
        //        {
        //            await _unitOfWork.TaxonomyRepository.BulkMergeAsync(listaTaxonomies);
        //        }

        //        if (listaTermini.Count > 0)
        //        {
        //            await _unitOfWork.TerminiRepository.BulkMergeAsync(listaTermini);
        //        }

        //        _unitOfWork.Save();
        //        transaction.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        transaction.Rollback();
        //        var innerException = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
        //        throw new ApplicationException($"Errore durante l'aggiornamento dei dati: {ex.Message}. Dettagli: {innerException}");
        //    }
        //}
        public async Task AddOrUpdateTaxonomy(List<TaxonomyDTO> taxonomies)
        {
            using var transaction = _unitOfWork.BeginTransaction();

            try
            {
                var nomiTaxonomies = taxonomies?.Select(t => t.Nome).ToList() ?? new List<string>();
                var taxonomiesEsistenti = await _unitOfWork.TaxonomyRepository
                    .Get(t => nomiTaxonomies.Contains(t.Nome))
                    .Include(t => t.Termini)
                    .ToListAsync();

                var listaTaxonomies = new List<Taxonomy>();
                var listaTermini = new List<Termini>();

                foreach (var taxonomyDto in taxonomies ?? new List<TaxonomyDTO>())
                {
                    var taxonomyEsistente = taxonomiesEsistenti.FirstOrDefault(t => t.Nome == taxonomyDto.Nome);

                    if (taxonomyEsistente == null)
                    {
                        taxonomyEsistente = new Taxonomy
                        {
                            Nome = taxonomyDto.Nome,
                            Termini = new List<Termini>()
                        };
                        listaTaxonomies.Add(taxonomyEsistente);
                    }

                    foreach (var termineDto in taxonomyDto.Termini ?? new List<TerminiDTO>())
                    {
                        foreach (var label in termineDto.Labels ?? new Dictionary<string, string>())
                        {
                            var lingua = label.Key;
                            var traduzione = label.Value;

                            var termineEsistente = taxonomyEsistente.Termini
                                .FirstOrDefault(t => t.Id == termineDto.Id && t.Lingua == lingua);

                            if (termineEsistente == null)
                            {
                                listaTermini.Add(new Termini
                                {
                                    Id = termineDto.Id,
                                    Lingua = lingua,
                                    Traduzione = traduzione,
                                    IsDefault = lingua == "en_US",
                                    TaxonomyId = taxonomyEsistente.Id
                                });
                            }
                            else
                            {
                                termineEsistente.Traduzione = traduzione;
                                termineEsistente.IsDefault = lingua == "en_US";
                                listaTermini.Add(termineEsistente);
                            }
                        }
                    }
                }

                // Salva le entità Taxonomy prima di aggiungere le entità Termini
                if (listaTaxonomies.Count > 0)
                {
                    await _unitOfWork.TaxonomyRepository.BulkMergeAsync(listaTaxonomies);
                    await _unitOfWork.SaveAsync();
                }

                if (listaTermini.Count > 0)
                {
                    var uniqueTermini = listaTermini
                        .GroupBy(t => new { t.Id, t.Lingua, t.TaxonomyId })
                        .Select(g => g.First())
                        .ToList();

                    await _unitOfWork.TerminiRepository.BulkMergeAsync(uniqueTermini);
                }

                _unitOfWork.Save();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                var innerException = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
                throw new ApplicationException($"Errore durante l'aggiornamento dei dati: {ex.Message}. Dettagli: {innerException}");
            }
        }


    }
}
