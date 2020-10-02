using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FDS.Models;
using FDS2.Data;
using FDS2.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

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
        public IEnumerable<PackageReturn> Get()
        {
            var packages = _packageService.GetAll();
            return PreparePackagesModel(packages);
        }

        private IEnumerable<PackageReturn> PreparePackagesModel(IEnumerable<Package> packages)
        {
            return packages.Select(p => new PackageReturn
            {
                Id = p.Id.ToString(),
                Name = p.Name
            });
        }
    }
}
