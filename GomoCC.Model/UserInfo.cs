using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace GomoCC.Model
{
    public partial class UserInfo
    {
        public int Id { get; set; }

        [Column("User_Id")]
        public string UserId { get; set; }

        public string Uname { get; set; }
        public string Pwd { get; set; }
        public int DelFlag { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime SubTime { get; set; }
    }
}
