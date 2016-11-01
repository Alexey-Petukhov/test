using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vkSmartWall
{
    public class VkClientConverter
    {
        VkAPI vkApi;
        public VkClientConverter() 
        {
            vkApi = new VkAPI();
        }

        public AppUser Convert(VkApiUser vkApiUser)
        {
            var appUser = new AppUser(vkApiUser.id);
            appUser.FirstName = vkApiUser.first_name;
            appUser.LastName = vkApiUser.last_name;
            return appUser;
        }
        public AppUser Convert(Member member) // for group member
        {
            var appUser = new AppUser(member.id);
            appUser.FirstName = member.first_name;
            appUser.LastName = member.last_name;
            //appUser.Friends = vkClient.GetFriends(appUser.Uid.ToString());
            //appUser.Wall = vkClient.GetWall(appUser.Uid.ToString() );
            return appUser;
        }

        public AppUser Convert(Friend friend) // for group friend
        {
            var appUser = new AppUser(friend.id);
            appUser.FirstName = friend.first_name;
            appUser.LastName = friend.last_name;
            //appUser.Friends = vkClient.GetFriends(appUser.Uid.ToString());
            //AppUser.Wall = vkClient.GetWall(appUser.Uid.ToString() );
            return appUser;
        }

        public AppWallItem Convert(Item wallItem)
        {
            var appWallItem = new AppWallItem();
            appWallItem.Id = wallItem.owner_id + "_" + wallItem.id;
            appWallItem.FromId = wallItem.from_id;
            appWallItem.OwnerId = wallItem.owner_id;
            appWallItem.Date = wallItem.date;
            appWallItem.Text = wallItem.text;
            appWallItem.AppLikes = new AppLikes();
                appWallItem.AppLikes.Count = wallItem.likes.count;
            appWallItem.AppReposts = new AppReposts();
                appWallItem.AppReposts.Count = wallItem.reposts.count;
            appWallItem.AppComments = new AppComments();
                appWallItem.AppComments.Count = wallItem.comments.count;
            //appWallItem.AppCopyHistory = wallItem.copy_history;
            //appWallItem.AppAttachments = wallItem.attachments;

            return appWallItem;
        }

        public List<AppUser> Convert(VkApiFriends vkApiFriends)
        {
            List<AppUser> friends = new List<AppUser>();
            foreach (var friend in vkApiFriends.response.items)
            {
                //friends.Add(CreateAppUser(friend));
                friends.Add(Convert(friend));
            }
            return friends;
        }

        public AppWall Convert(VkApiWall vkApiWall)
        {
            AppWall appWall = new AppWall();
            foreach (var item in vkApiWall.response.items)
            {
                appWall.Items.Add(Convert(item));
            }
            return appWall;
        }

        public void ConvertTo(VkApiWall vkApiWall)
        {
            
        }
    }

}
