using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TigerAdmin.Teaching.Categorys
{
    /// <summary>
    /// 课程分类
    /// </summary>
    public class Category:AuditedEntity<Guid>
    {
        public string Name { get; set; }


        public Guid? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Parent { get; set; }
    }
}
