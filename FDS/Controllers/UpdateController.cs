using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FDS.Models;
using FDS.Models.Response;
using FDS2.Data;
using FDS2.Data.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateController : ControllerBase
    {
        private readonly IUpdate _updateService;
        private readonly IValidate _validateService;

        public UpdateController(IUpdate updateService, IValidate validateService)
        {
            _updateService = updateService;
            _validateService = validateService;
        }

        [HttpPost]
        public UpdateResponse Get([FromBody]ClientData data)
        {
            if (_validateService.ValidateClientData(data.PackageId, data.VersionId, data.Software, out Guid packageId, out Guid versionId))
            {
                var update = _updateService.GetUpdate(packageId, versionId, data.Software, data.Country);
                if (update != null && update.UpdateFiles.Count() > 0)
                {
                    return PrepareVersionsModel(update);
                }
                else
                {
                    return EmptyUpdateResponse();
                }
            }
            else
            {
                return EmptyUpdateResponse();
            }
        }

        private UpdateResponse PrepareVersionsModel(FDS2.Data.Models.Update update)
        {
            return new UpdateResponse
            {
                Files = update.UpdateFiles.Select(f => new FileResponse
                {
                    Id = f.File.Id.ToString(),
                    Location = f.File.Location,
                    Checksum = f.File.Checksum
                })
            };
        }

        private UpdateResponse EmptyUpdateResponse()
        {
            return new UpdateResponse
            {
                Files = new List<FileResponse>()
            };
        }
    }
}
