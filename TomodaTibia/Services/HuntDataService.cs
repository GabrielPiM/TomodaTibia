using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TomodaTibiaModels.DB.Response;
using Microsoft.EntityFrameworkCore;
using TomodaTibiaModels.Hunt;
using EFDataAcessLibrary.Models;
using TomodaTibiaModels.Character;


namespace TomodaTibiaAPI.Services
{
    public interface IHuntDataService
    {

        //Task<PaginatedList<ToDo>> GetList(int? pageNumber, string sortField, string sortOrder);
        Task<dynamic> Get(string charName);
        Task<HuntResponse> GetDetail(int HuntId);
        Task<Hunt> Add(Hunt hunt);
        Task<Hunt> Update(Hunt hunt);
        Task<Hunt> Delete(int id);

    }
    public class HuntDataService : IHuntDataService
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


            //Identifica o ID da vocacao do Player
            var idVoc = db.Vocations
                .Where(v => v.Name == character.vocacao)
                .Select(v => v.Id)
                .FirstOrDefault();

            //Identifica os ids das hunts validas
            var huntsValidas = db.Players
                .Where(p => p.Id == idVoc)
                .Select(p => p.Id)
                .Distinct();

            //Filtra as hunts por nível e vocação e gera uma coleção de cards
            var hunts = db.Hunts
                .Where(h => huntsValidas
                .Contains(h.Id) && character.level >= h.NivelMinReq)
                .Select(h => new HuntCardModel
                {
                    Id = h.Id,
                    Nome = h.Name,
                    NivelMinRec = h.NivelMinReq,
                    URLGif = db.Monsters
                        .Where(m => m.Id == h.Id)
                        .Select(m => m.Img)

                });

            return new { character, hunts };

        }

        public async Task<HuntResponse> GetDetail(int HuntId)
        {
            //Consulta informacoes da Hunt 
            var hunt = await db.Hunts
                .Where(h => h.Id == HuntId)
                .Select(h => new HuntResponse
                {
                    Name = h.Name,
                    NivelMinReq = h.NivelMinReq,
                    XpHr = h.XpHr,
                    QtyPlayer = h.QtyPlayer,
                    VideoTutorialUrl = h.VideoTutorialUrl,
                    DescHunt = h.DescHunt,
                    Difficulty = h.Difficulty,
                    Rating = h.Rating,
                    IsPremium = h.IsPremium

                }).FirstOrDefaultAsync();

            //Return caso não encontrada
            if (hunt == null) return null;

            hunt.Players = await db.Players.Where(p => p.IdHunt == HuntId)
                .Select(p => new PlayerResponse
                {
                    Vocation = p.Vocation,
                    Level = p.Level,
                    Equipaments = db.Equipaments.Where(e => e.Id == p.Id)
                    .Select(e => new EquipamentResponse
                    {
                        Amulet = e.Amulet,
                        Bag = e.Bag,
                        Helmet = e.Helmet,
                        Armor = e.Armor,
                        WeaponRight = e.WeaponRight,
                        WeaponLeft = e.WeaponLeft,
                        Ring = e.Ring,
                        Legs = e.Legs,
                        Boots = e.Boots,
                        Ammo = e.Ammo

                    }).FirstOrDefault()

                }).ToListAsync();

            //Consulta outros Items da hunt
            hunt.OtherItems = await db.HuntItems
                .Where(i => i.IdHunt == HuntId)
                .Select(i => new ItemResponse
                {
                    Img = i.IdItemNavigation.Img,
                    Qty = i.Qty

                }).ToListAsync();

            //Preys desta hunt.
            hunt.Preys = db.HuntPreys.Where(h => h.IdHunt == HuntId)
                .Select(p => new PreyResponse
                {
                    Img = p.IdMonsterNavigation.Img,
                    ReccStars = p.ReccStar,
                    Monster = new MonsterResponse
                    {
                        Img = p.IdMonsterNavigation.Img
                    }
                }).ToList();

            //Imbuements desta hunt.
            hunt.Imbuements = db.HuntImbuements.Where(hi => hi.IdHunt == HuntId)
                .Select(h => new ImbuementResponse
                {
                    Category = h.IdImbuementNavigation.Category,

                    Value = db.ImbuementDescs
                    .Where(desc => desc.IdImbuementLevel == h.IdImbuementLevel
                    && desc.IdImbuementType == h.IdImbuementType)
                    .Select(desc2 => desc2.Value)
                    .FirstOrDefault(),

                    Desc = h.IdImbuementNavigation.Desc,
                    Level = h.IdImbuementLevelNavigation.Name,
                    Qty = h.Qty,
                    Img = h.IdImbuementNavigation.Img,

                    Items = db.ImbuementItems
                    .Where(i => i.IdImbuement == h.IdImbuementNavigation.Id
                      && i.IdImbuementLevel <= h.IdImbuementLevel)
                    .Select(it => new ItemResponse
                    {
                        Img = it.IdItemNavigation.Img,
                        Qty = it.Qty
                    }).ToList()

                }).ToList();


            //Versões do client dessa hunt
            hunt.Versions = db.HuntClientVersions.Where(h => h.Id == HuntId)
                .Select(f => f.IdClientVersionNavigation.VersionName)
                .ToList();

            //Retorna o HuntDetail
            return hunt;
        }

        public async Task<Hunt> Add(Hunt hunt)
        {
            db.AddRange(hunt);
            await db.SaveChangesAsync();
            return hunt;
        }

        public Task<Hunt> Update(Hunt hunt)
        {
            throw new NotImplementedException();
        }

        public Task<Hunt> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
