using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vkSmartWall
{


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

    public class VkApiFriends
    {
        public FriendsList response { get; set; }
    }
}
