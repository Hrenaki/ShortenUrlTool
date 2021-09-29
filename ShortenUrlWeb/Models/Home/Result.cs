using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortenUrlWeb.Models.Home
{
    public class Result
    {
        public bool IsSuccessful { get; set; }
        public string InternalUrl { get; set; }
        public string Message { get; set; }
    }
}