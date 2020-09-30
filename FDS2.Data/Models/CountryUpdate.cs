using System;
using System.Collections.Generic;
using System.Text;

namespace FDS2.Data.Models
{
    public class CountryUpdate
    {
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public Guid UpdateId { get; set; }
        public Update Update { get; set; }
    }
}
