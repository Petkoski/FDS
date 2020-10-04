using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FDS.Models.Response;
using FDS2.Data;
using FDS2.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace FDS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackageController : ControllerBase
    {
        private readonly IPackage _packageService;

        public PackageController(IPackage packageService)
        {
            _packageService = packageService;
        }

        [HttpGet]
        public IEnumerable<PackageResponse> Get()
        {
            var packages = _packageService.GetAll();
            return PreparePackagesModel(packages);
        }

        private IEnumerable<PackageResponse> PreparePackagesModel(IEnumerable<Package> packages)
        {
            return packages.Select(p => new PackageResponse
            {
                Id = p.Id.ToString(),
                Name = p.Name
            });
        }
    }
}
