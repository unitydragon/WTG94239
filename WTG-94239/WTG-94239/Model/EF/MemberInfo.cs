using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace WTG_94239.Model.EF
{
    public partial class MemberInfo
    {
        public int Id { get; set; }
        public string TrueName { get; set; }
        public string EMail { get; set; }
        [JsonIgnore]
        public virtual Account IdNavigation { get; set; }
    }
}
