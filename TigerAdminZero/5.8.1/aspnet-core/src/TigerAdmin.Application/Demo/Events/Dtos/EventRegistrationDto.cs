using System;
using System.Collections.Generic;
using System.Text;

namespace TigerAdmin.Demo.Events
{
    public class EventRegistrationDto
    {
        public virtual Guid EventId { get; protected set; }

        public virtual long UserId { get; protected set; }

        public virtual string UserName { get; protected set; }

        public virtual string UserSurname { get; protected set; }

        // 项目如何连接多个库
        // swagger 如何分成多个网页来显示
        // swagger 生成的接口如何导入到postman
    }
}
