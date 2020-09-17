using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomodaTibiaAPI.DBContext;
using TomodaTibiaModels.Hunt;
using TomodaTibiaModels.DB;
using TomodaTibiaModels.Character;
using Microsoft.EntityFrameworkCore;

namespace TomodaTibiaAPI.Services
{
    public interface ITomodaTibiaDataService
    {
        Task<List<HuntCardModel>> Get(string charName);
        //Task<PaginatedList<ToDo>> GetList(int? pageNumber, string sortField, string sortOrder);
        Task<HuntDetailModel> Get(int id);
        Task<Hunt> Add(Hunt hunt);
        Task<Hunt> Update(Hunt hunt);
        Task<Hunt> Delete(int id);

    }
    public class HuntDataService /*:ITomodaTibiaDataService*/
    {
        private readonly TomodaTibiaContext db;
        private readonly TibiaApiService api;

        public HuntDataService(TomodaTibiaContext _db, TibiaApiService api)
        {
            this.db = _db;
            this.api = api;
        }

        public async Task<dynamic> Get(string charName)
        {
            //Consulta a API para obter as informacoes do personagem
            var result = await api.getCharInfo(charName);

            //Retorna se não encontrar o personagem
            if (result.characters.error != null) return null;

            //Dados do Personagem
            var character = new CharacterModel
             (
              result.characters.data.name,
              result.characters.data.level,
              result.characters.data.vocation,
              result.characters.data.sex,
              result.characters.data.account_status
             );

            //Identifica os ids das hunts validas
            var huntsValidas = db.Player
                .Where(p => (db.Vocacao.Where(v => v.NomeVocacao == character.vocacao).Select(v => v.Id)
                .FirstOrDefault()) == p.Vocacao)
                .Select(p => p.IdHunt)
                .Distinct();

            //Filtra as hunts por nível e vocação e gera uma coleção de cards
            var hunts = db.Hunt
                .Where(h => huntsValidas
                .Contains(h.Id) && character.level >= h.NivelMinReq)
                .Select(h => new HuntCardModel
                {
                    id = h.Id,
                    nome = h.Nome,
                    nivelMinRec = h.NivelMinReq,
                    urlGif = db.Monstro
                        .Where(m => m.Id == h.Id)
                        .Select(m => m.Nome),
                    autor = db.Autor
                      .Where(a => a.Id == h.Id)
                      .FirstOrDefault()
                });

            return new { character, hunts };
        }

        public async Task<HuntDetailModel> GetDetail(int id)
        {
            //Consulta informacoes da Hunt 
            var hunt = await db.Hunt
                .Where(h => h.Id == id)
                .Select(h => new
                {
                    h.VideoTutorial,
                    h.DescHunt,
                    h.Player
                }).FirstOrDefaultAsync();

            //Return caso não encontrada
            if (hunt == null) return null;

            //Consulta equipamentos da hunt
            var equips = await db.Equipamento
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            //Consulta outros Items da hunt
            var items = await db.OutroItem
                .Where(i => i.IdHunt == id)
                .Select(i => new ItemModel
                {
                    idItem = i.IdItem,
                    nome = i.IdItemNavigation.Nome,
                    qtd = i.Qtd

                }).ToListAsync();

            //Retorna o HuntDetail
            return new HuntDetailModel
            {
                equipamento = equips,
                item = items,
                videoTutorial = hunt.VideoTutorial,
                descHunt = hunt.DescHunt,
                player = hunt.Player

            };
        }

        public async Task<bool> add(Hunt hunt)
        {
            try
            {
                db.Hunt.Add(hunt);
                
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }           
        }

        public async Task<bool> AddAutor(Autor autor)
        {
            try
            {
                db.Autor.Add(autor);
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
