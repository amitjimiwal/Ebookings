using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Events
{
    public class QueryParamsDTO
    {
        public string? Category { get; set; } = null;
        public string? SortBY { get; set; } = null;

        //specifies the order of the above SORTBY parameter
        public bool IsDescending { get; set; } = false;
    }
}