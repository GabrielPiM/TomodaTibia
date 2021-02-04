using EFDataAcessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomodaTibiaAPI.Services
{

    public interface IAuthorDataService
    {
        //Task<Author> Get(Author author);
        Task<Author> Add(Author author);
        Task Delete(Author author);
        Task<Author> Update(Author author);

    }

    public class AuthorDataService : IAuthorDataService
    {

        private readonly TomodaTibiaContext _db;
        public AuthorDataService(TomodaTibiaContext db)
        {
            _db = db;
        }

        public async Task<Author> Add(Author author)
        {
            if (!EmailExist(author.Email))
            {
                _db.Authors.Add(author);
                await _db.SaveChangesAsync();
                Author result = _db.Authors
                    .Where(p => p.Name == author.Name)
                    .Select(a => new Author { Name = a.Name })
                    .FirstOrDefault();
                return result;
            }
            return null;
        }
        private bool EmailExist(string email)
        {
            return _db.Authors
                .Where(a => a.Email == email)
                .FirstOrDefault() != null ? true : false;
        }

        public async Task Delete(Author author)
        {

            var removeAuthor = _db.Authors
                .Where(a => a.Email == author.Email
                 && a.Password == author.Password)
                .FirstOrDefault();

            if (removeAuthor != null)
            {
                _db.Authors.Remove(removeAuthor);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Author> Update(Author author)
        {

            var updateAuthor = _db.Authors.Where(a => a.Email == author.Email).SingleOrDefault();

            if(updateAuthor!=null)
            {
                updateAuthor = author;
                _db.Authors.Update(updateAuthor);
                await _db.SaveChangesAsync();
                return author;
            }

            return null;
        }

        //public Task<Author> Get(string email)
        //{
        //    return _db.Authors.Where(a => a.Email == email).SingleOrDefault();
        //}
    }
}
