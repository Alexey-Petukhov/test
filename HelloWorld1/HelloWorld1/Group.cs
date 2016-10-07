using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace vkSmartWall
{
    class Group
    {
        //private String groupNmae = "csu_iit";
        //private int groupId;
        private List<User> users;
        private int count = 0;
        private String groupName;

        //public Group(int groupId)
        //{
        //    this.groupId = groupId;
        //}

        public Group(string groupName)
        {
            this.groupName = groupName;
        }

        public List<User> GetUsers()
        {
            return this.users;
        }

        public void SetUsers(List<User> users)
        {
            this.users = users;
        }

        public int GetCount()
        {
            return this.count;
        }

        public void SetCount(int count)
        {
            this.count = count;
        }

        public String GetGroupId()
        {
            return this.groupName;
        }

        public void SetGroupId(String groupName)
        {
            this.groupName = groupName;
        }

        /*public User GetMembers()
        {

            return null;
        } */

    }

    //public class GroupMembers
    //{
    //    public int count { get; set; }
    //    public List<int> users { get; set; }
    //}

    //public class RootGroupMembers
    //{
    //    public GroupMembers response { get; set; }
    //}

    public class Member
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int? hidden { get; set; }
        public string deactivated { get; set; }
    }

    public class GroupMembers
    {
        public int count { get; set; }
        public List<Member> items { get; set; }
    }

    public class RootGroupMembers
    {
        public GroupMembers response { get; set; }
    }
}
