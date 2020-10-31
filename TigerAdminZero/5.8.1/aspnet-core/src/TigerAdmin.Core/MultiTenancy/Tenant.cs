using Abp.MultiTenancy;
using Abp.Timing;
using System;
using System.ComponentModel.DataAnnotations;
using TigerAdmin.Authorization.Users;

namespace TigerAdmin.MultiTenancy
{
    /// <summary>
    /// 租户类扩展
    /// </summary>
    public class Tenant : AbpTenant<User>
    {
        public const int MaxLogoMimeTypeLength = 64;

        public DateTime? SubscriptionEndDateUtc { get; set; }

        public bool IsInTrialPeriod { get; set; }

        public virtual Guid? CustomCssId { get; set; }

        public virtual Guid? LogoId { get; set; }

        [MaxLength(MaxLogoMimeTypeLength)]
        public virtual string LogoFileType { get; set; }


        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }

        public virtual bool HasLogo()
        {
            return LogoId != null && LogoFileType != null;
        }

        public void ClearLogo()
        {
            LogoId = null;
            LogoFileType = null;
        }

        private bool IsSubScriptionEnded()
        {
            return SubscriptionEndDateUtc < Clock.Now.ToUniversalTime();
        }

        /// <summary>
        /// 计算剩余订阅时间
        /// </summary>
        /// <returns></returns>
        public int CalculateRemainingDayCount()
        {
            return SubscriptionEndDateUtc != null ? (SubscriptionEndDateUtc.Value - Clock.Now.ToUniversalTime()).Days : 0;
        }

        /// <summary>
        /// 是否无限制订阅时间
        /// </summary>
        /// <returns></returns>
        public bool HasUnlimitedTimeSubscription()
        {
            return SubscriptionEndDateUtc == null;
        }

    }
}
