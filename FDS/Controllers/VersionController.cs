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
        private readonly IValidate _validateService;

        public VersionController(IVersion versionService, IValidate validateService)
        {
            _versionService = versionService;
            _validateService = validateService;
        }

        [HttpPost]
        public IEnumerable<VersionResponse> Get([FromBody]ClientData data)
        {
            if (_validateService.ValidateClientData(data.PackageId, out Guid packageId))
            {
                var versions = _versionService.GetAllForClient(packageId, data.Software, data.Country);
                if (versions != null && versions.Count() > 0)
                {
                    return PrepareVersionsModel(versions);
                }
                else
                {
                    return EmptyVersionResponse();
                }
            }
            else
            {
                return EmptyVersionResponse();
            }
        }

        private IEnumerable<VersionResponse> PrepareVersionsModel(IEnumerable<FDS2.Data.Models.Version> versions)
        {
            return versions.Select(v => new VersionResponse
            {
                Id = v.Id.ToString(),
                Name = v.Name
            });
        }

        private List<VersionResponse> EmptyVersionResponse()
        {
            return new List<VersionResponse>();
        }
    }
}
