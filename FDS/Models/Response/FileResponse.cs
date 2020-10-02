using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDS.Models.Response
{
    public class FileResponse
    {
        public string Id { get; set; }
        public string Location { get; set; }
        public string Checksum { get; set; }
    }
}
