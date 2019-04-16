using System;
using System.Collections.Generic;

namespace WTG_94239.Model.EF
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Account1 { get; set; }
        public string Password { get; set; }
        public string Md5password { get; set; }
        public bool? IsBanned { get; set; }

        public virtual MemberInfo IdNavigation { get; set; }
    }
}
