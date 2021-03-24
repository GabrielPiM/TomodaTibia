using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomodaTibiaModels.Utils.DBMaps
{

    public static class VocationMap
    {

        private static readonly Dictionary<string, int> Vocations = new Dictionary<string, int>
        {
                { "None", 0 },
                { "Knight", 1 },
                { "Elite Knight", 1},
                { "Druid", 2 },
                { "Elder Druid", 2 },
                { "Paladin", 3 },
                { "Royal Paladin", 3 },
                { "Sorecer", 4 },
                { "Master Sorcerer", 4 },
         };

        public static int GetID(string key)
        {
            int id;

            Vocations.TryGetValue(key, out id);
            return id;
        }

        public static string GetVocation(int key)
        {
            string vocation;
            vocation = Vocations.FirstOrDefault(x => x.Value == key).Key;
            return vocation;
        }

        public static readonly List<int> VocationsIds = new List<int>() { 0, 1, 2, 3, 4 };
    }





}
