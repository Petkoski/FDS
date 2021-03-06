﻿using FDS2.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FDS2.Data
{
    public interface IPackage
    {
        IEnumerable<Package> GetAll();
        Package GetById(Guid id, bool includeFiles = true);
    }
}
