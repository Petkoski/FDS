using FDS2.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace FDS2.Service
{
    public class ZipService : IZip
    {
        public async Task<byte[]> ReturnZippedUpdateBytes(List<Data.Models.File> files, string contentRootPath)
        {
            if (files == null || files.Count == 0)
            {
                return null;
            }

            string zipFileName = $"{Guid.NewGuid()}.zip";
            var zipPath = Path.Combine(contentRootPath, "zippedUpdates", zipFileName);

            using (var zipArchive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                foreach (var file in files)
                {
                    List<string> filePath = new List<string>
                    {
                        contentRootPath
                    };
                    filePath.AddRange(file.Location.Split("/", StringSplitOptions.RemoveEmptyEntries));

                    var fileInfo = new FileInfo(Path.Combine(filePath.ToArray()));
                    zipArchive.CreateEntryFromFile(fileInfo.FullName, fileInfo.Name);
                }
            }

            return await File.ReadAllBytesAsync(zipPath);
        }
    }
}
