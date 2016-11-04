using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vkSmartWall
{
    public class AppWall
    {
        public List<AppWallItem> Items { get; set; }

        public AppWall()
        {
            Items = new List<AppWallItem>();
        }

    }

    public class AppWallItem
    {
        public string Id { get; set; }
        public int FromId { get; set; }
        public int OwnerId { get; set; }
        public int Date { get; set; }
        public string Text { get; set; }
        public AppComments AppComments { get; set; }
        public AppLikes AppLikes { get; set; }
        public AppReposts AppReposts { get; set; }
        public List<AppCopyHistory> AppCopyHistory { get; set; }
        public List<AppAttachment> AppAttachments { get; set; }


        public double LikesPriority { get; set; } 
        public double CommentsPriority { get; set; }
        public double RepostsPriority { get; set; }


        public AppWallItem()
        {

        }


       
    }
    public class AppPhoto
    {
        public string Photo130 { get; set; }
        
    }

    public class AppAudio
    {
        
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
       
    }
    public class AppLink
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public AppPhoto Photo { get; set; }
    }

    public class AppAttachment
    {
        public string Type { get; set; }
        public AppPhoto Photo { get; set; }
        public AppAudio Audio { get; set; }

        public AppLink Link { get; set; }
    }

   
    public class AppCopyHistory
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int FromId { get; set; }
        public int Date { get; set; }
        public string PostType { get; set; }
        public string Text { get; set; }
        public List<AppAttachment> Attachments { get; set; }
    }

    public class AppComments
    {
        public int Count { get; set; }
    }

    public class AppLikes
    {
        public int Count { get; set; }
    }

    public class AppReposts
    {
        public int Count { get; set; }
    }

   
}
