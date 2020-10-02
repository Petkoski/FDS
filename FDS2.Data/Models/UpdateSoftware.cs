using System;
using System.Collections.Generic;
using System.Text;

namespace FDS2.Data.Models
{
    public class UpdateSoftware
    {
        public Guid UpdateId { get; set; }
        public Update Update { get; set; }
        public Guid SoftwareId { get; set; }
        public Software Software { get; set; }
    }
}
