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

        public Update GetUpdate(Guid packageId, Guid versionId, string clientCountry, string clientSoftware)
        {
            var package = _packageService.GetById(packageId);
            var version = _versionService.GetById(versionId);

            return FilterUpdates(package?.Updates?.ToList(), version, clientCountry, clientSoftware);
        }

        private Update FilterUpdates(List<Update> updates, Data.Models.Version version, string clientCountry, string clientSoftware)
        {
            return updates?.Where(u => u.UpdateSoftwares.Any(us => us.Software.Name.ToLower() == clientSoftware.ToLower())
                && u.Version.Order > version?.Order
                && (u.UpdateCountries.Count() == 0 || u.UpdateCountries.Any(c => c.Country.Code.ToLower() == clientCountry.ToLower()))
                && (u.PublishDate is null || u.PublishDate <= DateTime.Now)
                && u.Channel.Value == (int)ChannelEnum.Public)
                .OrderByDescending(u => u.UpdateCountries.Count()) //Order region-specific updates on top
                .ThenByDescending(u => u.CreatedDate) //Order newest updates on top
                .FirstOrDefault();
        }
    }
}
