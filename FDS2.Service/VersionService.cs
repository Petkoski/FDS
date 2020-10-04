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

        public IEnumerable<Data.Models.Version> GetAllForClient(Guid packageId, string clientSoftware, string clientCountry)
        {
            var package = _packageService.GetById(packageId, false);
            return FilterVersions(package.Updates.ToList(), clientSoftware, clientCountry);
        }

        private IEnumerable<Data.Models.Version> FilterVersions(List<Update> updates, string clientSoftware, string clientCountry)
        {
            return updates.Where(u => u.UpdateSoftwares.Any(us => us.Software.Name.ToLower() == clientSoftware.ToLower())
                && (u.UpdateCountries.Count() == 0 || u.UpdateCountries.Any(c => c.Country.Code.ToLower() == clientCountry.ToLower()))
                && (u.PublishDate is null || u.PublishDate <= DateTime.Now)
                && u.Channel.Value == (int)ChannelEnum.Public)
                .Select(u => u.Version)
                .Distinct();
        }
    }
}
