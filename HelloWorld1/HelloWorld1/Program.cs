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

            //String uid1 = "42560016"; String uid2 = "71985644"; String uid3 = "53516640"; String uid4 = vkClient.GetUserById("mr.pavlichenkov").Uid.ToString();
           
            var appGroup = new AppGroup(gId);
            appGroup.Users = vkClient.GetMembers(appGroup.GroupName); // gets AppGroup members with their friends
            
            //// запись в файл дерева -----------------------------------------------------------------------------
            int userCounter = 0;
            int friendsCounter;
            //File.Create(@"..\..\members+friends.txt");
            File.Delete(@"..\..\members+friends.txt");
            foreach (var memb in appGroup.Users)
            {
                userCounter++;
                File.AppendAllText(@"..\..\members+friends.txt",
                    "~(№ " + userCounter + ") id = " + memb.Uid +
                    " - " + memb.FirstName + " " + memb.LastName +
                    ". Friends count:" + memb.Friends.Count + "\r\n");
                friendsCounter = 0;
                foreach (var friend in memb.Friends)
                {
                    friendsCounter++;
                    File.AppendAllText(@"..\..\members+friends.txt",
                        "-----(№ " + friendsCounter + ") id = " + friend.Uid + " - " +
                        friend.FirstName + " " +
                        friend.LastName + "\r\n");
                }
            }
            
            ////------------------------------------------------------------------------------------------------------------
            // вывод списка пользователей группы в консоль
            int cnt = 0;
            foreach (var u in appGroup.Users)
            {
                cnt++;
                Console.WriteLine("(№ " + cnt + ") id = " + u.Uid + " --- " + u.FirstName + " " + u.LastName);
            }
            // конец вывода списка пользователей группы
            Console.WriteLine("Теперь введите id пользователя, чтобы получить список его друзей:");
            String neededId = Console.ReadLine();
            AppUser appUser = appGroup.Users.Find(x => x.Uid == int.Parse(neededId));
            /// !!! список друзей
            
            List<AppUser> friends = appUser.Friends;
            Console.WriteLine("Друзья пользователя " + appUser.FirstName + " " + appUser.LastName + "");
            foreach (var friend in friends)
            {
                Console.WriteLine("---" + friend.FirstName + " " + friend.LastName);
            }
            /// !!! - список новостей

            AppWall userNews = vkClient.GetNews(appUser);
            // сначала сортировка по приоритетам, затем по максимальному количеству лайков.
            userNews.Items = userNews.Items.OrderBy(o => o.LikesPriority).ThenByDescending(o => o.GetLikes().count).ToList();

            Console.WriteLine("Новости пользователя " + appUser.FirstName + " " + appUser.LastName);
            int newsCount = 0;
            int copyHistoryCount = 0;
            int attachmentsCount = 0;
            File.Delete(@"..\..\newsFor-"+ appUser.Uid+ "-User.txt");
            foreach (var newsItem in userNews.Items)
            {
                newsCount++;

                File.AppendAllText(@"..\..\newsFor-" + appUser.Uid + "-User.txt", "№ " + newsCount + " +-- newsText: " + newsItem.Text + "\r\n" +
                    "     --- id владельца: " + newsItem.OwnerId + "\r\n" +
                    "     --- date: " + new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(newsItem.Date).ToString() + "\r\n" +
                    "     --- likes: " + newsItem.GetLikes().count + "\r\n" +
                    "     --- reposts: " + newsItem.GetReposts().count + "\r\n" +
                    "     --- comments: " + newsItem.GetComments().count + "\r\n"
                    );

                if (newsItem.GetAttachment2() != null)
                {
                    File.AppendAllText(@"..\..\newsFor-" + appUser.Uid + "-User.txt", "     --- newsAttachments: " + "\r\n");

                    attachmentsCount = 0;
                    foreach (var attachment2 in newsItem.GetAttachment2())
                    {
                        attachmentsCount++;
                        File.AppendAllText(@"..\..\newsFor-" + appUser.Uid + "-User.txt", "     --- --- Вложение № " + attachmentsCount + "\r\n" +
                            "     --- ~~~ тип вложения " + attachment2.type + "\r\n"
                            );

                        if (attachment2.type.Equals("photo"))
                        {
                            File.AppendAllText(@"..\..\newsFor-" + appUser.Uid + "-User.txt", "     --- ~~~ фото: " + attachment2.photo.photo_130 + "\r\n");
                        }
                        else if (attachment2.type.Equals("audio"))
                        {
                            File.AppendAllText(@"..\..\newsFor-" + appUser.Uid + "-User.txt", "     --- ~~~ аудио: " + attachment2.audio.url + "\r\n");
                        }

                    }
                }

                if (newsItem.GetCopyHistory() != null)
                {
                    File.AppendAllText(@"..\..\newsFor-" + appUser.Uid + "-User.txt", "     --- newsCopyHistory: " + "\r\n");
                    copyHistoryCount = 0;
                    foreach (var copyHistoryItem in newsItem.GetCopyHistory())
                    {
                        copyHistoryCount++;
                        File.AppendAllText(@"..\..\newsFor-" + appUser.Uid + "-User.txt", "     --- --- Вложение № " + copyHistoryCount + "\r\n" +
                            "     --- ~~~ CHText: " + copyHistoryItem.text + "\r\n" +
                            "     --- ~~~ CHOwnerId: " + copyHistoryItem.owner_id + "\r\n" +
                            "     --- ~~~ CHDate: " + new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(copyHistoryItem.date).ToString() + "\r\n"
                            );
                    }

                }
            }
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }

}