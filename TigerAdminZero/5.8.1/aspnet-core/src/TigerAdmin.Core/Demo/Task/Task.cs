using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TigerAdmin.Demo
{
    [Table("AppTasks")]
    public class Task : Entity<int>, IHasCreationTime
    {
        //I derived from ABP's base Entity class, which includes Id property as int by default. We can use the generic version, Entity<TPrimaryKey>, to choice a different PK type.
        //IHasCreationTime is a simple interface just defines CreationTime property(it's good to use a standard name for CreationTime).
        //Task entity defines a required Title and an optional Description.
        //Clock.Now returns DateTime.Now by default. But it provides an abstraction, so we can easily switch to DateTime.UtcNow in the feature if it's needed. Always use Clock.Now instead of DateTime.Now while working with ABP framework.



        // 官网 task单表的CRUD作为基本的练习
        public const int MaxTitleLength = 256;

        public const int MaxDescriptionLength = 64 * 1024; // 64KB

        [Required]
        [StringLength(MaxTitleLength)]
        public string Title { get; set; }

        [StringLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public DateTime CreationTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public TaskState State { get; set; }

        public Task()
        {
            CreationTime = Clock.Now;
            State = TaskState.Open;
        }

        public Task(string title, string description = null):this()
        {
            Title = title;
            Description = description;
        }
    }

    public enum TaskState: byte
    {
        Open = 0,
        Complete = 1
    }
}
