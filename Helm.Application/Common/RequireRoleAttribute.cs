using System;
using System.Collections.Generic;
using System.Text;

namespace Helm.Application.Common
{
    public class RequireRole : Attribute
    {
        public RequireRole(string role)
        {
            Role = role;
        }
        public string Role {  get; set; }
    }
}
