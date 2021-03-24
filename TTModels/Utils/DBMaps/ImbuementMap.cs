using System;
using System.Collections.Generic;
using System.Text;

namespace TomodaTibiaModels.Utils.DBMaps
{
    public static class ImbuementMap
    {

        private static readonly Dictionary<int, string> Imbuements = new Dictionary<int, string> {
            { 1,"Bash"},
            { 2,"Blockade"},
            { 3,"Chop"},
            { 4,"Cloud Fabric"},
            { 5,"Demon Presence"},
            { 6,"Dragon Hide"},
            { 7,"Electrify"},
            { 8,"Epiphany"},
            { 9,"Featherweight"},
            { 10,"Frost"},
            { 11,"Lich Shroud"},
            { 12,"Precision"},
            { 13,"Quara Scale"},
            { 14,"Reap"},
            { 15,"Scorch"},
            { 16,"Slash"},
            { 17,"Snake Skin"},
            { 18,"Strike"},
            { 19,"Swiftness"},
            { 20,"Vampirism"},
            { 21,"Venom"},
            { 22,"Vibrancy"},
            { 23,"Void"},

        };

        public static string GetImbuement(int id)
        {
            string imbuementName = "";
            Imbuements.TryGetValue(id, out imbuementName);
            return imbuementName;
        }


    }
}
