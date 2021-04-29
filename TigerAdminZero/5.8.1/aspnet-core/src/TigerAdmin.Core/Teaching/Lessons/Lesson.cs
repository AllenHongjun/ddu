using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TigerAdmin.Teaching.Missions;
using TigerAdmin.Teaching.Resources;

namespace TigerAdmin.Teaching.Lessons
{
    public class Lesson:FullAuditedEntity<Guid>
    {
        public string Title { get; set; }

        public string Intro { get; set; }

        public string Content { get; set; }

        public string VideoLink { get; set; }


        public virtual ICollection<Mission> Missions { get; set; }

        public virtual ICollection<LessonResource> LessonResources { get; set; }


    }
}
