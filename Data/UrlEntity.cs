using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UrlEntity : Entity
    {
        public string LongUrl { get; set; }
        public string ShortRelativeUrl { get; set; }
    }
}