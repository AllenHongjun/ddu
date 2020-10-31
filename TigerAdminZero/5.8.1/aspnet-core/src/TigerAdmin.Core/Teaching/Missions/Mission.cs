using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace TigerAdmin.Teaching.Missions
{
    /// <summary>
    /// 课程任务
    /// </summary>
    public class Mission:FullAuditedEntity<Guid>
    {
        public string Title { get; set; }

        public string Intro { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        public MissionType Type { get; set; }

        public string VideoLink { get; set; }
    }

    public enum MissionType
    {
        Lab = 1,
        HomeWork = 2,
        /// <summary>
        /// 小项目
        /// </summary>
        Project = 3,
        Exam = 4,
        /// <summary>
        /// 讨论
        /// </summary>
        Discussion = 5
    }
}
