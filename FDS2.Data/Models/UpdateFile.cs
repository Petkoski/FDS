using System;
using System.Collections.Generic;
using System.Text;

namespace FDS2.Data.Models
{
    public class UpdateFile
    {
        public Guid UpdateId { get; set; }
        public Update Update { get; set; }
        public Guid FileId { get; set; }
        public File File { get; set; }
    }
}
