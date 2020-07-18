using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TomodaTibiaAPI.Services
{

    public interface ITApiservice
    {

        dynamic getNivel(string _nome);
    }

    public class TibiaApiService : ITApiservice
    {
        private readonly HttpClient _client;
 

        public TibiaApiService(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json; charset=utf-8");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "TomodaTibia");
            _client = httpClient;
        }
        public dynamic getNivel(string _nome)
        {
            var res = _client.GetAsync( _nome + ".json").Result;
            res.EnsureSuccessStatusCode();
            string stringData = res.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject(stringData);

            return data;
        }



    }


    //public async Task<string> getNivel2(String _nome)
    //{
    //    var response = await Client.GetAsync($"{_nome}.json");

    //    response.EnsureSuccessStatusCode();

    //    using var responseStream = await response.Content.ReadAsStreamAsync();
    //    JObject nivel = JObject.Parse(response.ToString());

    //    return nivel["level"].ToString();

    //}
}

