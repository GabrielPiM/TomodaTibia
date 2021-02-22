using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TomodaTibiaAPI.BLL
{


    public class BaseBLL : IBLL
    {
        private List<string> _errors;
        private Dictionary<string, string> _errorsDic;

        public IConfiguration Configuration { get; }
        public BaseBLL(IConfiguration configuration)
        {
            Configuration = configuration;
            _errors = new List<string>();
            _errorsDic = new Dictionary<string, string>();
            PopulateDictionary();
        }

        public void CheckName(string name)
        {
            Regex rgx = new Regex(@"^[a-zA-Z\s]*$");  //Apenas letras e espaços permitidas no nome

            if (!rgx.IsMatch(name))
            {
                _errors.Add($"({name}) " + _errorsDic["regex"]);
            }

            if (name.Length > 30)
            {
                _errors.Add($"({name}) " + _errorsDic["namelenght"]);
            }
        }

        public bool CheckIsNull(Object obj)
        {
            if (obj == null)
            {
                SetError("nullobj");
                return true;
            }

            return false;
        }

        public void PopulateDictionary()
        {
            AddDicItem("nullobj", "The query object resulted in a null object. Check your json request for Datatype/Format errors.");
            AddDicItem("regex", "contains invalid letters. Please use only [A-Z,a-z] and space.");
            AddDicItem("namelenght", "is longer than 30 characters.");
        }

        public bool FoundErrors()
        {
            return _errors.Count > 0;
        }

        public void SetError(string key)
        {
            _errors.Add(_errorsDic[key]);
        }

        public void AddDicItem(string key, string mens)
        {
            _errorsDic.Add(key, mens);
        }

        public List<string> GetErrors()
        {
            return _errors;
        }

    }
}
