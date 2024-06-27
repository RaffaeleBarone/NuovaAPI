using Microsoft.EntityFrameworkCore;
using NuovaAPI.Commons.DTO;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Infrastructure;

namespace NuovaAPI.DataLayer.Manager
{
    public class ProdottoManager : IProdottoManager
    {

        private readonly IUnitOfWork _unitOfWork;
        public ProdottoManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddProdotto(Prodotto prodotto)
        {
            //var prodotto = new Prodotto { NomeProdotto = nome, Prezzo = prezzo, IdVetrina = idVetrina };
            _unitOfWork.ProdottoRepository.Add(prodotto);
            _unitOfWork.Save();
        }

        public async Task RemoveProdotto(int id)
        {
            var prodottoDaRimuovere = await _unitOfWork.ProdottoRepository.GetById(id);
            if (prodottoDaRimuovere != null)
            {
                _unitOfWork.ProdottoRepository.Delete(prodottoDaRimuovere);
                _unitOfWork.Save();
            }
        }

        public async Task<IEnumerable<ProdottoDTO>> GetProdotti(string nomeProdotto = null, string orderBy = null, bool ascending = true)
        {
            //try
            //{
            //    return _unitOfWork.ProdottoRepository.Get(null);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception($"Errore durante il recupero dei prodotti dal database: {ex.Message}");
            //}

            var query = _unitOfWork.ProdottoRepository.Get(null)
            .AsQueryable();

            //Filtri
            if (!string.IsNullOrEmpty(nomeProdotto))
            {
                query = query.Where(c => c.NomeProdotto.Contains(nomeProdotto));
            }

            // Ordinamento
            if (!string.IsNullOrEmpty(orderBy))
            {
                switch (orderBy.ToLower())
                {
                    case "nome":
                        query = ascending ? query.OrderBy(c => c.NomeProdotto) : query.OrderByDescending(c => c.NomeProdotto);
                        break;
                    case "prezzo":
                        query = ascending ? query.OrderBy(p => p.Prezzo) : query.OrderByDescending(p => p.Prezzo);
                        break;
                    case "quantita":
                        query = ascending ? query.OrderBy(p => p.QuantitaDisponibile) : query.OrderByDescending(p => p.QuantitaDisponibile);
                        break;
                    case "idvetrina":
                        query = ascending ? query.OrderBy(p => p.IdVetrina) : query.OrderByDescending(p => p.IdVetrina);
                        break;
                    default:
                        query = query.OrderBy(p => p.NomeProdotto); 
                        break;
                }
            }

            var prodotto = await query.ToListAsync();

            var prodottoDTO = prodotto.Select(x => new ProdottoDTO
            {
                NomeProdotto = x.NomeProdotto,
                Prezzo = x.Prezzo,
                Quantita = x.QuantitaDisponibile,
                IdVetrina = x.IdVetrina
            });

            return prodottoDTO;
        }

        public async Task<Prodotto> GetIdProdotto(int id)
        {
            try
            {
                //return _unitOfWork.ProdottoRepository
                //    .Where(x => x.Id == id)
                //    .SingleOrDefault();
                var prodotto = await _unitOfWork.ProdottoRepository.GetById(id);
                return prodotto;
            }

            catch (Exception ex)
            {
                throw new Exception($"Errore durante il recupero del prodotto dal database: {ex.Message}");
            }
        }

        public async Task<Prodotto> ModificaProdotto(int id, ProdottoDTO prodottoDTO)
        {
            var prodottoDaModificare = await _unitOfWork.ProdottoRepository.GetById(id);

            if (prodottoDaModificare == null)
            {
                throw new Exception("Prodotto non trovato");
            }

            if (prodottoDTO.NomeProdotto != null)
            {
                prodottoDaModificare.NomeProdotto = prodottoDTO.NomeProdotto;
            }
            if (prodottoDTO.Prezzo != null)
            {
                prodottoDaModificare.Prezzo = (float)prodottoDTO.Prezzo;
            }
            if (prodottoDTO.Quantita != null)
            {
                prodottoDaModificare.QuantitaDisponibile = (int)prodottoDTO.Quantita;
            }
            if (prodottoDTO.IdVetrina != null)
            {
                prodottoDaModificare.IdVetrina = prodottoDTO.IdVetrina;
            }

            _unitOfWork.Save();
            return prodottoDaModificare;
        }
    }
}
