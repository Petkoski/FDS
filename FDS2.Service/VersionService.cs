using FDS2.Data;
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

        public VersionService(ApplicationDbContext context, IPackage packageService)
        {
            _context = context;
            _packageService = packageService;
        }

        public Data.Models.Version GetById(Guid versionId)
        {
            return _context.Versions
                .FirstOrDefault(v => v.Id == versionId);
        }

        public IEnumerable<Data.Models.Version> GetAllForClient(Guid packageId, string software, string country)
        {
            var package = _packageService.GetById(packageId);
            return package.Updates.Select(u => u.Version).Distinct();
        }
    }
}
