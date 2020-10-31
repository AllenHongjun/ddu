using Abp.Auditing;
using System;
using System.Collections.Generic;
using System.Text;
using TigerAdmin.Authorization.Users;

namespace TigerAdmin.Auditing
{
    public class AuditLogAndUser
    {
        public AuditLog AuditLog { get; set; }
        
        public User User { get; set; }
    }
}
