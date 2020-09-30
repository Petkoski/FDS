using FDS2.Data;
using FDS2.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FDS2.Service
{
    public class UpdateService : IUpdate
    {
        private readonly ApplicationDbContext _context;

        public UpdateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Package GetPackageById(string id)
        {
            var guidId = new Guid(id);

            return _context.Packages
                .Where(p => p.Id == guidId)
                .Include(p => p.Updates)
                    .ThenInclude(u => u.UpdateFiles)
                        .ThenInclude(uf => uf.File)
                .Include(p => p.Updates)
                    .ThenInclude(u => u.Version)
                .Include(p => p.Updates)
                    .ThenInclude(u => u.CountryUpdates)
                .Include(p => p.PackageFiles)
                    .ThenInclude(pf => pf.File)
                .FirstOrDefault();
        }

        public Update GetUpdate(string id)
        {
            var package = GetPackageById(id);
            return package.Updates.FirstOrDefault();
        }
    }
}
