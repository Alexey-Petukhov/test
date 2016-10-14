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
    class VkAPI
    {
        private String baseUrl = "";
        private String vers = "";

        public VkAPI()
        {
            this.baseUrl = "https://api.vk.com/method/";
            this.vers = "&v=5.58";
        }

        public List<User> GetMembers(String groupId) // of group by groupId or groupName
        {
            String fields = "&fields=1";
            String url = baseUrl + "groups.getMembers?group_id=" + groupId + fields + vers;
            String jsonAnswer = DoReqGet(url); // list of members ids

            RootGroupMembers memb = JsonConvert.DeserializeObject<RootGroupMembers>(jsonAnswer);
            List<User> users = new List<User>();
            Group group = new Group(groupId);
            group.SetCount(memb.response.count);

            users = GetUsersInfo(memb.response.items);
           
            group.SetUsers(users);
            return group.GetUsers(); // 
            
        }

        public List<User> GetUsersInfo(List<Member> members) // of list of users by uids
        {

            //var postData = "";
            //foreach (var uid in uids)
            //{
            //    postData += "," + uid.ToString();
            //}
            //postData = "user_ids=" + postData.Substring(1); // delete first ","
            //String jsonAnswer = DoReqPost(baseUrl + "users.get", postData);

            //RootUsersBaseInfo usersBaseInfo = JsonConvert.DeserializeObject<RootUsersBaseInfo>(jsonAnswer);
            List<User> users = new List<User>();
            User user = null;
            int cnt = 1;
            foreach (var u in members)
            {
                Console.WriteLine(cnt++); // для вывода в консоль порядкового номера человека (визуализация)
                user = new User(u.id);
                user.SetFirstname(u.first_name);
                user.SetLastname(u.last_name);
                user.SetFriends(GetFriends(user.GetUid().ToString()));
                //user.SetWall(GetWallItems(user.GetUid().ToString() ));
                users.Add(user);
            }

            return users;

        }

        public User GetUserInfo(String uid) // of user by uid
        {
            String url = baseUrl + "users.get?user_ids=" + uid + vers;
            String jsonAnswer = DoReqGet(url);
            RootUserBaseInfo uBaseInfo = JsonConvert.DeserializeObject<RootUserBaseInfo>(jsonAnswer);
            User user = null;
            foreach (var u in uBaseInfo.response) // in fact just uBaseInfo.response[0]
            {
                user = new User(u.id);
                //user.SetUid(u.uid);
                user.SetFirstname(u.first_name);
                user.SetLastname(u.last_name);
            }
            return user;

        }


        public List<User> GetFriends(String uid) // of user by uid
        {
            String fields = "&fields=1";
            String url = baseUrl + "friends.get?user_id=" + uid + fields + vers;
            String jsonAnswer = DoReqGet(url);
            RootFriendsList friendsList = JsonConvert.DeserializeObject<RootFriendsList>(jsonAnswer);
            List<User> friends = new List<User>();
            User user = null;
            if (friendsList.response != null) // user page is hidden or deleted
            {
                foreach (var u in friendsList.response.items)
                {
                    user = new User(u.id);
                    user.SetFirstname(u.first_name);
                    user.SetLastname(u.last_name);
                    friends.Add(user);
                }
            } // else user hasn't friends (hiden friend list)

            return friends;

        }


        public List<WallItem> GetWallItems(String uid)
        {
            return GetWallItems(uid, int.MaxValue);
        }
        
        public  List<WallItem> GetWallItems(String uid, int count) // of user by uid
        {
            //uid = GetUserInfo(uid).GetUid().ToString(); // получает id числовое, даже из домейна
            
            int _limit_count = 100;
            int _offset = 0;
            int cnt = 0;
            String limitWallItemsInJson;
            RootWallItems rootWall;
            List<WallItem> wallItems = new List<WallItem>();
            WallItem wItem = null;
            
            int needCnt = 0;
            bool all = true; // все ли записи нужны?
            if (count != int.MaxValue) // нужны не все записи
            {
                all = false;
                needCnt = count;
            } 
            

            do
            {
                if (count < _limit_count) _limit_count = count;
                limitWallItemsInJson = GetLimitWallItemsInJson(uid, _limit_count, _offset);
                rootWall = JsonConvert.DeserializeObject<RootWallItems>(limitWallItemsInJson); // i-ая порция записей со стены пользователя
                if (rootWall != null && rootWall.response != null)
                {
                    if (all) needCnt = rootWall.response.count;
                    //else needCnt = count;

                    cnt = (needCnt - _offset - 1) / _limit_count;
                    foreach (var item in rootWall.response.items)
                    {
                        wItem = new WallItem();
                        wItem.SetId(item.id);
                        wItem.SetFromId(item.from_id);
                        wItem.SetOwnerId(item.owner_id);
                        wItem.SetDate(item.date);
                        wItem.SetText(item.text);
                        wItem.SetLikes(item.likes);
                        wItem.SetReposts(item.reposts);
                        wItem.SetComments(item.comments);
                        wItem.SetCopyHistory(item.copy_history);
                        wItem.SetAttachment2(item.attachments);

                        wallItems.Add(wItem);
                    }
                    _offset += _limit_count;
                    count = count - _limit_count;
                } // else стена закрыта - записей не получить. => выход из цикла.
            } while (cnt > 0);

            return wallItems;

        }

        private String GetLimitWallItemsInJson(String uid, int _limit_count, int _offset)
        {
            String filter = "&filter=owner";
            String count = "&count=" + _limit_count.ToString();
            String offset = "&offset=" + _offset.ToString();
            String url = baseUrl + "wall.get?owner_id=" + uid + filter + count + offset + vers;
            String jsonAnswer = DoReqGet(url);

            return jsonAnswer;
        }

        public List<WallItem> GetNews(/*String uid*/User user) // возвращает для пользователя его новости - записи со стен друзей.
        {
            //List<User> friends = GetFriends(uid);
            List<User> friends = user.GetFriends();
            int cnt = 0;
            foreach (var friend in friends)
            {
                // experemental smart sorting {
                List<WallItem> tmp = GetWallItems(friend.GetUid().ToString(), 10);
                tmp = tmp.OrderByDescending(o => o.GetLikes().count).ToList();
                int lpCnt = 0;
                foreach (var t in tmp)
                {
                    lpCnt++;
                    t.SetLikesPriority(lpCnt);
                }
                // }
                friend.SetWall(tmp);
                cnt++;
                Console.WriteLine(cnt);
            } // у каждого друга пользователя появилась стена.

            List<WallItem> news = new List<WallItem>();
            foreach (var friend in friends)
            {
                news.AddRange(friend.GetWall());
            }
            return news;
        }

       
        public String DoReqGet(String url) // do GET
        {
            WebRequest gReq = WebRequest.Create(url);
            WebResponse gResp = gReq.GetResponse();
            var responseString = new StreamReader(gResp.GetResponseStream()).ReadToEnd();
            return responseString;
        }

        public String DoReqPost(String url, String postData) // do POST
        {
            WebRequest pReq = WebRequest.Create(url);
            var data = Encoding.ASCII.GetBytes(postData);
            pReq.Method = "POST";
            pReq.ContentType = "application/x-www-form-urlencoded";
            pReq.ContentLength = data.Length;
            using (var stream = pReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            WebResponse pResp = pReq.GetResponse();
            var responseString = new StreamReader(pResp.GetResponseStream()).ReadToEnd();
            return responseString;
        }
    }
}
