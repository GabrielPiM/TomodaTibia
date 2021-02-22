using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using TomodaTibiaAPI.Utils;
using TomodaTibiaModels.Hunt.Response;
using TomodaTibiaModels.Utils;

namespace TomodaTibiaAPI.Services
{
    public class CacheService
    {
        public MemoryCache Cache { get; set; }

        public CacheService()
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                //Quantidade maxima de "Searchs" armazenados em cache simultaneamente.
                SizeLimit = 200
            });
        }


        public Response<SearchResponse> GetCachedSearch(string characterNameKey)
        {
            var response = new Response<SearchResponse>(new SearchResponse());
            var SearchResponse = new SearchResponse();

           

            if (Cache.TryGetValue(characterNameKey, out SearchResponse))
            {
                response.Data = SearchResponse;
            }
            else
            {
                
                response.Succeeded = false;
            }

            return response;
        }

        public void CreateCachedSearch(SearchResponse searchResponse)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Set cache entry size by extension method.
                .SetSize(1)
              // Keep in cache for this time, reset time if accessed.
              .SetSlidingExpiration(TimeSpan.FromSeconds(180));

            Cache.Set(searchResponse.Character.Name, searchResponse, cacheEntryOptions);
        }

    }
}
