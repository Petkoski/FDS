using FDS2.Data;
using FDS2.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public Update GetUpdate(string id)
        {
            var package = GetPackageById(id);
            return FilterUpdates(package.Updates.ToList());
        }

        public Update FilterUpdates(List<Update> updates)
        {
            return updates.Where(u => u.UpdateSoftwares.Any(us => us.Software.Name == "Windows")
                && u.Version.Order > 1
                && (u.UpdateCountries.Count() == 0 || u.UpdateCountries.Any(c => c.Country.Code == "MK"))
                && (u.PublishDate is null || u.PublishDate <= DateTime.Now)
                && u.Channel.Value == 3)
                .OrderByDescending(u => u.UpdateCountries.Count()) //Order region-specific updates on top
                .ThenByDescending(u => u.CreatedDate) //Order newest updates on top
                .FirstOrDefault();
        }
    }
}
