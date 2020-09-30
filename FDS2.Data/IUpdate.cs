using FDS2.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FDS2.Data
{
    public interface IUpdate
    {
        Update GetUpdate(string id);
        Package GetPackageById(string id);
    }
}
