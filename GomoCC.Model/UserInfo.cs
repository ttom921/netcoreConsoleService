using System;
using System.Collections.Generic;
using System.Text;

namespace GomoCC.Model
{
    public partial class UserInfo
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Uname { get; set; }
        public string Pwd { get; set; }
        public int DelFlag { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime SubTime { get; set; }
    }
}
