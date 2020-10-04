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
        private readonly IFilter _filterService;

        public UpdateService(ApplicationDbContext context, IPackage packageService, IVersion versionService, IFilter filterService)
        {
            _context = context;
            _packageService = packageService;
            _versionService = versionService;
            _filterService = filterService;
        }

        public Update GetUpdate(Guid packageId, Guid versionId, string clientSoftware, string clientCountry)
        {
            var package = _packageService.GetById(packageId);
            var version = _versionService.GetById(versionId);
            return _filterService.FilterUpdates(package?.Updates?.ToList(), version, clientSoftware, clientCountry);
        }
    }
}
