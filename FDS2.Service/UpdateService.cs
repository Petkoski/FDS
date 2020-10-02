using FDS2.Data;
using FDS2.Data.Enums;
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
        private readonly IPackage _packageService;
        private readonly IVersion _versionService;

        public UpdateService(ApplicationDbContext context, IPackage packageService, IVersion versionService)
        {
            _context = context;
            _packageService = packageService;
            _versionService = versionService;
        }

        public Update GetUpdate(Guid packageId)
        {
            var package = _packageService.GetById(packageId);
            var guid = new Guid("8E15BFC4-A4D8-463D-8864-5BC45811DEC2");
            var version = _versionService.GetById(guid);

            return FilterUpdates(package.Updates.ToList(), version);
        }

        private Update FilterUpdates(List<Update> updates, Data.Models.Version version)
        {
            return updates.Where(u => u.UpdateSoftwares.Any(us => us.Software.Name == "Windows")
                && u.Version.Order > version.Order
                && (u.UpdateCountries.Count() == 0 || u.UpdateCountries.Any(c => c.Country.Code == "MK"))
                && (u.PublishDate is null || u.PublishDate <= DateTime.Now)
                && u.Channel.Value == (int)ChannelEnum.Public)
                .OrderByDescending(u => u.UpdateCountries.Count()) //Order region-specific updates on top
                .ThenByDescending(u => u.CreatedDate) //Order newest updates on top
                .FirstOrDefault();
        }
    }
}
