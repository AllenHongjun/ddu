﻿using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using TigerAdmin.Authorization.Users;

namespace TigerAdmin.Demo.Events
{
    public class EventManager : IEventManager
    {   
        public IEventBus EventBus { get; set; }

        private readonly IEventRegistrationPolicy _registrationPolicy;
        private readonly IRepository<EventRegistration> _eventRegistrationRepository;
        private readonly IRepository<Event, Guid> _eventRepository;

        public EventManager(
            IEventRegistrationPolicy registrationPolicy, 
            IRepository<EventRegistration> eventRegistrationRepository, 
            IRepository<Event, Guid> eventRepository)
        {
            
            _registrationPolicy = registrationPolicy;
            _eventRegistrationRepository = eventRegistrationRepository;
            _eventRepository = eventRepository;

            EventBus = NullEventBus.Instance;
        }

        public void Cancel(Event @event)
        {
            @event.Cancel();
            EventBus.Trigger(new EventCancelledEvent(@event));
        }

        public async System.Threading.Tasks.Task CancelRegistrationAsync(Event @event, User user)
        {
            var registration = await _eventRegistrationRepository.FirstOrDefaultAsync(r => r.EventId == @event.Id && r.UserId == user.Id);
            if (registration == null)
            {   
                // No need to cancel since there is no such a registratoin
                return;
            }

            await registration.CancelAsync(_eventRegistrationRepository);
        }

        public async System.Threading.Tasks.Task CreateAsync(Event @event)
        {
            await _eventRepository.InsertAsync(@event);
        }

        public async Task<Event> GetAsync(Guid id)
        {
            var @event = await _eventRepository.FirstOrDefaultAsync(id);

            if (@event == null)
            {
                throw new UserFriendlyException("Could not found the event, maybe it's deleted!");
            }

            return @event;
        }

        public async Task<IReadOnlyList<User>> GetRegisteredUsersAsync(Event @event)
        {   
            return await _eventRegistrationRepository
                .GetAll()
                .Include(registration => registration.User)
                .Where(registration => registration.EventId == @event.Id)
                .Select(registration => registration.User)
                .ToListAsync();
        }

        public async Task<EventRegistration> RegisterAsync(Event @event, User user)
        {
            return await _eventRegistrationRepository.InsertAsync(
                await EventRegistration.CreateAsync(@event, user, _registrationPolicy));
        }
    }
}
