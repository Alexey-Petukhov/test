// A Hello World! program in C#.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using vkSmartWall.FacebookApiLayer;
using vkSmartWall.FbClientLayer;

namespace vkSmartWall
{
    class vkSmartWall
    {
        static void Main()
        {
            String gId = "csu_iit"; 
            //String gId = "38725714";

            var vkAPI = new VkAPI();
            var vkClient = new VkClient(vkAPI);
            var appSorter = new AppSorter();

            var fbApi = new FbAPI();
            var fbClient = new FbClient(fbApi);

            
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
                    "~(№ " + userCounter + ") Id = " + memb.Uid +
                    " - " + memb.FirstName + " " + memb.LastName +
                    ". Friends Count:" + memb.Friends.Count + "\r\n");
                friendsCounter = 0;
                foreach (var friend in memb.Friends)
                {
                    friendsCounter++;
                    File.AppendAllText(@"..\..\members+friends.txt",
                        "-----(№ " + friendsCounter + ") Id = " + friend.Uid + " - " +
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
                Console.WriteLine("(№ " + cnt + ") Id = " + u.Uid + " --- " + u.FirstName + " " + u.LastName);
            }
            // конец вывода списка пользователей группы
            Console.WriteLine("Теперь введите Id пользователя, чтобы получить список его друзей:");
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

            //-------------------------- Выставление ко всем ссылкам количество facebook шеров 
            foreach (var userNewsItem in userNews.Items)
            {
                if (userNewsItem.AppAttachments != null)
                {
                    foreach (var appAttachment in userNewsItem.AppAttachments)
                    {
                        if (appAttachment.Type.Equals("link"))
                        {
                            appAttachment.Link.FbShareCount =
                                fbClient.GetFbSharesFromLink(appAttachment.Link.Url).ShareCount;
                        }
                    }
                }
                if (userNewsItem.AppCopyHistory != null)
                {
                    foreach (var appCopyHistory in userNewsItem.AppCopyHistory)
                    {
                        if (appCopyHistory.Attachments != null)
                        {
                            foreach (var appAttachment in appCopyHistory.Attachments)
                            {
                                if (appAttachment.Type.Equals("link"))
                                {
                                    appAttachment.Link.FbShareCount =
                                        fbClient.GetFbSharesFromLink(appAttachment.Link.Url).ShareCount;
                                }
                            }
                        }
                    }
                }
            }
            //---------------------------------------------------------------------------------------------
            
            userNews = appSorter.SortByLikesDesc(userNews);

            Console.WriteLine("Новости пользователя " + appUser.FirstName + " " + appUser.LastName);
            int newsCount = 0;
            int copyHistoryCount = 0;
            int attachmentsCount = 0;
            File.Delete(@"..\..\newsFor-"+ appUser.Uid+ "-User.txt");
            foreach (var newsItem in userNews.Items)
            {
                newsCount++;

                File.AppendAllText(@"..\..\newsFor-" + appUser.Uid + "-User.txt", "№ " + newsCount + " +-- newsText: " + newsItem.Text + "\r\n" +
                    "     --- Id владельца: " + newsItem.OwnerId + "\r\n" +
                    "     --- Date: " + new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(newsItem.Date).ToString() + "\r\n" +
                    "     --- likes: " + newsItem.AppLikes.Count + "\r\n" +
                    "     --- reposts: " + newsItem.AppReposts.Count + "\r\n" +
                    "     --- comments: " + newsItem.AppComments.Count + "\r\n"
                    );

                if (newsItem.AppAttachments != null)
                {
                    File.AppendAllText(@"..\..\newsFor-" + appUser.Uid + "-User.txt", "     --- newsAttachments: " + "\r\n");

                    attachmentsCount = 0;
                    foreach (var appAttachment in newsItem.AppAttachments)
                    {
                        attachmentsCount++;
                        File.AppendAllText(@"..\..\newsFor-" + appUser.Uid + "-User.txt", "     --- --- Вложение № " + attachmentsCount + "\r\n" +
                            "     --- ~~~ тип вложения " + appAttachment.Type + "\r\n"
                            );

                        if (appAttachment.Type.Equals("photo"))
                        {
                            File.AppendAllText(@"..\..\newsFor-" + appUser.Uid + "-User.txt", "     --- ~~~ фото: " + appAttachment.Photo.Photo130 + "\r\n");
                        }
                        else if (appAttachment.Type.Equals("audio"))
                        {
                            File.AppendAllText(@"..\..\newsFor-" + appUser.Uid + "-User.txt", "     --- ~~~ аудио: " + appAttachment.Audio.Url + "\r\n");
                        }

                    }
                }

                if (newsItem.AppCopyHistory != null)
                {
                    File.AppendAllText(@"..\..\newsFor-" + appUser.Uid + "-User.txt", "     --- newsCopyHistory: " + "\r\n");
                    copyHistoryCount = 0;
                    foreach (var copyHistoryItem in newsItem.AppCopyHistory)
                    {
                        copyHistoryCount++;
                        File.AppendAllText(@"..\..\newsFor-" + appUser.Uid + "-User.txt", "     --- --- Вложение № " + copyHistoryCount + "\r\n" +
                            "     --- ~~~ CHText: " + copyHistoryItem.Text + "\r\n" +
                            "     --- ~~~ CHOwnerId: " + copyHistoryItem.OwnerId + "\r\n" +
                            "     --- ~~~ CHDate: " + new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(copyHistoryItem.Date).ToString() + "\r\n"
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