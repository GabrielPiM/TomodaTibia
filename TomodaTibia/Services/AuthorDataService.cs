using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomodaTibiaAPI.BLL;
using TomodaTibiaModels.Account.Request;
using TomodaTibiaModels.DB.Request;
using TomodaTibiaModels.DB.Response;
using TomodaTibiaModels.Utils;
using TomodaTibiaAPI.EntityFramework;

namespace TomodaTibiaAPI.Services
{

    public interface IAuthorDataService
    {
        //Task<Author> Get(Author author);
        Task<Response<string>> Add(AuthorRequest authorReq);
        Task<Response<string>> Update(AuthorRequest authorReq, string oldPassword);
        Task<Response<string>> Remove(AuthorRequest authorReq);
        Task<Response<AuthorResponse>> Author(int idAuthor);
    }

    public class AuthorDataService : IAuthorDataService
    {

        private readonly TomodaTibiaContext _db;
        private readonly IMapper _mapper;
        private readonly AuthorBLL _bll;
        private List<string> _errors;

        public AuthorDataService(TomodaTibiaContext db, IMapper mapper, AuthorBLL bll)
        {
            _db = db;
            _mapper = mapper;
            _bll = bll;
            _errors = new List<string>();
        }

        public async Task<Response<string>> Add(AuthorRequest authorReq)
        {
            var response = new Response<string>(string.Empty);
            var checkAuthor = _bll.CheckAuthor(authorReq);

            if (checkAuthor.Succeeded)
            {
                var author = _mapper.Map<EntityFramework.Author>(authorReq);

                if (!IsEmailResgistered(author.Email))
                {
                    try
                    {
                        author.IsBan = true;
                        author.IsAdmin = false;

                        await _db.Authors.AddAsync(author);
                        await _db.SaveChangesAsync();

                        response.Sucess($"Account Created ({author.Name}).", string.Empty);
                    }
                    catch
                    {
                        _errors.Add("Error when creating accout.");
                        response.Failed(_errors, StatusCodes.Status500InternalServerError);
                    }
                }
                else
                {
                    _errors.Add("This email is already being used.");
                    response.Failed(_errors, StatusCodes.Status400BadRequest);
                }
            }
            else
            {
                response.Failed(checkAuthor.Errors, checkAuthor.StatusCode);
            }

            return response;
        }

        public async Task<Response<string>> Remove(AuthorRequest authorReq)
        {
            var response = new Response<string>(string.Empty);
            var checkAuthor = _bll.CheckAuthorRemove(authorReq);

            if (checkAuthor.Succeeded)
            {
                try
                {
                    var authorToRemove = await _db.Authors.SingleAsync(a =>
                    a.Id == authorReq.Id
                    && a.Email == authorReq.Email
                    && a.Password == authorReq.Password);

                    if (authorToRemove != null)
                    {
                        using (var dbContextTransaction = _db.Database.BeginTransaction())
                        {
                            int adminId = 1;
                            var huntToChangeAuthor = await _db.Hunts.Where(h => h.IdAuthor == authorToRemove.Id).ToListAsync();

                            if (huntToChangeAuthor.Count > 0)
                                huntToChangeAuthor.ForEach(h => h.IdAuthor = adminId);

                            _db.Authors.Remove(authorToRemove);

                            await _db.SaveChangesAsync();
                            dbContextTransaction.Commit();

                            response.Sucess($"Author ({authorToRemove.Name}) was deleted.", string.Empty);
                        }
                    }
                    else
                    {
                        _errors.Add("Wrong email or password.");
                        response.Failed(_errors, StatusCodes.Status400BadRequest);
                    }
                }
                catch
                {
                    _errors.Add("Error when deleting author.");
                    response.Failed(_errors, StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                response.Failed(checkAuthor.Errors, checkAuthor.StatusCode);
            }

            return response;
        }

        public async Task<Response<string>> Update(AuthorRequest newAuthor, string oldPassword)
        {
            var response = new Response<string>(string.Empty);
            var checkAuthor = _bll.CheckAuthor(newAuthor);

            if (checkAuthor.Succeeded)
            {
                try
                {
                    var auhorToUpdate = await _db.Authors.SingleAsync(
                        a => a.Id == newAuthor.Id
                        && a.Password == oldPassword);

                    if (auhorToUpdate != null)
                    {

                        auhorToUpdate = _mapper.Map<EntityFramework.Author>(newAuthor);
                        _db.Authors.Update(auhorToUpdate);
                        await _db.SaveChangesAsync();

                        response.Sucess($"Author ({auhorToUpdate.Name}) has been updated.", string.Empty);
                    }
                    else
                    {
                        _errors.Add("Wrong password.");
                        response.Failed(_errors, StatusCodes.Status400BadRequest);
                    }
                }
                catch
                {
                    _errors.Add("Error updating author.");
                    response.Failed(_errors, StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                response.Failed(checkAuthor.Errors, checkAuthor.StatusCode);
            }

            return response;
        }

        public async Task<Response<AuthorResponse>> Author(int idAuthor)
        {
            var response = new Response<AuthorResponse>(new AuthorResponse());

            try
            {
                var author = await _db.Authors.SingleAsync(a => a.Id == idAuthor);

                if (author != null)
                {

                    response.Sucess("Author was found.", _mapper.Map<AuthorResponse>(author));
                }
                else
                {
                    _errors.Add("Author not found.");
                    response.Failed(_errors, StatusCodes.Status400BadRequest);
                }
            }
            catch
            {
                _errors.Add("Error getting author.");
                response.Failed(_errors, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        private bool IsEmailResgistered(string email)
        {
            return _db.Authors
                .Where(a => a.Email == email)
                .FirstOrDefault() == null ? false : true;
        }


    }
}
