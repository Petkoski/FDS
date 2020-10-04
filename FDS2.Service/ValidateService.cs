using FDS2.Data;
using FDS2.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FDS2.Service
{
    public class ValidateService : IValidate
    {
        public bool ValidateClientData(string packageId, out Guid packageIdGuid)
        {
            return Guid.TryParse(packageId, out packageIdGuid);
        }

        public bool ValidateClientData(string packageId, string versionId, string clientSoftware, out Guid packageIdGuid, out Guid versionIdGuid)
        {
            bool packageCheck = Guid.TryParse(packageId, out packageIdGuid);
            bool versionCheck = Guid.TryParse(versionId, out versionIdGuid);
            return (packageCheck
                && versionCheck
                && Enum.TryParse(clientSoftware, out SoftwareEnum software));
        }
    }
}
