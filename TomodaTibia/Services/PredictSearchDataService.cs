using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomodaTibiaAPI.EntityFramework;
using TomodaTibiaModels.Hunt.Response.AddHunt.PredictSearch;
using TomodaTibiaModels.Utils;

namespace TomodaTibiaAPI.Services
{

    interface IPredictSearchDataService
    {
        Task<Response<List<ImgObjPredictSearchResponse>>> SearchBook(string name);
        Task<Response<List<ImgObjPredictSearchResponse>>> SearchKey(string name);
        Task<Response<List<ImgObjPredictSearchResponse>>> SearchMount(string name);
        Task<Response<List<ImgObjPredictSearchResponse>>> SearchObject(string name);
        Task<Response<List<ImgObjPredictSearchResponse>>> SearchSpell(string name);
        Task<Response<List<TxtObjPredictSearchResponse>>> SearchQuest(string name);
        Task<Response<List<TxtObjPredictSearchResponse>>> SearchAchivement(string name);
        Task<Response<List<ImgObjPredictSearchResponse>>> SearchItem(string name);
        Task<Response<List<TxtObjPredictSearchResponse>>> SearchLocation(string name);
        Task<Response<List<TxtObjPredictSearchResponse>>> SearchHuntingPlace(string name);
        Task<Response<List<ImgObjPredictSearchResponse>>> SearchMonster(string name);

    }

    public class PredictSearchDataService : IPredictSearchDataService
    {

        private readonly TomodaTibiaContext _db;
        private const int MaxResultLenght = 10;
        //BLL
        private List<string> _errors;

        public PredictSearchDataService(TomodaTibiaContext db)
        {
            _db = db;
            _errors = new List<string>();
        }

        public async Task<Response<List<ImgObjPredictSearchResponse>>> SearchBook(string name)
        {

            var response = new Response<List<ImgObjPredictSearchResponse>>(new List<ImgObjPredictSearchResponse>());

            try
            {
                response.Data = await _db.Books.Where(b => b.Img.Contains(name)).Select(sb => new ImgObjPredictSearchResponse()
                {

                    Id = sb.Id,
                    Img = sb.Img

                }).Take(MaxResultLenght).ToListAsync();

            }
            catch
            {
                _errors.Add("Error connecting to database.");
                response.Failed(_errors, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<ImgObjPredictSearchResponse>>> SearchKey(string name)
        {

            var response = new Response<List<ImgObjPredictSearchResponse>>(new List<ImgObjPredictSearchResponse>());

            try
            {
                response.Data = await _db.Keys.Where(b => b.Img.Contains(name)).Select(sb => new ImgObjPredictSearchResponse()
                {

                    Id = sb.Id,
                    Img = sb.Img

                }).Take(MaxResultLenght).ToListAsync();

            }
            catch
            {
                _errors.Add("Error connecting to database.");
                response.Failed(_errors, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<ImgObjPredictSearchResponse>>> SearchMount(string name)
        {
            var response = new Response<List<ImgObjPredictSearchResponse>>(new List<ImgObjPredictSearchResponse>());

            try
            {
                response.Data = await _db.Mounts.Where(b => b.Img.Contains(name)).Select(sb => new ImgObjPredictSearchResponse()
                {

                    Id = sb.Id,
                    Img = sb.Img

                }).Take(MaxResultLenght).ToListAsync();

            }
            catch
            {
                _errors.Add("Error connecting to database.");
                response.Failed(_errors, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<ImgObjPredictSearchResponse>>> SearchObject(string name)
        {
            var response = new Response<List<ImgObjPredictSearchResponse>>(new List<ImgObjPredictSearchResponse>());

            try
            {
                response.Data = await _db.Objects.Where(b => b.Img.Contains(name)).Select(sb => new ImgObjPredictSearchResponse()
                {

                    Id = sb.Id,
                    Img = sb.Img

                }).Take(MaxResultLenght).ToListAsync();

            }
            catch
            {
                _errors.Add("Error connecting to database.");
                response.Failed(_errors, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<ImgObjPredictSearchResponse>>> SearchSpell(string name)
        {
            var response = new Response<List<ImgObjPredictSearchResponse>>(new List<ImgObjPredictSearchResponse>());

            try
            {
                response.Data = await _db.Spells.Where(b => b.Img.Contains(name)).Select(sb => new ImgObjPredictSearchResponse()
                {

                    Id = sb.Id,
                    Img = sb.Img

                }).Take(MaxResultLenght).ToListAsync();

            }
            catch
            {
                _errors.Add("Error connecting to database.");
                response.Failed(_errors, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<TxtObjPredictSearchResponse>>> SearchQuest(string name)
        {
            var response = new Response<List<TxtObjPredictSearchResponse>>(new List<TxtObjPredictSearchResponse>());

            try
            {
                response.Data = await _db.Quests.Where(b => b.Name.Contains(name)).Select(sb => new TxtObjPredictSearchResponse()
                {

                    Id = sb.Id,
                    Name = sb.Name

                }).Take(MaxResultLenght).ToListAsync();

            }
            catch
            {
                _errors.Add("Error connecting to database.");
                response.Failed(_errors, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<TxtObjPredictSearchResponse>>> SearchAchivement(string name)
        {
            var response = new Response<List<TxtObjPredictSearchResponse>>(new List<TxtObjPredictSearchResponse>());

            try
            {
                response.Data = await _db.Achivements.Where(b => b.Name.Contains(name)).Select(sb => new TxtObjPredictSearchResponse()
                {

                    Id = sb.Id,
                    Name = sb.Name

                }).Take(MaxResultLenght).ToListAsync();

            }
            catch
            {
                _errors.Add("Error connecting to database.");
                response.Failed(_errors, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<ImgObjPredictSearchResponse>>> SearchItem(string name)
        {
            var response = new Response<List<ImgObjPredictSearchResponse>>(new List<ImgObjPredictSearchResponse>());

            try
            {
                response.Data = await _db.Items.Where(b => b.Img.Contains(name)).Select(sb => new ImgObjPredictSearchResponse()
                {

                    Id = sb.Id,
                    Img = sb.Img

                }).Take(MaxResultLenght).ToListAsync();

            }
            catch
            {
                _errors.Add("Error connecting to database.");
                response.Failed(_errors, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<TxtObjPredictSearchResponse>>> SearchLocation(string name)
        {
            var response = new Response<List<TxtObjPredictSearchResponse>>(new List<TxtObjPredictSearchResponse>());

            try
            {
                response.Data = await _db.Locations.Where(b => b.Name.Contains(name)).Select(sb => new TxtObjPredictSearchResponse()
                {

                    Id = sb.Id,
                    Name = sb.Name

                }).Take(MaxResultLenght).ToListAsync();

            }
            catch
            {
                _errors.Add("Error connecting to database.");
                response.Failed(_errors, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<TxtObjPredictSearchResponse>>> SearchHuntingPlace(string name)
        {
            var response = new Response<List<TxtObjPredictSearchResponse>>(new List<TxtObjPredictSearchResponse>());

            try
            {
                response.Data = await _db.HuntingPlaces.Where(b => b.Name.Contains(name)).Select(sb => new TxtObjPredictSearchResponse()
                {

                    Id = sb.Id,
                    Name = sb.Name

                }).Take(MaxResultLenght).ToListAsync();

            }
            catch
            {
                _errors.Add("Error connecting to database.");
                response.Failed(_errors, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        public async Task<Response<List<ImgObjPredictSearchResponse>>> SearchMonster(string name)
        {
            var response = new Response<List<ImgObjPredictSearchResponse>>(new List<ImgObjPredictSearchResponse>());

            try
            {
                response.Data = await _db.Monsters.Where(b => b.Img.Contains(name)).Select(sb => new ImgObjPredictSearchResponse()
                {

                    Id = sb.Id,
                    Img = sb.Img

                }).Take(MaxResultLenght).ToListAsync();

            }
            catch
            {
                _errors.Add("Error connecting to database.");
                response.Failed(_errors, StatusCodes.Status500InternalServerError);
            }

            return response;
        }
    }
}
