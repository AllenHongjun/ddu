using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TigerAdmin.Teaching.Lessons;

namespace TigerAdmin.Teaching.Resources
{
    /// <summary>
    /// 课程资源
    /// </summary>
    public class Resource:FullAuditedEntity<Guid>
    {
        public string Name { get; set; }

        public string Link { get; set; }

        public ResourceType ResourceType { get; set; }

        [ForeignKey("Id")]
        public virtual ICollection<LessonResource> LessonResources { get; set; }
    }

    public enum ResourceType
    {
        Video = 1,
        Document = 2,
        PPT = 3,
        Zip = 4
    }
}
