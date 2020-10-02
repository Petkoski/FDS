using FDS2.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FDS2.Data
{
    public interface IVersion
    {
        Models.Version GetById(Guid versionId);
        IEnumerable<Models.Version> GetAllForClient(Guid packageId, string software, string country);
    }
}
