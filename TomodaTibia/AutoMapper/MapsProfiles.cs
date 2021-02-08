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
            CreateMap<HuntRequest, Hunt>()
                .ForMember(dest => dest.HuntClientVersions, opt => opt
                    .MapFrom(src => src.HuntClientVersions.Select(hcv => new HuntClientVersion()
                        { 
                            IdClientVersion=hcv.IdClientVersion
                        })));

            CreateMap<HuntClientVersionRequest, HuntClientVersion>();
            CreateMap<HuntImbuementRequest, HuntImbuement>();
            CreateMap<HuntPreyRequest, HuntPrey>();
            CreateMap<HuntItemRequest, HuntItem>();
            CreateMap<PlayerRequest, Player>();
            CreateMap<EquipamentRequest, Equipament>();

            CreateMap<Author, AuthorResponse>();
            CreateMap<AuthorRequest, Author>();
       


            //CreateMap<AuthorRequest, Author>();
            //CreateMap<AuthorResponse, Author>();
        }
    }
}