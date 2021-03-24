using System;
using System.Collections.Generic;
using System.Text;

namespace TomodaTibiaModels.Utils.DBMaps
{
   public static class HuntSpecialReqTypeMap
    {
        //Categorias----------------------
        private const int WithImg = 1;
        private const int Text = 2;
        private const int Spoiler = 3;
        //--------------------------------
        
        public const int Location = Text;
        public const int Spell = Text;

        public const int Quest = Spoiler;
        public const int Achivement = Spoiler;

        public const int Key = WithImg;
        public const int Mount = WithImg;
        public const int Object = WithImg;
        public const int Book = WithImg;
        public const int Item = WithImg;

    }
}
