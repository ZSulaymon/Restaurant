using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models.Restaurant
{
    public class RestInfo
    {
        public int Id { get; set; }
        public string RestName { get; set; }
        public string RestAddress { get; set; }
        public string RestReferencePoint { get; set; }
        public string RestPhone { get; set; }
        public string RestAdministrator { get; set; }
    }
}
