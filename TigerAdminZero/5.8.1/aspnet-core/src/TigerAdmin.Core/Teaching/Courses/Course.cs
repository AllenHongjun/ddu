using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace TigerAdmin.Teaching.Courses
{
    /// <summary>
    /// 课程
    /// </summary>
    public class Course:FullAuditedEntity<Guid>
    {
        public string Title { get; set; }

        public string Intro { get; set; }

        public int CategoryId { get; set; }

        public string CoverUrl { get; set; }


    }

    public enum CourseState
    {
        Unpublished = 1,
        Published = 2,
        Closed = 3
    }
}
