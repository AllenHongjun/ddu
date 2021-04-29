using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TigerAdmin.Teaching.Lessons;

namespace TigerAdmin.Teaching.Resources
{
    public class LessonResource:Entity<Guid>
    {
        public Guid LessonId { get; set; }

        public Guid ResourceId { get; set; }

        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }

        [ForeignKey("ResourceId")]
        public virtual Resource Resource { get; set; }
    }
}
