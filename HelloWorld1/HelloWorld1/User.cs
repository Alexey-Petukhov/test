using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace vkSmartWall
{
    class User
    {
        private int uid = 0;
        private String first_name = "";
        private String last_name = "";
        private List<User> friends = null;

        public User(int uid)
        {
            this.uid = uid;
        }


        public String GetFirstname()
        {
            return this.first_name;
        }

        public void SetFirstname(String first_name)
        {
            this.first_name = first_name;
        }

        public String GetLastname()
        {
            return this.last_name;
        }

        public void SetLastname(String last_name)
        {
            this.last_name = last_name;
        }

        public List<User> GetFriends()
        {
            return this.friends;
        }

        public void SetFriends(List<User> friends)
        {
            this.friends = friends;
        }

        public int GetUid()
        {
            return this.uid;
        }

        public void SetUid(int uid)
        {
            this.uid = uid;
        }

   
    }

    public class UserBaseInfo
    {
        public int uid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }

    public class RootUserBaseInfo
    {
        public List<UserBaseInfo> response { get; set; }
    }

    public class UsersBaseInfo
    {
        public int uid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int? hidden { get; set; }
        public string deactivated { get; set; }
    }

    public class RootUsersBaseInfo
    {
        public List<UsersBaseInfo> response { get; set; }
    }



    public class Friend
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string deactivated { get; set; }
        public int? hidden { get; set; }
        public List<int?> lists { get; set; }
    }

    public class FriendsList
    {
        public int count { get; set; }
        public List<Friend> items { get; set; }
    }

    public class RootFriendsList
    {
        public FriendsList response { get; set; }
    }

}
