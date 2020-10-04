using FDS2.Data;
using FDS2.Data.Enums;
using FDS2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FDS2.Service
{
    public class VersionService : IVersion
    {
        private readonly ApplicationDbContext _context;
        private readonly IPackage _packageService;
        private readonly IFilter _filterService;

        public VersionService(ApplicationDbContext context, IPackage packageService, IFilter filterService)
        {
            _context = context;
            _packageService = packageService;
            _filterService = filterService;
        }

        public Data.Models.Version GetById(Guid versionId)
        {
            return _context.Versions
                .FirstOrDefault(v => v.Id == versionId);
        }

        public IEnumerable<Data.Models.Version> GetAllForClient(Guid packageId, string clientSoftware, string clientCountry)
        {
            var package = _packageService.GetById(packageId, false);
            return _filterService.FilterVersions(package?.Updates?.ToList(), clientSoftware, clientCountry);
        }
    }
}
