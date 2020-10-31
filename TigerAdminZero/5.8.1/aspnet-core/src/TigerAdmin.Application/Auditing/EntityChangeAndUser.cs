using Abp.EntityHistory;
using System;
using System.Collections.Generic;
using System.Text;
using TigerAdmin.Authorization.Users;

namespace TigerAdmin.Auditing
{
    public class EntityChangeAndUser
    {
        public EntityChange EntityChange { get; set; }

        public User User { get; set; }

    }
}
