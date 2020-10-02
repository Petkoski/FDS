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

        public UpdateController(IUpdate updateService)
        {
            _updateService = updateService;
        }

        [HttpPost]
        public UpdateResponse Get([FromBody]ClientData data)
        {
            if (ValidateClientInput(data, out Guid packageId, out Guid versionId))
            {
                var update = _updateService.GetUpdate(packageId, versionId, data.Country, data.Software);
                return PrepareVersionsModel(update);
            }
            else
            {
                return EmptyUpdateResponse();
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

        private UpdateResponse PrepareVersionsModel(FDS2.Data.Models.Update update)
        {
            if (update?.UpdateFiles?.Count() > 0)
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
            else
            {
                return EmptyUpdateResponse();
            }
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
