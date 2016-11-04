using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;


namespace vkSmartWall
{
    public class VkAPI
    {
        private String baseUrl = "";
        private String vers = "";

        public VkAPI()
        {
            this.baseUrl = "https://api.vk.com/method/";
            this.vers = "&v=5.58";
        }

        public VkApiGroup GetMembers(String groupId) // of group by groupId or groupName
        {
            var fields = "&fields=1";
            var url = baseUrl + "groups.getMembers?group_id=" + groupId + fields + vers;
            var jsonAnswer = DoReqGet(url); // list of members ids

            var memb = JsonConvert.DeserializeObject<VkApiGroup>(jsonAnswer);
            return memb; // 

        }

        

        public VkApiUsers GetUserById(String uid) // of user by uid
        {
            var url = baseUrl + "users.get?user_ids=" + uid + vers;
            var jsonAnswer = DoReqGet(url);
            var vkApiUser = JsonConvert.DeserializeObject<VkApiUsers>(jsonAnswer);

            return vkApiUser;
        }


        public VkApiFriends GetFriends(String uid) // of user by uid
        {
            var fields = "&fields=1";
            var url = baseUrl + "friends.get?user_id=" + uid + fields + vers;
            var jsonAnswer = DoReqGet(url);
            var friends = JsonConvert.DeserializeObject<VkApiFriends>(jsonAnswer);

            return friends;

        }


        public VkApiWall GetLimitedWallItems(String uid, int _limit_count, int _offset)
        {
            var filter = "&filter=owner";
            var count = "&count=" + _limit_count.ToString();
            var offset = "&offset=" + _offset.ToString();
            var url = baseUrl + "wall.get?owner_id=" + uid + filter + count + offset + vers;
            var jsonAnswer = DoReqGet(url);

            var rootWall = JsonConvert.DeserializeObject<VkApiWall>(jsonAnswer);

            return rootWall;
        }

       

       
        public String DoReqGet(String url) // do GET
        {
            string reply;
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                reply = client.DownloadString(url);
            }
            return reply;
        }

    }
}
