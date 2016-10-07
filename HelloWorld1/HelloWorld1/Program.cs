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
            String uid1 = "42560016";
            String uid2 = "71985644";

            //String url = "https://api.vk.com/method/users.get?user_ids=42560016,71985644,mr.pavlichenkov";
            //String url = "https://api.vk.com/method/groups.getMembers?group_id=csu_iit";//&sort=time_asc&v=5.53"; // список пользователей группы
            //String url = "https://api.vk.com/method/friends.get?user_id=42560016"; // список друзей пользователя
            //String url = "https://api.vk.com/method/wall.get?domain=mr.pavlichenkov"; //owner_id=71985644"; количество и контент стены пользователя.

            VkAPI vkAPI = new VkAPI();
            //Console.WriteLine(vkAPI.GetMembers(gId));
            //File.WriteAllText(@"D:\Documents\Visual Studio 2013\Projects\HelloWorld1\members.txt", vkAPI.GetMembers(gId));

           // Console.WriteLine(vkAPI.GetUserInfo(uid2));
            //File.WriteAllText(@"D:\Documents\Visual Studio 2013\Projects\HelloWorld1\usr2.txt", vkAPI.GetUserInfo(uid2));
     
            Console.WriteLine("----------------------------------------------------------------");

            ////RootGroupMembers memb = JsonConvert.DeserializeObject<RootGroupMembers>(vkAPI.GetMembers(gId));
            ////RootUserBaseInfo uBaseInfo = null;
            ////int counter = 0;
            ////for (int i = 0; i < memb.response.count; ++i)
            ////{
            ////    counter++;
            ////    uBaseInfo = JsonConvert.DeserializeObject<RootUserBaseInfo>(vkAPI.GetUserInfo(memb.response.users[i].ToString()));  //
            ////    //Console.WriteLine("(№ " + counter + ") id = " + memb.response.users[i] + " - " + uBaseInfo.response[0].first_name + " " + uBaseInfo.response[0].last_name);


                
            ////    //File.AppendAllText/*WriteAllText*/(@"D:\Documents\Visual Studio 2013\Projects\HelloWorld1\members.txt",
            ////    //    "(№ " + counter + ") id = " + memb.response.users[i] + " - " + uBaseInfo.response[0].first_name + " " + uBaseInfo.response[0].last_name + "\r\n");
            ////}
            Console.WriteLine("================================================================");
            Group group = new Group(gId);
            group.SetUsers(vkAPI.GetMembers(group.GetGroupId().ToString())); // gets group members with their friends
            
            // запись в файл дерева -----------------------------------------------------------------------------
            int uCounter = 0;
            int fCounter = 0;
            foreach (var memb in group.GetUsers())
            {
                uCounter++;
                File.AppendAllText(@"D:\Documents\Visual Studio 2013\Projects\HelloWorld1\HelloWorld1\members+friends.txt",
                    "(№ " + uCounter + ") id = " + memb.GetUid() + 
                    " - " + memb.GetFirstname() + " " + memb.GetLastname() + 
                    ". Friends count:" + memb.GetFriends().Count +"\r\n");
                fCounter = 0;
                foreach (var friend in memb.GetFriends())
                {
                    fCounter++;
                    File.AppendAllText(@"D:\Documents\Visual Studio 2013\Projects\HelloWorld1\HelloWorld1\members+friends.txt",
                        "-----(№ " + fCounter + ") id = " + friend.GetUid() + " - " + 
                        friend.GetFirstname() + " " + 
                        friend.GetLastname() + "\r\n");
                }
            }
            
            //------------------------------------------------------------------------------------------------------------

            int cnt = 0;
            foreach (var u in group.GetUsers())
            {
                cnt++;
                Console.WriteLine("(№ " + cnt + ") id = " + u.GetUid() + " --- " + u.GetFirstname() + " " + u.GetLastname());
            }
            Console.WriteLine("Теперь введите id пользователя, чтобы получить список его друзей:");
            String neededId = Console.ReadLine();
            //User us = new User();

            User usr = group.GetUsers().Find(x => x.GetUid() == int.Parse(neededId));//vkAPI.GetUserInfo(neededId);
            List<User> friends = usr.GetFriends();//vkAPI.GetFriends(neededId);
            Console.WriteLine("Друзья пользователя " + usr.GetFirstname() + " " + usr.GetLastname() + "");
            foreach (var friend in friends)
            {
                Console.WriteLine("---" + friend.GetFirstname() + " " + friend.GetLastname());
            }

                //File.WriteAllText(@"D:\Documents\Visual Studio 2013\Projects\HelloWorld1\usr1Friends.txt", vkAPI.GetFriends(uid1));
            //FriendsIdsList friendIds = JsonConvert.DeserializeObject<FriendsIdsList>(vkAPI.GetFriends(uid1));
            //List<User> friends = new List<User>();
            //foreach (var u in friendIds.response)
            //{
                
            //    friends.Add(vkAPI.GetUserInfo(u.ToString()));
            //}
            //User usr = vkAPI.GetUserInfo(uid1);
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