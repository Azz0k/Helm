using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Core.Domain.Users
{
    [Flags]
    public enum UserRole
    {
        None = 0b_0000_0000,
        CanEditUsers = 0b_0000_0001,

    }
}
