// A Hello World! program in C#.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace vkSmartWall
{
    class vkSmartWall
    {
        static void Main()
        {
            String gId = "csu_iit";
            

            //String url = "https://api.vk.com/method/users.get?user_ids=42560016,71985644,mr.pavlichenkov";
            //String url = "https://api.vk.com/method/groups.getMembers?group_id=csu_iit";//&sort=time_asc&v=5.53"; // список пользователей группы
            //String url = "https://api.vk.com/method/friends.get?user_id=42560016"; // список друзей пользователя
            //String url = "https://api.vk.com/method/wall.get?domain=mr.pavlichenkov"; //owner_id=71985644"; количество и контент стены пользователя.

            VkAPI vkAPI = new VkAPI();
            //Console.WriteLine(vkAPI.GetMembers(gId));
            //File.WriteAllText(@"D:\Documents\Visual Studio 2013\Projects\HelloWorld1\members.txt", vkAPI.GetMembers(gId));

           // Console.WriteLine(vkAPI.GetUserById(uid2));
            //File.WriteAllText(@"D:\Documents\Visual Studio 2013\Projects\HelloWorld1\usr2.txt", vkAPI.GetUserById(uid2));
     
            Console.WriteLine("----------------------------------------------------------------");

            ////RootGroupMembers memb = JsonConvert.DeserializeObject<RootGroupMembers>(vkAPI.GetMembers(gId));
            ////RootUserBaseInfo uBaseInfo = null;
            ////int counter = 0;
            ////for (int i = 0; i < memb.response.count; ++i)
            ////{
            ////    counter++;
            ////    uBaseInfo = JsonConvert.DeserializeObject<RootUserBaseInfo>(vkAPI.GetUserById(memb.response.users[i].ToString()));  //
            ////    //Console.WriteLine("(№ " + counter + ") id = " + memb.response.users[i] + " - " + uBaseInfo.response[0].first_name + " " + uBaseInfo.response[0].last_name);


                
            ////    //File.AppendAllText/*WriteAllText*/(@"D:\Documents\Visual Studio 2013\Projects\HelloWorld1\members.txt",
            ////    //    "(№ " + counter + ") id = " + memb.response.users[i] + " - " + uBaseInfo.response[0].first_name + " " + uBaseInfo.response[0].last_name + "\r\n");
            ////}
            Console.WriteLine("================================================================");

            String uid1 = "42560016";
            String uid2 = "71985644";
            String uid3 = "53516640";
            String uid4 = vkAPI.GetUserById("mr.pavlichenkov").GetUid().ToString();


            //List<WallItem> wallItems = vkAPI.GetWallItems(uid2, 10);
            //String str = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(1475332312).ToString();
            //wallItems = vkAPI.GetWallItems(uid2, 46);
            //wallItems = vkAPI.GetWallItems(uid2, 100);
            //wallItems = vkAPI.GetWallItems(uid2, 101);
            //wallItems = vkAPI.GetWallItems(uid2, 105);
            //wallItems = vkAPI.GetWallItems(uid2, 120);

            User testUser = vkAPI.GetUserById(uid2);
            testUser.SetFriends(vkAPI.GetFriends(testUser.GetUid().ToString()));

            
            //List<WallItem> userNews = vkAPI.GetNews(testUser);
            ////userNews = userNews.OrderByDescending(o => o.GetLikes().count).ToList();
            //// сначала сортировка по приоритетам, затем по максимальному количеству лайков.
            //userNews = userNews.OrderBy(o => o.GetLikesPriority()).ThenByDescending(o => o.GetLikes().count).ToList();

            
            //userNews.Sort(delegate(WallItem x, WallItem y)
            //{
            //    /*if (x.GetLikes() == null && y.GetLikes() == null) return 0;
            //    else if (x.GetLikes() == null) return -1;
            //    else if (y.GetLikes() == null) return 1;
            //    else */ return x.GetLikes().CompareTo(y.GetLikes());
            //});

            
            Group group = new Group(gId);
            group.SetUsers(vkAPI.GetMembers(group.GetGroupId().ToString())); // gets group members with their friends
            
            //// запись в файл дерева -----------------------------------------------------------------------------
            int uCounter = 0;
            int fCounter = 0;
            foreach (var memb in group.GetUsers())
            {
                uCounter++;
                File.AppendAllText(@"..\..\members+friends.txt",
                    "~(№ " + uCounter + ") id = " + memb.GetUid() +
                    " - " + memb.GetFirstname() + " " + memb.GetLastname() +
                    ". Friends count:" + memb.GetFriends().Count + "\r\n");
                fCounter = 0;
                foreach (var friend in memb.GetFriends())
                {
                    fCounter++;
                    File.AppendAllText(@"..\..\members+friends.txt",
                        "-----(№ " + fCounter + ") id = " + friend.GetUid() + " - " +
                        friend.GetFirstname() + " " +
                        friend.GetLastname() + "\r\n");
                }
            }
            
            ////------------------------------------------------------------------------------------------------------------

            int cnt = 0;
            foreach (var u in group.GetUsers())
            {
                cnt++;
                Console.WriteLine("(№ " + cnt + ") id = " + u.GetUid() + " --- " + u.GetFirstname() + " " + u.GetLastname());
            }
            Console.WriteLine("Теперь введите id пользователя, чтобы получить список его друзей:");
            String neededId = Console.ReadLine();
            //User us = new User();
            User usr = group.GetUsers().Find(x => x.GetUid() == int.Parse(neededId));//vkAPI.GetUserById(neededId);
            /// !!! список друзей
            
            List<User> friends = usr.GetFriends();//vkAPI.GetFriends(neededId);
            Console.WriteLine("Друзья пользователя " + usr.GetFirstname() + " " + usr.GetLastname() + "");
            foreach (var friend in friends)
            {
                Console.WriteLine("---" + friend.GetFirstname() + " " + friend.GetLastname());
            }
            /// !!! - список новостей

            List<WallItem> userNews = vkAPI.GetNews(usr);
            //userNews = userNews.OrderByDescending(o => o.GetLikes().count).ToList();
            // сначала сортировка по приоритетам, затем по максимальному количеству лайков.
            userNews = userNews.OrderBy(o => o.GetLikesPriority()).ThenByDescending(o => o.GetLikes().count).ToList();

            Console.WriteLine("Новости пользователя " + testUser.GetFirstname() + " " + testUser.GetLastname());
            int newsCnt = 0;
            int chCnt = 0;
            int atCnt = 0;
            //File.Create(@"..\..\newsForTestUser.txt");
            foreach (var newsItem in userNews)
            {
                newsCnt++;

                File.AppendAllText(@"..\..\newsForTestUser.txt", "№ " + newsCnt + " +-- newsText: " + newsItem.GetText() + "\r\n" +
                    "     --- id владельца: " + newsItem.GetOwnerId() + "\r\n" +
                    "     --- date: " + new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(newsItem.GetDate()).ToString() + "\r\n" +
                    "     --- likes: " + newsItem.GetLikes().count + "\r\n" +
                    "     --- reposts: " + newsItem.GetReposts().count + "\r\n" +
                    "     --- comments: " + newsItem.GetComments().count + "\r\n"
                    );


                //Console.WriteLine("№ " + newsCnt + " +-- newsText: " + newsItem.GetText());
                //Console.WriteLine("     --- id владельца: " + newsItem.GetOwnerId());
                //Console.WriteLine("     --- date: " + new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(newsItem.GetDate()).ToString());
                //Console.WriteLine("     --- likes: " + newsItem.GetLikes().count);
                //Console.WriteLine("     --- reposts: " + newsItem.GetReposts().count);
                //Console.WriteLine("     --- comments: " + newsItem.GetComments().count);
                if (newsItem.GetAttachment2() != null)
                {
                    File.AppendAllText(@"..\..\newsForTestUser.txt", "     --- newsAttachments: " + "\r\n");

                    //Console.WriteLine("     --- newsAttachments: ");
                    atCnt = 0;
                    foreach (var attachment2 in newsItem.GetAttachment2())
                    {
                        atCnt++;
                        File.AppendAllText(@"..\..\newsForTestUser.txt", "     --- --- Вложение № " + atCnt + "\r\n" +
                            "     --- ~~~ тип вложения " + attachment2.type + "\r\n"
                            );


                        //Console.WriteLine("     --- --- Вложение № " + chCnt);
                        //Console.WriteLine("     --- ~~~ тип вложения " + attachment2.type);
                        if (attachment2.type.Equals("photo"))
                        {
                            File.AppendAllText(@"..\..\newsForTestUser.txt", "     --- ~~~ фото: " + attachment2.photo.photo_130 + "\r\n");
                            //Console.WriteLine("     --- ~~~ фото: " + attachment2.photo.photo_130);
                        }
                        else if (attachment2.type.Equals("audio"))
                        {
                            File.AppendAllText(@"..\..\newsForTestUser.txt", "     --- ~~~ аудио: " + attachment2.audio.url + "\r\n");
                            //Console.WriteLine("     --- ~~~ аудио: " + attachment2.audio.url);
                        }

                    }
                }

                if (newsItem.GetCopyHistory() != null)
                {
                    File.AppendAllText(@"..\..\newsForTestUser.txt", "     --- newsCopyHistory: " + "\r\n");
                    //Console.WriteLine("     --- newsCopyHistory: ");
                    chCnt = 0;
                    foreach (var copyHistoryItem in newsItem.GetCopyHistory())
                    {
                        chCnt++;
                        File.AppendAllText(@"..\..\newsForTestUser.txt", "     --- --- Вложение № " + chCnt + "\r\n" +
                            "     --- ~~~ CHText: " + copyHistoryItem.text + "\r\n" +
                            "     --- ~~~ CHOwnerId: " + copyHistoryItem.owner_id + "\r\n" +
                            "     --- ~~~ CHDate: " + new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(copyHistoryItem.date).ToString() + "\r\n"
                            );
                        //Console.WriteLine("     --- --- Вложение № " + chCnt);
                        //Console.WriteLine("     --- ~~~ CHText: " + copyHistoryItem.text);
                        //Console.WriteLine("     --- ~~~ CHOwnerId: " + copyHistoryItem.owner_id);
                        //Console.WriteLine("     --- ~~~ CHDate: " + new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(copyHistoryItem.date).ToString());

                    }

                }
            }
            //List<WallItem> userNews = vkAPI.

                //File.WriteAllText(@"D:\Documents\Visual Studio 2013\Projects\HelloWorld1\usr1Friends.txt", vkAPI.GetFriends(uid1));
            //FriendsIdsList friendIds = JsonConvert.DeserializeObject<FriendsIdsList>(vkAPI.GetFriends(uid1));
            //List<User> friends = new List<User>();
            //foreach (var u in friendIds.response)
            //{
                
            //    friends.Add(vkAPI.GetUserById(u.ToString()));
            //}
            //User usr = vkAPI.GetUserById(uid1);
            //Console.WriteLine("Друзья пользователя " + usr.GetFirstname() + " " + usr.GetLastname() + "");
            //foreach (var friend in friends)
            //{
            //   Console.WriteLine("---" + friend.GetFirstname() + " " + friend.GetLastname());
            //}
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }

}