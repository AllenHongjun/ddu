using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TigerAdmin.Domain.Events;

namespace TigerAdmin.Demo.Events
{
    [Table("AppEvents")]
    public class Event : FullAuditedEntity<Guid>, IMustHaveTenant
    {   
        // 看官网文档 中文教程 demo 源码  熟悉了。感觉能上手了。在去做自己的业务。
        // 有问题就 baidu google stackoverflow 等等


        public const int MaxTitleLength = 128;

        public const int MaxDescriptionLength = 2048;

        public int TenantId { get; set; }

        [Required]
        [StringLength(MaxTitleLength)]
        public virtual string Title { get; protected set; }

        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; protected set; }

        public virtual DateTime Date { get; protected set; }

        public virtual bool IsCancelled { get; protected set; }

        /// <summary>
        /// We don't make constructor public and forcing to create events using <see cref="Create"/> method.
        /// But constructor can not be private since it's used by EntityFramework.
        /// Thats why we did it protected.
        /// </summary>
        [Range(0, int.MaxValue)]
        public virtual int MaxRegistrationCount { get; protected set; }

        /// <summary>
        /// 一个时间对象可以被多个用户注册 1对多
        /// </summary>
        [ForeignKey("EventId")]
        public virtual ICollection<EventRegistration> Registrations { get; protected set; }


        /// <summary>
        /// 
        /// </summary>
        protected Event()
        {

        }

        public static Event Create(int tenantId, string title, DateTime date, string desription = null, int maxRegistrationCount = 0)
        {
            var @event = new Event
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Title = title,
                Description = desription,
                MaxRegistrationCount = maxRegistrationCount
            };

            //@event.SetDate
            @event.SetDate(date);
            @event.Registrations = new Collection<EventRegistration>();
            return @event;
        }

        public bool IsInPast()
        {
            return Date < Clock.Now;
        }

        public bool IsAllowCancellationTimeEnded()
        {
            return Date.Subtract(Clock.Now).TotalHours <= 2.0;
        }

        public void ChangeDate(DateTime date)
        {
            if (date == Date)
            {
                return;
            }

            SetDate(date);

            DomainEvents.EventBus.Trigger(new EventDateChangedEvent(this));

        }

        internal void Cancel()
        {
            AssertNotInPast();
            IsCancelled = true;
        }

        private void SetDate(DateTime date)
        {
            AssertNotCancelled();
            if (date < Clock.Now)
            {
                throw new UserFriendlyException("Can not set an event's date in the past!");
            }

            if (date <= Clock.Now.AddHours(3))
            {
                throw new UserFriendlyException("Should set an event's date 3 hours before at least!");
            }
            Date = date;

            // 通过事件总线触发来 修改时间
            DomainEvents.EventBus.Trigger(new EventDateChangedEvent(this));
        }

        private void AssertNotInPast()
        {
            if (IsInPast())
            {
                throw new UserFriendlyException("This event was in the past");
            }
        }

        private void AssertNotCancelled()
        {
            if (IsCancelled)
            {
                throw new UserFriendlyException("This event is cancelled");
            }
        }

    }
}
