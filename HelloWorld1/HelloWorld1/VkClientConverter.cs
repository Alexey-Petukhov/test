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
            appWallItem.AppCopyHistory = new List<AppCopyHistory>();

            
            if (wallItem.copy_history != null)
            {
                AppCopyHistory emptyCopyHistory;
                foreach (var copyHistory in wallItem.copy_history)
                {
                    emptyCopyHistory = new AppCopyHistory();
                    appWallItem.AppCopyHistory.Add(emptyCopyHistory);

                    appWallItem.AppCopyHistory.Last().Id = copyHistory.id;
                    appWallItem.AppCopyHistory.Last().OwnerId = copyHistory.owner_id;
                    appWallItem.AppCopyHistory.Last().FromId = copyHistory.from_id;
                    appWallItem.AppCopyHistory.Last().Date = copyHistory.date;
                    appWallItem.AppCopyHistory.Last().PostType = copyHistory.post_type;
                    appWallItem.AppCopyHistory.Last().Text = copyHistory.text;
                    AppAttachment emptyCopyHistoryAttachment;
                    if (copyHistory.attachments != null)
                    {
                        appWallItem.AppCopyHistory.Last().Attachments = new List<AppAttachment>();

                        foreach (var attachment in copyHistory.attachments)
                        {
                            
                            emptyCopyHistoryAttachment = new AppAttachment();

                            appWallItem.AppCopyHistory.Last().Attachments.Add(emptyCopyHistoryAttachment);
                            appWallItem.AppCopyHistory.Last().Attachments.Last().Type = attachment.type;
                            switch (appWallItem.AppCopyHistory.Last().Attachments.Last().Type)
                            {
                                case "audio":
                                    appWallItem.AppCopyHistory.Last().Attachments.Last().Audio = new AppAudio(); 
                                    appWallItem.AppCopyHistory.Last().Attachments.Last().Audio.Artist =
                                        attachment.audio.artist;
                                    appWallItem.AppCopyHistory.Last().Attachments.Last().Audio.Title =
                                        attachment.audio.title;
                                    appWallItem.AppCopyHistory.Last().Attachments.Last().Audio.Url =
                                        attachment.audio.url;
                                    break;
                                case "photo":
                                    appWallItem.AppCopyHistory.Last().Attachments.Last().Photo = new AppPhoto();
                                    appWallItem.AppCopyHistory.Last().Attachments.Last().Photo.Photo130 =
                                        attachment.photo.photo_130;
                                    break;
                                case "link":
                                    appWallItem.AppCopyHistory.Last().Attachments.Last().Link = new AppLink();
                                    appWallItem.AppCopyHistory.Last().Attachments.Last().Link.Url = attachment.link.url;
                                    appWallItem.AppCopyHistory.Last().Attachments.Last().Link.Title =
                                        attachment.link.title;
                                    appWallItem.AppCopyHistory.Last().Attachments.Last().Link.Description =
                                        attachment.link.description;
                                    if (attachment.link.photo != null)
                                    {
                                        appWallItem.AppCopyHistory.Last().Attachments.Last().Link.Photo = new AppPhoto();
                                        appWallItem.AppCopyHistory.Last().Attachments.Last().Link.Photo.Photo130 =
                                            attachment.link.photo.photo_130;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                }
            } // for copy history

            AppAttachment emptyAppAttachment;
            if (wallItem.attachments != null)
            {
                appWallItem.AppAttachments = new List<AppAttachment>();
                foreach (var attachment in wallItem.attachments)
                {
                    emptyAppAttachment = new AppAttachment();
                    appWallItem.AppAttachments.Add(emptyAppAttachment);
                    appWallItem.AppAttachments.Last().Type = attachment.type;
                    switch (appWallItem.AppAttachments.Last().Type)
                    {
                        case "audio":
                            appWallItem.AppAttachments.Last().Audio = new AppAudio(); 
                            appWallItem.AppAttachments.Last().Audio.Artist = attachment.audio.artist;
                            appWallItem.AppAttachments.Last().Audio.Title = attachment.audio.title;
                            appWallItem.AppAttachments.Last().Audio.Url = attachment.audio.url;
                            break;
                        case "photo":
                            appWallItem.AppAttachments.Last().Photo = new AppPhoto();
                            appWallItem.AppAttachments.Last().Photo.Photo130 = attachment.photo.photo_130;
                            break;
                        case "link":
                            appWallItem.AppAttachments.Last().Link = new AppLink();
                            appWallItem.AppAttachments.Last().Link.Url = attachment.link.url;
                            appWallItem.AppAttachments.Last().Link.Title =
                                attachment.link.title;
                            appWallItem.AppAttachments.Last().Link.Description =
                                attachment.link.description;
                            if (attachment.link.photo != null)
                            {
                                appWallItem.AppAttachments.Last().Link.Photo = new AppPhoto();
                                appWallItem.AppAttachments.Last().Link.Photo.Photo130 =
                                    attachment.link.photo.photo_130;
                            }
                            break;
                        default:
                            break;
                    }
                }
            } // for attachments

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
