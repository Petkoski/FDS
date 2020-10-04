using System;
using System.Collections.Generic;
using System.Text;

namespace FDS2.Data
{
    public interface IValidate
    {
        bool ValidateClientData(string packageId, out Guid packageIdGuid);
        bool ValidateClientData(string packageId, string versionId, string clientSoftware, out Guid packageIdGuid, out Guid versionIdGuid);
    }
}
