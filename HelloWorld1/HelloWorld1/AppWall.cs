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

        public AppWall(/*String ownerId*/)
        {
            Items = new List<AppWallItem>();
        }

    }

    public class AppWallItem
    {
        public int Id { get; set; }
        public int FromId { get; set; }
        public int OwnerId { get; set; }
        public int Date { get; set; }
        //private string post_type;
        public string Text { get; set; }
        public Comments comments;
        public Likes likes;
        public Reposts reposts;
        public List<CopyHistory> copy_history;
        public List<Attachment2> attachments;
        public int LikesPriority { get; set; }

        public AppWallItem()
        {

        }


        public Comments GetComments()
        {
            return this.comments;
        }

        public void SetComments(Comments comments)
        {
            this.comments = comments;
        }

        public Likes GetLikes()
        {
            return this.likes;
        }

        public void SetLikes(Likes likes)
        {
            this.likes = likes;
        }

        public Reposts GetReposts()
        {
            return this.reposts;
        }

        public void SetReposts(Reposts reposts)
        {
            this.reposts = reposts;
        }
        
        public List<CopyHistory> GetCopyHistory()
        {
            return this.copy_history;
        }

        public void SetCopyHistory(List<CopyHistory> copy_history)
        {
            this.copy_history = copy_history;
        }

        public List<Attachment2> GetAttachment2()
        {
            return this.attachments;
        }

        public void SetAttachment2(List<Attachment2> attachments)
        {
            this.attachments = attachments;
        }
    }

    //class AppWallItem
    //{
    //    public int Id { get; private set; }
    //    public int FromId { get; private set; }
    //    public int OwnerId { get; private set; }
    //    public int Date { get; private set; }
    //    //private string post_type;
    //    public string Text { get; private set; }
    //    public Comments Comments1 { get; private set; }
    //    public Likes Likes1 { get; private set; }
    //    public Reposts Reposts1 { get; private set; }

    //    public AppWallItem()
    //    {

    //    }
    //}


    

}
