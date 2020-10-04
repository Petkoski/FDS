using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FDS2.Data
{
    public interface IZip
    {
        Task<byte[]> ReturnZippedUpdateBytes(List<Models.File> files, string contentRootPath);
    }
}
