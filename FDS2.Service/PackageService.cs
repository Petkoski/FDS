using FDS2.Data;
using FDS2.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FDS2.Service
{
    public class PackageService : IPackage
    {
        private readonly ApplicationDbContext _context;

        public PackageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Package> GetAll()
        {
            return _context.Packages.ToList();
        }

        public Package GetById(Guid packageId)
        {
            return _context.Packages
                .Where(p => p.Id == packageId)
                .Include(p => p.Updates)
                    .ThenInclude(u => u.Version)
                .Include(p => p.Updates)
                    .ThenInclude(u => u.Channel)
                .Include(p => p.Updates)
                    .ThenInclude(u => u.UpdateCountries)
                        .ThenInclude(uc => uc.Country)
                .Include(p => p.Updates)
                    .ThenInclude(u => u.UpdateFiles)
                        .ThenInclude(uf => uf.File)
                .Include(p => p.Updates)
                    .ThenInclude(u => u.UpdateSoftwares)
                        .ThenInclude(uf => uf.Software)
                .Include(p => p.PackageFiles)
                    .ThenInclude(pf => pf.File)
                .FirstOrDefault();
        }

        public Package GetByIdLight(Guid packageId)
        {
            return _context.Packages
                .Where(p => p.Id == packageId)
                .Include(p => p.Updates)
                    .ThenInclude(u => u.Version)
                .Include(p => p.Updates)
                    .ThenInclude(u => u.Channel)
                .Include(p => p.Updates)
                    .ThenInclude(u => u.UpdateCountries)
                        .ThenInclude(uc => uc.Country)
                //.Include(p => p.Updates)
                //    .ThenInclude(u => u.UpdateFiles)
                //        .ThenInclude(uf => uf.File)
                .Include(p => p.Updates)
                    .ThenInclude(u => u.UpdateSoftwares)
                        .ThenInclude(uf => uf.Software)
                //.Include(p => p.PackageFiles)
                //    .ThenInclude(pf => pf.File)
                .FirstOrDefault();
        }
    }
}
