﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace TigerAdmin.Demo.Events
{
    [AutoMapFrom(typeof(Event))]
    public class EventListDto:FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsCancelled { get; set; }

        public virtual int MaxRegistrationCount { get; set; }

        public int RegistrationsCount { get; set; }
    }
}
