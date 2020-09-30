using System;
using System.Collections.Generic;
using System.Text;

namespace FDS2.Data.Models
{
    public class PackageFile
    {
        public Guid PackageId { get; set; }
        public Package Package { get; set; }
        public Guid FileId { get; set; }
        public File File { get; set; }
    }
}