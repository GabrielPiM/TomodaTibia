using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using TomodaTibiaModels.Utils;

using TomodaTibiaAPI.Utils;
using Microsoft.AspNetCore.Http;
using TomodaTibiaAPI.Utils.Pagination;
using TomodaTibiaModels.DB.Request;
using Microsoft.Extensions.Configuration;

namespace TomodaTibiaAPI.BLL
{
    public class HuntBLL
    {

        private readonly BaseBLL _baseBll;
        private List<string> _validHosts;

        public HuntBLL(BaseBLL basebll)
        {
            _baseBll = basebll;
            _validHosts = _baseBll.Configuration.GetSection("ValidHosts").Get<List<string>>();
            PopulateDictionary();
        }


        public Response<string> CheckCharName(string characterName)
        {
            var response = new Response<string>(string.Empty);

            _baseBll.CheckName(characterName);

            if (_baseBll.FoundErrors())
            {
                response.Sucess("", Formatting.ToPascalCase(characterName));
            }
            else
            {
                response.Failed(_baseBll.GetErrors(), StatusCodes.Status400BadRequest);
            }

            return response;
        }

        public Response<string> CheckParameters(SearchParameterRequest paramters)
        {
            var response = new Response<string>(string.Empty);

            if (!_baseBll.CheckIsNull(paramters))
            {
                if (paramters.PageSize < 1)
                    _baseBll.SetError("pagesize");


                foreach (var x in paramters.Vocation)
                {
                    if (x < 0 || x > 4)
                    {
                        _baseBll.SetError("vocation");
                        break;
                    }
                }

                foreach (var x in paramters.IdClientVersion)
                {
                    if (x < 0 || x > 3)
                    {
                        _baseBll.SetError("idclientversion");
                        break;
                    }
                }

                foreach (var x in paramters.Level)
                {
                    if (x < 1)
                    {
                        _baseBll.SetError("levelminreq");
                        break;
                    }
                }

                if (paramters.XpHr < 0)
                    _baseBll.SetError("xphr");

                foreach (var x in paramters.QtyPlayer)
                {
                    if (x < 1)
                    {
                        _baseBll.SetError("qtyplayer");
                        break;
                    }
                }

                if (paramters.Difficulty.Count < 2
                    || paramters.Difficulty.First() < 0
                    || paramters.Difficulty.Last() < 0
                    || paramters.Difficulty.First() > 10
                    || paramters.Difficulty.Last() > 10
                    || paramters.Difficulty.First() > paramters.Difficulty.Last())
                    _baseBll.SetError("difficulty");

                if (paramters.Rating < 0
                    || paramters.Rating > 5)
                    _baseBll.SetError("rating");

                if (paramters.IdLoot < 0)
                    _baseBll.SetError("idloot");
            }

            if (_baseBll.FoundErrors())
                response.Failed(_baseBll.GetErrors(), StatusCodes.Status400BadRequest);


            return response;
        }

        public Response<string> CheckHuntReq(HuntRequest huntReq)
        {
            var response = new Response<string>(string.Empty);

            if (!_baseBll.CheckIsNull(huntReq))
            {

                if (huntReq.Id < 0)
                {
                    _baseBll.SetError("idHunt");
                }

                _baseBll.CheckName(huntReq.Name);

                if (huntReq.LevelMinReq < 1)
                    _baseBll.SetError("levelminreq");

                if (huntReq.XpHr < 0)
                    _baseBll.SetError("xphr");

                if (huntReq.QtyPlayer < 1)
                    _baseBll.SetError("qtyplayer");

                CheckTutorialURL(huntReq.VideoTutorialUrl);

                //Descrição checar pessoalmente.

                if (huntReq.Difficulty < 0)
                    _baseBll.SetError("difficulty");

                if (huntReq.Rating < 0)
                    _baseBll.SetError("rating");

                if (huntReq.HuntImbuements.Count > 11)
                    _baseBll.SetError("imbuement");
                else
                {
                    int sumTotalImbuements = huntReq.HuntImbuements.ToArray().Select(x => x.Qty).ToArray().Sum();
                    if (sumTotalImbuements > 11)
                        _baseBll.SetError("imbuement");
                }

                if (huntReq.HuntPreys.Count > 3)
                    _baseBll.SetError("prey");

                if (huntReq.HuntItems.Count > 36)
                    _baseBll.SetError("item");

                if (huntReq.DescHunt.Length > 2048)
                    _baseBll.SetError("desc");

            }

            if (_baseBll.FoundErrors())
                response.Failed(_baseBll.GetErrors(), StatusCodes.Status400BadRequest);

            return response;
        }

        private void CheckTutorialURL(string url)
        {
            string host = "";
            try
            {
                host = new Uri(url).Host;
            }
            catch { }
            finally
            {
                if (url.Length > 1000 || !IsValidHost(host))
                    _baseBll.SetError("videotutorialurl");
            }
        }

        private bool IsValidHost(string videoTutorialURL)
        {
            bool isValid = false;


            foreach (var host in _validHosts)
            {
                if (host == videoTutorialURL)
                {
                    isValid = true;
                    break;
                }
            }

            return isValid;
        }

        private void PopulateDictionary()
        {
            _baseBll.AddDicItem("idhunt", "Ivalid hunt Id. (value >= 0).");
            _baseBll.AddDicItem("difficulty", "Diffculty[v1,v2] must be in range(0,10) and (v1 <= v2).");
            _baseBll.AddDicItem("levelminreq", "Level must be > 0.");
            _baseBll.AddDicItem("vocation", "Vocation ID must be i range(1,4). ");
            _baseBll.AddDicItem("idclientversion", "Invalid client version ID.");
            _baseBll.AddDicItem("qtyplayer", "QtyPlayer must be > 0.");
            _baseBll.AddDicItem("xphr", "XpHr must be >= 0.");
            _baseBll.AddDicItem("rating", "Rating must be in range(0,5).");
            _baseBll.AddDicItem("idloot", "Loot ID must be >= 0, (0 means no Loot).");
            _baseBll.AddDicItem("videotutorialurl", "Invalid tutorial video link. Accepted hosts: " +
               string.Join(", ", _validHosts.ToArray()) + ", (https:// is required).");
            _baseBll.AddDicItem("pagesize", "Page size must be > 0.");
            _baseBll.AddDicItem("imbuement", "Too many imbuements, max is 11.");
            _baseBll.AddDicItem("prey", "Too many preys, max is 3.");
            _baseBll.AddDicItem("item", "Too many items, max is 36.");
            _baseBll.AddDicItem("desc", "Description is to large, max 2048 characters.");
        }
    }
}
