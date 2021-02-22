using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomodaTibiaAPI.Utils.Pagination
{
    public abstract class PagedQueryParameter
    {
        const int maxPageSize = 50;

        private int _pageNumber { get; set; }
        private int _pageSize = 10;

        public int PageNumber {

            get 
            { 
                return _pageNumber; 
            }

            set 
            {
                _pageNumber = (value <= 0) ? 1 : value;
            }
        }
  
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
