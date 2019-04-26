using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace WTG_94239.Model.EF
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Account1 { get; set; }
        public string Password { get; set; }
        public string Md5password { get; set; }
        public bool? IsBanned { get; set; }
        public string SiteUserName { get; set; }
        [JsonIgnore]
        public virtual MemberInfo MemberInfo { get; set; }
    }
}
