using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using TigerAdmin.Authorization.Users;

namespace TigerAdmin.Demo.Events
{
    public interface IEventRegistrationPolicy: IDomainService
    {
        /// <summary>
        /// Checks if given user can register to <see cref="@event"/> and throws exception if can not.
        /// </summary>
        System.Threading.Tasks.Task CheckRegistrationAttemptAsync(Event @event, User user);
    }
}
