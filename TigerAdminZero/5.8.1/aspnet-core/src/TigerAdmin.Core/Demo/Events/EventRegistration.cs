using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using TigerAdmin.Authorization.Users;

namespace TigerAdmin.Demo.Events
{
    [Table("AppEventRegistrations")]
    public class EventRegistration : CreationAuditedEntity, IMustHaveTenant
    {   

        public int TenantId { get; set; }

        public virtual Event Event { get; set; }
        
        public virtual Guid EventId { get; protected set; }

        [ForeignKey("UserId")]
        public virtual User User { get; protected set; }

        public virtual long UserId { get; protected set; }

        protected EventRegistration()
        {

        }

        /// <summary>
        /// 通过CreateAsync方法来创建这个实体类对象
        /// </summary>
        /// <param name="event"></param>
        /// <param name="user"></param>
        /// <param name="registrationPolicy"></param>
        /// <returns></returns>
        public static async Task<EventRegistration> CreateAsync(Event @event, User user, IEventRegistrationPolicy registrationPolicy)
        {
            await registrationPolicy.CheckRegistrationAttemptAsync(@event, user);

            return new EventRegistration
            {
                TenantId = @event.TenantId,
                EventId = @event.Id,
                Event = @event,
                UserId = user.Id,
                User = user
            };
        }

        public async System.Threading.Tasks.Task CancelAsync(IRepository<EventRegistration> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            if (Event.IsInPast())
            {
                throw new UserFriendlyException("Can not cancel event which is in the past!");
            }

            if (Event.IsAllowCancellationTimeEnded())
            {
                throw new UserFriendlyException("It's too late to cancel your registration!");
            }

            await repository.DeleteAsync(this);
        } 


    }
}
