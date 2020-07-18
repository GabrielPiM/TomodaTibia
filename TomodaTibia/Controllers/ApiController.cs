using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TomodaTibiaAPI.DB;
using Microsoft.AspNetCore.Http;
using TomodaTibiaAPI.Services;
using System.Net.Http;

namespace TomodaTibiaAPI.Controllers
{

    public class ApiController : Controller
    {
        private readonly TomodaTibiaContext db;
        private readonly TibiaApiService _api;
  

        public ApiController(TomodaTibiaContext context, TibiaApiService api)
        {
            db = context;
            _api = api;
        }

       

        public IActionResult Index(string _a)
        {
            //teste consulta api
            var result = _api.getNivel("neon%20dandar");
            //deste consulta banco de dados
            var locais = ( from u in db.Local select u );
           
            return Ok(locais);
        }



    }
}