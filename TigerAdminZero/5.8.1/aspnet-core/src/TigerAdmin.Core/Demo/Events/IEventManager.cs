using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using TigerAdmin.Authorization.Users;

namespace TigerAdmin.Demo.Events
{
    public interface IEventManager:IDomainService
    {
        System.Threading.Tasks.Task<Event> GetAsync(Guid id);

        System.Threading.Tasks.Task CreateAsync(Event @event);

        void Cancel(Event @event);

        System.Threading.Tasks.Task<EventRegistration> RegisterAsync(Event @event, User user);

        System.Threading.Tasks.Task CancelRegistrationAsync(Event @event, User user);

        System.Threading.Tasks.Task<IReadOnlyList<User>> GetRegisteredUsersAsync(Event @event);
    }
}
