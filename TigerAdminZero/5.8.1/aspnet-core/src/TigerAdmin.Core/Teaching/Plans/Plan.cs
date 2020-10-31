using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TigerAdmin.Teaching.Courses;

namespace TigerAdmin.Teaching.Plans
{
    /// <summary>
    /// 课程计划
    /// </summary>
    public class Plan:FullAuditedEntity<Guid>
    {
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public Guid CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

    }
}
