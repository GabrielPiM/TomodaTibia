using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomodaTibiaAPI.BLL
{
    public interface IBLL
    {
        void PopulateDictionary();
        void CheckName(string name);
        bool CheckIsNull(Object obj);
        bool FoundErrors();    
    }
}
