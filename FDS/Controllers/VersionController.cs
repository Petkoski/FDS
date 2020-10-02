using FDS.Models;
using FDS.Models.Response;
using FDS2.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VersionController : ControllerBase
    {
        private readonly IVersion _versionService;

        public VersionController(IVersion versionService)
        {
            _versionService = versionService;
        }

        [HttpPost]
        public IEnumerable<VersionResponse> Get([FromBody]ClientData data)
        {
            if (ValidateClientInput(data, out Guid packageId))
            {
                var versions = _versionService.GetAllForClient(packageId, data.Software, data.Country);
                return PrepareVersionsModel(versions);
            }
            else
            {
                return new List<VersionResponse>();
            }
        }

        private bool ValidateClientInput(ClientData data, out Guid packageId)
        {
            return Guid.TryParse(data.PackageId, out packageId);
        }

        private IEnumerable<VersionResponse> PrepareVersionsModel(IEnumerable<FDS2.Data.Models.Version> versions)
        {
            return versions.Select(v => new VersionResponse
            {
                Id = v.Id.ToString(),
                Name = v.Name
            });
        }
    }
}
