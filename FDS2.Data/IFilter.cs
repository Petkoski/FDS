using FDS2.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FDS2.Data
{
    public interface IFilter
    {
        Update FilterUpdates(List<Update> updates, Models.Version version, string clientSoftware, string clientCountry);
        IEnumerable<Models.Version> FilterVersions(List<Update> updates, string clientSoftware, string clientCountry);
    }
}
