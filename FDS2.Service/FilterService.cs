using FDS2.Data;
using FDS2.Data.Enums;
using FDS2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FDS2.Service
{
    public class FilterService : IFilter
    {
        public Update FilterUpdates(List<Update> updates, Data.Models.Version version, string clientSoftware, string clientCountry)
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

        public IEnumerable<Data.Models.Version> FilterVersions(List<Update> updates, string clientSoftware, string clientCountry)
        {
            return updates?.Where(u => u.UpdateSoftwares.Any(us => us.Software.Name.ToLower() == clientSoftware.ToLower())
                && (u.UpdateCountries.Count() == 0 || u.UpdateCountries.Any(c => c.Country.Code.ToLower() == clientCountry.ToLower()))
                && (u.PublishDate is null || u.PublishDate <= DateTime.Now)
                && u.Channel.Value == (int)ChannelEnum.Public)
                .Select(u => u.Version)
                .Distinct();
        }
    }
}
