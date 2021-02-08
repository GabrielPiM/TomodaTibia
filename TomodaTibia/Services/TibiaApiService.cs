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

    public interface ITApiService
    {
        Task<dynamic> Character(string _nome);
    }

    public class TibiaApiService : ITApiService
    {
        private readonly HttpClient _client;


        public TibiaApiService(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json; charset=utf-8");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "TomodaTibia");

            _client = httpClient;
        }

        public async Task<dynamic> Character(string _nome)
        {           
            _nome += ".json";
            var res = await _client.GetAsync(_nome).ConfigureAwait(false);
            res.EnsureSuccessStatusCode();
            string stringData = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
            var data = JsonConvert.DeserializeObject(stringData);

            return data;
        }



    }

}

