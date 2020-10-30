using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
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
    public class EventAppService : TigerAdminAppServiceBase, IEventAppService
    {
        private readonly IEventManager _eventManager;
        private readonly IRepository<Event, Guid> _eventRepository;

        public EventAppService(
            IEventManager eventManager, 
            IRepository<Event, Guid> eventRepository)
        {
            _eventManager = eventManager;
            _eventRepository = eventRepository;
        }

        public async Task CancelAsync(EntityDto<Guid> input)
        {
            var @event = await _eventManager.GetAsync(input.Id);
            _eventManager.Cancel(@event);
        }

        public async Task CancelRegistrationAsync(EntityDto<Guid> input)
        {
            await _eventManager.CancelRegistrationAsync(
                await _eventManager.GetAsync(input.Id),
                await GetCurrentUserAsync()
                );
            throw new NotImplementedException();
        }

        public async Task CreateAsync(CreateEventInput input)
        {
            var @event = Event.Create(AbpSession.GetTenantId(), input.Title, input.Date, input.Description, input.MaxRegistrationCount);
            await _eventManager.CreateAsync(@event);
        }

        public async Task<EventDetailOutput> GetDetailAsync(EntityDto<Guid> input)
        {
            var @event = await _eventRepository
                .GetAll()
                .Include(e => e.Registrations)
                .ThenInclude(r => r.User)
                .Where(e => e.Id == input.Id)
                .FirstOrDefaultAsync();
            if (@event == null)
            {
                throw new UserFriendlyException("Could not found the event, maybe it's deleted.");
            }
            return ObjectMapper.Map<EventDetailOutput>(@event);
            //return @event.MapTo<EventDetailOutput>();
        }

        public async Task<ListResultDto<EventListDto>> GetListAsync(GetEventListInput input)
        {
            var events = await _eventRepository
                .GetAll()
                .Include(e => e.Registrations)
                .WhereIf(!input.IncludeCanceledEvents, e => !e.IsCancelled)
                .OrderByDescending(e => e.CreationTime)
                .Take(64)
                .ToListAsync();
            return new ListResultDto<EventListDto>(events.MapTo<List<EventListDto>>());
        }

        public async Task<EventRegisterOutput> RegisterAsync(EntityDto<Guid> input)
        {
            var registration = await RegisterAndSaveAsync(
                await _eventManager.GetAsync(input.Id),
                await GetCurrentUserAsync()
                );
            return new EventRegisterOutput
            {
                RegistrationId = registration.Id
            };
        }

        private async Task<EventRegistration> RegisterAndSaveAsync(Event @event, User user)
        {
            var registration = await _eventManager.RegisterAsync(@event, user);
            await CurrentUnitOfWork.SaveChangesAsync();
            return registration;
        }
    }
}
