
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using TomodaTibiaModels.Account.Request;
using TomodaTibiaModels.Utils;


namespace TomodaTibiaAPI.BLL
{
    public class AuthorBLL
    {
        private readonly BaseBLL _baseBll;

        public AuthorBLL(BaseBLL baseBll)
        {
            _baseBll = baseBll;
            PopulateDictionary();
        }

        public Response<string> CheckAuthor(AuthorRequest authorReq)
        {
            var response = new Response<string>(string.Empty);

            _baseBll.CheckName(authorReq.Name);
            CheckEmail(authorReq.Email);
            CheckPassword(authorReq.Password);
            _baseBll.CheckName(authorReq.NameMainChar);

            if (_baseBll.FoundErrors())
                response.Failed(_baseBll.GetErrors(), StatusCodes.Status400BadRequest);

            return response;
        }

        public Response<string> CheckAuthorRemove(AuthorRequest authorReq)
        {
            var response = new Response<string>(string.Empty);

            if (!_baseBll.CheckIsNull(authorReq))
            {
                CheckEmail(authorReq.Email);
                CheckPassword(authorReq.Password);
            }

            if (_baseBll.FoundErrors())
                response.Failed(_baseBll.GetErrors(), StatusCodes.Status400BadRequest);

            return response;
        }

        public void PopulateDictionary()
        {
            _baseBll.AddDicItem("password", "Password cannot be empty.");
            _baseBll.AddDicItem("email", "Invalid email format.");
        }

        private void CheckPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                _baseBll.SetError("password");
        }

        private void CheckEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
            }
            catch
            {
                _baseBll.SetError("email");
            }

        }

    }
}
