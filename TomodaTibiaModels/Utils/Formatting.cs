using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TomodaTibiaModels.Utils
{
    public static class Formatting
    {
       
        static TextInfo myTI = new CultureInfo("en-US", false).TextInfo;


        public static string ExtractName(string input)
        {
            input = input.Replace("_"," ");
            input = myTI.ToTitleCase(input);
            input = input.Substring(0, input.Length - 4);

            return input;
        }
    }
}
