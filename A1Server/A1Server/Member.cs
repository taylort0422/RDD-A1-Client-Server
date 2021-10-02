using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace A1Server
{
    public class Member
    {
        public int MemberID
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public DateTime DateOfBirth
        {
            get;
            set;
        }
    }

    public class Members
    {
        [JsonProperty("allMembers")]
        public List<Member> allMembers { get; set; }
    }
}
