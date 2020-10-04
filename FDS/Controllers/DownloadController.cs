using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FDS.Models;
using FDS.Models.Response;
using FDS2.Data;
using FDS2.Data.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DownloadController : ControllerBase
    {
        private readonly IUpdate _updateService;
        private readonly IZip _zipService;
        private IWebHostEnvironment _hostingEnvironment;

        public DownloadController(IUpdate updateService, IZip zipService, IWebHostEnvironment environment)
        {
            _updateService = updateService;
            _zipService = zipService;
            _hostingEnvironment = environment;
        }

        [HttpPost]
        public async Task<ActionResult> Get([FromBody]ClientData data)
        {
            if (ValidateClientInput(data, out Guid packageId, out Guid versionId))
            {
                var update = _updateService.GetUpdate(packageId, versionId, data.Country, data.Software);
                var zippedFile = await _zipService.ReturnZippedUpdateBytes(update.UpdateFiles.Select(uf => uf.File).ToList(), _hostingEnvironment.ContentRootPath);
                
                if (zippedFile != null)
                {
                    return new FileContentResult(zippedFile, System.Net.Mime.MediaTypeNames.Application.Zip)
                    {
                        FileDownloadName = "update.zip"
                    };
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        private bool ValidateClientInput(ClientData data, out Guid packageId, out Guid versionId)
        {
            bool packageCheck = Guid.TryParse(data.PackageId, out packageId);
            bool versionCheck = Guid.TryParse(data.VersionId, out versionId);
            return (packageCheck
                && versionCheck
                && Enum.TryParse(data.Software, out SoftwareEnum software));
        }
    }
}
