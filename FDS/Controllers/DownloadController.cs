using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FDS.Models;
using FDS2.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DownloadController : ControllerBase
    {
        private readonly IUpdate _updateService;
        private readonly IZip _zipService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IValidate _validateService;

        public DownloadController(IUpdate updateService, IZip zipService, IWebHostEnvironment environment, IValidate validateService)
        {
            _updateService = updateService;
            _zipService = zipService;
            _hostingEnvironment = environment;
            _validateService = validateService;
        }

        [HttpPost]
        public async Task<ActionResult> Get([FromBody]ClientData data)
        {
            if (_validateService.ValidateClientData(data.PackageId, data.VersionId, data.Software, out Guid packageId, out Guid versionId))
            {
                var update = _updateService.GetUpdate(packageId, versionId, data.Software, data.Country);
                var zippedFile = await _zipService.ReturnZippedUpdateBytes(update?.UpdateFiles?.Select(uf => uf.File).ToList(), _hostingEnvironment.ContentRootPath);
                
                if (zippedFile != null)
                {
                    return new FileContentResult(zippedFile, System.Net.Mime.MediaTypeNames.Application.Zip)
                    {
                        FileDownloadName = "Update.zip"
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
    }
}
