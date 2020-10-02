using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDS.Models.Response
{
    public class UpdateResponse
    {
        public IEnumerable<FileResponse> Files { get; set; }
    }
}
