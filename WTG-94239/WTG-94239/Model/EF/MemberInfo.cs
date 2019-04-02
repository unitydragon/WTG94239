using System;
using System.Collections.Generic;

namespace WTG_94239.Model.EF
{
    public partial class MemberInfo
    {
        public MemberInfo()
        {
            Account = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
