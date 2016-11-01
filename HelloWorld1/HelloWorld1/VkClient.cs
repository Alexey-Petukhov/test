using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace vkSmartWall
{
    public class VkClient
    {
        VkAPI vkApi;
        VkClientConverter vkClientConverter;
        public VkClient(VkAPI vkApi)
        {
            this.vkApi = vkApi;
            vkClientConverter = new VkClientConverter(/*vkApi*/);

        }

        public List<AppUser> GetMembers(String groupId)
        {
            var tmp = vkApi.GetMembers(groupId);

            List<AppUser> users = new List<AppUser>();
            AppGroup appGroup = new AppGroup(groupId);
            appGroup.Count = tmp.response.count;

            users = GetUsersInfo(tmp.response.items);
            return users;
        }

        public AppUser GetUserById(String uid) // of AppUser by uid
        {
            VkApiUsers vkApiUser = vkApi.GetUserById(uid);
            
            AppUser appUser = null;
            foreach (var u in vkApiUser.response) // in fact just vkApiUser.response[0]
            {
                appUser = vkClientConverter.Convert(u);
                
            }
            return appUser;

        }

        public List<AppUser> GetUsersInfo(List<Member> members) // of list of users by uids
        {
            List<AppUser> users = new List<AppUser>();
            
            int cnt = 1;
            AppUser appUser;
            foreach (var member in members)
            {
                Console.WriteLine(cnt++); // для вывода в консоль порядкового номера человека (визуализация)
                //users.Add(CreateAppUser(member));
                appUser = vkClientConverter.Convert(member);
                appUser.Friends = GetFriends(appUser.Uid.ToString());
                //appUser.Wall = GetWall(appUser.Uid.ToString());
                users.Add(appUser);
                //// первые 25 человек в группе - чтобы не долго ждать.
                ////if (users.Count == 25) return users;
            }

            return users;

        }

        public List<AppUser> GetFriends(String uid) // of AppUser by uid
        {
            VkApiFriends vkApiFriends = vkApi.GetFriends(uid);
            List<AppUser> friends = new List<AppUser>();
            if (vkApiFriends.response != null) // AppUser page is hidden or deleted
            {
                friends = vkClientConverter.Convert(vkApiFriends);
                
            } // else AppUser hasn't friends (hiden friend list)

            return friends;

        }


        public AppWall GetWall(String uid)
        {
            return GetWall(uid, int.MaxValue);
        }

        public AppWall GetWall(String uid, int count) // of AppUser by uid
        {

            int _limit_count = 100;
            int _offset = 0;
            int cnt = 0;
            VkApiWall vkApiWall;
            AppWall appWall = new AppWall();
            AppWall limitAppWall = new AppWall();
            AppWallItem wItem = null;

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
                vkApiWall = vkApi.GetLimitedWallItems(uid, _limit_count, _offset);
                if (vkApiWall != null && vkApiWall.response != null)
                {
               
                    if (all) needCnt = vkApiWall.response.count;
                    //else needCnt = Count;

                    cnt = (needCnt - _offset - 1) / _limit_count;
                    limitAppWall = vkClientConverter.Convert(vkApiWall);
                    appWall.Items.AddRange(limitAppWall.Items);
                    
                    _offset += _limit_count;
                    count = count - _limit_count;
                } // else стена закрыта - записей не получить. => выход из цикла.
            } while (cnt > 0);
            return appWall;
        }

        public AppWall GetNews(AppUser appUser)
        {
            return GetNews(appUser, 10);
        }

        public AppWall GetNews(AppUser appUser, int postCountFromFriend) // возвращает для пользователя его новости - записи со стен друзей.
        {
            List<AppUser> friends = appUser.Friends;
            int cnt = 0;
            foreach (var friend in friends)
            {
                
                AppWall tmp = GetWall(friend.Uid.ToString(), postCountFromFriend);
                // experemental smart sorting {
                tmp.Items = tmp.Items.OrderByDescending(o => o.AppLikes.Count).ToList();
                int lpCnt = 0;
                foreach (var t in tmp.Items)
                {
                    lpCnt++;
                    t.LikesPriority = lpCnt;
                }
                // }
                friend.Wall = tmp;
                cnt++;
                Console.WriteLine(cnt);
            } // у каждого друга пользователя появилась стена.

            AppWall news = new AppWall();
            foreach (var friend in friends)
            {
                news.Items.AddRange(friend.Wall.Items);
            }
            return news;
        }
    }
}
