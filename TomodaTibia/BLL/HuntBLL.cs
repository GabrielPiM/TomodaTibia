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
using TomodaTibiaModels.Utils.DBMaps;

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

                if (huntReq.TeamSize < 1)
                    _baseBll.SetError("teamSize");

                CheckTutorialURL(huntReq.TutorialVideoUrl);

                //Descrição checar pessoalmente.

                if (huntReq.Difficulty < 0)
                    _baseBll.SetError("difficulty");

                //CheckPlayer
                foreach (var player in huntReq.Players)
                {
                    //Level
                    if (player.Level < 0)
                        _baseBll.SetErrorWithExtraMsg("level", $"-->({player.Function})");

                    //Function
                    if (player.Function.Length > 100)
                        _baseBll.SetErrorWithExtraMsg("function", $"-->({player.Function})");

                    //Imbuement
                    if (player.PlayerImbuements.Count > 11)
                    {
                        _baseBll.SetErrorWithExtraMsg("imbuementCount", $"-->({player.Function})");
                    }

                    else
                    {
                        foreach (var playerImbue in player.PlayerImbuements)
                        {

                            if (playerImbue.Qty > 11)
                                _baseBll.SetErrorWithExtraMsg("imbuementQty", $"-->({player.Function})" +
                                    $"-->on {ImbuementMap.GetImbuement(playerImbue.IdImbuement)} imbuement.");
                        }
                    }

                    //Prey
                    if (player.PlayerPreys.Count > 3)
                    {
                        _baseBll.SetErrorWithExtraMsg("prey", $"-->({player.Function}");

                    }

                    //Item
                    if (player.PlayerItems.Count > 36)
                    {
                        _baseBll.SetErrorWithExtraMsg("item", $"-->({player.Function}");
                    }
                }

                //CheckDesc
                foreach (var desc in huntReq.HuntDescs)
                {

                    if (desc.Title.Length > 100)
                    {
                        _baseBll.SetErrorWithExtraMsg("descTitle", $"-->({desc.Title})");
                    }

                    if (desc.Paragraph.Length > 2048)
                    {
                        _baseBll.SetErrorWithExtraMsg("descParagraph", $"-->Paragraph Title:{desc.Title}");
                    }
                }
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
            _baseBll.AddDicItem("teamSize", "TeamSize must be > 0.");
            _baseBll.AddDicItem("xphr", "XpHr must be >= 0.");
            _baseBll.AddDicItem("idloot", "Loot ID must be >= 0, (0 means no Loot).");
            _baseBll.AddDicItem("videotutorialurl", "Invalid tutorial video link. Accepted hosts: " +
               string.Join(", ", _validHosts.ToArray()) + ", (https:// is required).");
            _baseBll.AddDicItem("pagesize", "Page size must be > 0.");
            _baseBll.AddDicItem("imbuementCount", "Too many imbuements, max is 11.");
            _baseBll.AddDicItem("imbuementQty", "Imbuement qty Sum Exceded max, max is 11.");
            _baseBll.AddDicItem("level", "min level is 1.");
            _baseBll.AddDicItem("function", "function name is to large. max is 100 characters.");
            _baseBll.AddDicItem("prey", "Too many preys, max is 3.");
            _baseBll.AddDicItem("item", "Too many items, max is 36.");
            _baseBll.AddDicItem("descParagraph", "Description is to large, max 2048 characters.");
            _baseBll.AddDicItem("descTitle", "Title is to large, max 100 characters.");
        }
    }
}
