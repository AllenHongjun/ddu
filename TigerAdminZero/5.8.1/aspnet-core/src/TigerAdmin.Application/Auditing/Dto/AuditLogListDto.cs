using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TigerAdmin.Auditing.Dto
{
    public class AuditLogListDto:EntityDto<long>
    {
        public long? UserId { get; set; }

        public string UserName { get; set; }

        /// <summary>
        /// 模拟租户ID
        /// </summary>
        public int? ImpersonatorTenantId { get; set; }

        /// <summary>
        /// 模拟用户ID
        /// </summary>
        public long? ImpersonatorUserId { get; set; }

        public string ServiceName { get; set; }

        public string MethodName { get; set; }

        public string Parameters { get; set; }

        public DateTime ExecutionTime { get; set; }

        public int ExecutionDuration { get; set; }

        public string ClientIpAddress { get; set; }

        public string ClientName { get; set; }

        public string BrowserInfo { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// 自定义信息
        /// </summary>
        public string CustomData { get; set; }
    }
}
