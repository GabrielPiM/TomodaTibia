using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomodaTibiaAPI.Services
{
    public  class JsonReturn : ControllerBase
    {
        public  ActionResult NotFoundJson(string _mens)
        {
            return NotFound(new
            {
                error = new
                {
                    mens = _mens
                }
            });
        }

        public ActionResult OkJson(Object _data)
        {
            return Ok(new
            {
                data = _data

            });
        }
    }
}
