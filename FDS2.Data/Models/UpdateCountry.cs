using System;
using System.Collections.Generic;
using System.Text;

namespace FDS2.Data.Models
{
    public class UpdateCountry
    {
        public Guid UpdateId { get; set; }
        public Update Update { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
    }
}
