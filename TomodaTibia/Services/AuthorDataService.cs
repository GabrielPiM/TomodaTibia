using AutoMapper;
using EFDataAcessLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomodaTibiaAPI.Utils;
using TomodaTibiaModels.DB.Request;
using TomodaTibiaModels.DB.Response;

namespace TomodaTibiaAPI.Services
{

    public interface IAuthorDataService
    {
        //Task<Author> Get(Author author);
        Task<Response<string>> Add(AuthorRequest authorReq);
        Task<Response<string>> Update(AuthorRequest authorReq);
        Task<Response<string>> Remove(AuthorRequest senha);
        Task<Response<AuthorResponse>> Author(int idAuthor);

    }

    public class AuthorDataService : IAuthorDataService
    {

        private readonly TomodaTibiaContext _db;
        private readonly IMapper _mapper;

        public AuthorDataService(TomodaTibiaContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Response<string>> Add(AuthorRequest authorReq)
        {
            var response = new Response<string>(string.Empty);

            var author = _mapper.Map<Author>(authorReq);

            if (!EmailExist(author.Email))
            {
                try
                {
                    await _db.Authors.AddAsync(author);
                    await _db.SaveChangesAsync();
                    response.Message = "Account Created.";
                }
                catch
                {
                    response.Message = "Error when creating accout.";
                    response.Succeeded = false;
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                }
            }
            else
            {
                response.Message = "This email is already being used.";
                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status400BadRequest;
            }

            return response;
        }

        public async Task<Response<string>> Remove(AuthorRequest authorReq)
        {
            var response = new Response<string>(string.Empty);

            try
            {
                var authorToRemove = await _db.Authors.SingleAsync(a =>
                a.Id == authorReq.Id
                && a.Email == authorReq.Email
                && a.Password == authorReq.Password);

                if (authorToRemove == null)
                {
                    response.Message = "Wrong email or password.";
                    response.Succeeded = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                }
                else
                {
                    try
                    {
                        _db.Authors.Remove(authorToRemove);
                        await _db.SaveChangesAsync();
                    }
                    catch
                    {
                        response.Message = "Error when deleting author. ";
                        response.Succeeded = false;
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                    }
                }
            }
            catch
            {
                response.Message = "Error when deleting author. ";
                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return response;
        }

        public async Task<Response<string>> Update(AuthorRequest author)
        {
            var response = new Response<string>(string.Empty);

            try
            {
                var auhorToUpdate = await _db.Authors.SingleAsync(
                    a => a.Id == author.Id
                 && a.Email == author.Email
                 && a.Password == author.Password);

                if (auhorToUpdate != null)
                {
                    try
                    {
                        auhorToUpdate = _mapper.Map<Author>(author);
                        _db.Authors.Update(auhorToUpdate);                       
                        await _db.SaveChangesAsync();
                    }
                    catch
                    {
                        response.Message = "Error updating author.";
                        response.Succeeded = false;
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                    }
                }
            }
            catch
            {
                response.Message = "Author not found.";
                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status400BadRequest;
            }

            return response;
        }

        public async Task<Response<AuthorResponse>> Author(int idAuthor)
        {
            var response = new Response<AuthorResponse>(null);

            try
            {
                var author = await _db.Authors.SingleAsync(a => a.Id == idAuthor);

                if (author != null)
                {
                    response.Data = _mapper.Map<AuthorResponse>(author);
                }
                else
                {
                    response.Message = "Author not found.";
                    response.Succeeded = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                }
            }
            catch
            {
                response.Message = "Error getting author.";
                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return response;
        }

        private bool EmailExist(string email)
        {
            return _db.Authors
                .Where(a => a.Email == email)
                .FirstOrDefault() != null ? true : false;
        }

      
    }
}
