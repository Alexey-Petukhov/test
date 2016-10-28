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
    
            VkAPI vkAPI = new VkAPI();
            var vkClient = new VkClient(vkAPI);
            
            Console.WriteLine("----------------------------------------------------------------");

            String uid1 = "42560016";
            String uid2 = "71985644";
            String uid3 = "53516640";
            String uid4 = vkClient.GetUserById("mr.pavlichenkov").GetUid().ToString();

            var wall = new AppWall();
            wall = vkClient.GetWall(uid2);
            //List<AppWallItem> wallItems = vkAPI.GetWall(uid2, 10);
            //String str = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(1475332312).ToString();
            //wallItems = vkAPI.GetWall(uid2, 46);
            //wallItems = vkAPI.GetWall(uid2, 100);
            //wallItems = vkAPI.GetWall(uid2, 101);
            //wallItems = vkAPI.GetWall(uid2, 105);
            //wallItems = vkAPI.GetWall(uid2, 120);

            AppUser testAppUser = vkClient.GetUserById(uid2);
            testAppUser.SetFriends(vkClient.GetFriends(testAppUser.GetUid().ToString()));
            
            //List<AppWallItem> userNews = vkAPI.GetNews(testAppUser);
            ////userNews = userNews.OrderByDescending(o => o.GetLikes().count).ToList();
            //// сначала сортировка по приоритетам, затем по максимальному количеству лайков.
            //userNews = userNews.OrderBy(o => o.GetLikesPriority()).ThenByDescending(o => o.GetLikes().count).ToList();


            
            AppGroup appGroup = new AppGroup(gId);
            appGroup.Users = vkClient.GetMembers(appGroup.GroupName); // gets AppGroup members with their friends
            
            //// запись в файл дерева -----------------------------------------------------------------------------
            int uCounter = 0;
            int fCounter = 0;
            foreach (var memb in appGroup.Users)
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
            foreach (var u in appGroup.Users)
            {
                cnt++;
                Console.WriteLine("(№ " + cnt + ") id = " + u.GetUid() + " --- " + u.GetFirstname() + " " + u.GetLastname());
            }
            Console.WriteLine("Теперь введите id пользователя, чтобы получить список его друзей:");
            String neededId = Console.ReadLine();
            //AppUser us = new AppUser();
            AppUser usr = appGroup.Users.Find(x => x.GetUid() == int.Parse(neededId));//vkAPI.GetUserById(neededId);
            /// !!! список друзей
            
            List<AppUser> friends = usr.GetFriends();//vkAPI.GetFriends(neededId);
            Console.WriteLine("Друзья пользователя " + usr.GetFirstname() + " " + usr.GetLastname() + "");
            foreach (var friend in friends)
            {
                Console.WriteLine("---" + friend.GetFirstname() + " " + friend.GetLastname());
            }
            /// !!! - список новостей

            AppWall userNews = vkClient.GetNews(usr);
            //userNews = userNews.OrderByDescending(o => o.GetLikes().count).ToList();
            // сначала сортировка по приоритетам, затем по максимальному количеству лайков.
            userNews.Items = userNews.Items.OrderBy(o => o.LikesPriority).ThenByDescending(o => o.GetLikes().count).ToList();

            Console.WriteLine("Новости пользователя " + testAppUser.GetFirstname() + " " + testAppUser.GetLastname());
            int newsCnt = 0;
            int chCnt = 0;
            int atCnt = 0;
            //File.Create(@"..\..\newsForTestUser.txt");
            foreach (var newsItem in userNews.Items)
            {
                newsCnt++;

                File.AppendAllText(@"..\..\newsForTestUser.txt", "№ " + newsCnt + " +-- newsText: " + newsItem.Text + "\r\n" +
                    "     --- id владельца: " + newsItem.OwnerId + "\r\n" +
                    "     --- date: " + new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(newsItem.Date).ToString() + "\r\n" +
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
            //List<AppWallItem> userNews = vkAPI.

                //File.WriteAllText(@"D:\Documents\Visual Studio 2013\Projects\HelloWorld1\usr1Friends.txt", vkAPI.GetFriends(uid1));
            //FriendsIdsList friendIds = JsonConvert.DeserializeObject<FriendsIdsList>(vkAPI.GetFriends(uid1));
            //List<AppUser> friends = new List<AppUser>();
            //foreach (var u in friendIds.response)
            //{
                
            //    friends.Add(vkAPI.GetUserById(u.ToString()));
            //}
            //AppUser usr = vkAPI.GetUserById(uid1);
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