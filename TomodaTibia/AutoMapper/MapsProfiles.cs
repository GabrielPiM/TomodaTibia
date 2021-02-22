using AutoMapper;
using EFDataAcessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomodaTibiaModels.DB.Request;
using TomodaTibiaModels.DB.Response;

namespace TomodaTibiaAPI.Maps
{
    public class MapsProfiles : Profile
    {
        public MapsProfiles()
        {
            //Requests to Entitys
            CreateMap<HuntRequest, Hunt>();
            CreateMap<HuntClientVersionRequest, HuntClientVersion>();
            CreateMap<HuntImbuementRequest, HuntImbuement>();
            CreateMap<HuntPreyRequest, HuntPrey>();
            CreateMap<HuntItemRequest, HuntItem>();
            CreateMap<HuntMonsterRequest, HuntMonster>();
            CreateMap<PlayerRequest, Player>();
            CreateMap<EquipamentRequest, Equipament>();
            CreateMap<AuthorRequest, Author>();
            CreateMap<HuntLootRequest, HuntLoot>();

            ////Entity to request  
            CreateMap<HuntRequest, Hunt>().ReverseMap();
            CreateMap<HuntClientVersionRequest, HuntClientVersion>().ReverseMap();
            CreateMap<HuntImbuementRequest, HuntImbuement>().ReverseMap();
            CreateMap<HuntPreyRequest, HuntPrey>().ReverseMap();
            CreateMap<HuntItemRequest, HuntItem>().ReverseMap();
            CreateMap<HuntMonsterRequest, HuntMonster>().ReverseMap();
            CreateMap<PlayerRequest, Player>().ReverseMap();
            CreateMap<EquipamentRequest, Equipament>().ReverseMap();
            CreateMap<HuntLootRequest, HuntLoot>().ReverseMap();

            ////Entity to Responses
            CreateMap<Author, AuthorResponse>();
            CreateMap<Hunt, HuntResponse>();
            CreateMap<Equipament, EquipamentResponse>();

        }
    }
}