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
        public string Id { get; set; }
        public int FromId { get; set; }
        public int OwnerId { get; set; }
        public int Date { get; set; }
        //private string PostType;
        public string Text { get; set; }
        public AppComments AppComments { get; set; }
        public AppLikes AppLikes { get; set; }
        public AppReposts AppReposts { get; set; }
        public List<AppCopyHistory> AppCopyHistory { get; set; }
        public List<AppAttachment> AppAttachments { get; set; }
        //public Comments comments;
        //public Likes likes;
        //public Reposts reposts;
        //public List<CopyHistory> copy_history;
        //public List<Attachment2> attachments;

        public int LikesPriority { get; set; }

        public AppWallItem()
        {

        }


        //public Comments GetComments()
        //{
        //    return this.comments;
        //}

        //public void SetComments(Comments comments)
        //{
        //    this.comments = comments;
        //}

        //public Likes GetLikes()
        //{
        //    return this.likes;
        //}

        //public void SetLikes(Likes likes)
        //{
        //    this.likes = likes;
        //}

        //public Reposts GetReposts()
        //{
        //    return this.reposts;
        //}

        //public void SetReposts(Reposts reposts)
        //{
        //    this.reposts = reposts;
        //}
        
        //public List<CopyHistory> GetCopyHistory()
        //{
        //    return this.copy_history;
        //}

        //public void SetCopyHistory(List<CopyHistory> copy_history)
        //{
        //    this.copy_history = copy_history;
        //}

        //public List<Attachment2> GetAttachment2()
        //{
        //    return this.attachments;
        //}

        //public void SetAttachment2(List<Attachment2> attachments)
        //{
        //    this.attachments = attachments;
        //}
    }
    public class AppPhoto
    {
        //public int Id { get; set; }
        //public int AlbumId { get; set; }
        //public int OwnerId { get; set; }
        //public int user_id { get; set; }
        //public string photo_75 { get; set; }
        public string Photo130 { get; set; }
        //public string photo_604 { get; set; }
        //public string photo_807 { get; set; }
        //public int width { get; set; }
        //public int height { get; set; }
        //public string Text { get; set; }
        //public int Date { get; set; }
        //public string access_key { get; set; }
        //public string photo_1280 { get; set; }
        //public int? post_id { get; set; }
    }

    public class AppAudio
    {
        //public int Id { get; set; }
        //public int OwnerId { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        //public int duration { get; set; }
        //public int Date { get; set; }
        //public int lyrics_id { get; set; }
        //public int genre_id { get; set; }
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

    //public class AppPostSource
    //{
    //    public string Type { get; set; }
    //    public string platform { get; set; }
    //}

    public class AppCopyHistory
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int FromId { get; set; }
        public int Date { get; set; }
        public string PostType { get; set; }
        public string Text { get; set; }
        public List<AppAttachment> Attachments { get; set; }
        //public AppPostSource post_source { get; set; }
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

    public class AppPhoto2
    {
        
        public string Photo130 { get; set; }

    }

    //public class AppAudio2
    //{
    //    //public int Id { get; set; }
    //    //public int OwnerId { get; set; }
    //    public string Artist { get; set; }
    //    public string Title { get; set; }
    //    public string Url { get; set; }
    //    //public int duration { get; set; }
    //    //public int Date { get; set; }
    //    //public int genre_id { get; set; }
    //}

    //public class AppAttachment2
    //{
    //    public string Type { get; set; }
    //    public AppPhoto2 Photo { get; set; }
    //    public AppAudio2 Audio { get; set; }
    //}

    //public class AppPlace
    //{
    //    public int Id { get; set; }
    //    public string Title { get; set; }
    //    public double latitude { get; set; }
    //    public double longitude { get; set; }
    //    public int created { get; set; }
    //    public string icon { get; set; }
    //    public string country { get; set; }
    //    public string city { get; set; }
    //}

    //public class AppGeo
    //{
    //    public string Type { get; set; }
    //    public string coordinates { get; set; }
    //    public AppPlace place { get; set; }
    //}
    //class AppWallItem
    //{
    //    public int Id { get; private set; }
    //    public int FromId { get; private set; }
    //    public int OwnerId { get; private set; }
    //    public int Date { get; private set; }
    //    //private string PostType;
    //    public string Text { get; private set; }
    //    public Comments Comments1 { get; private set; }
    //    public Likes Likes1 { get; private set; }
    //    public Reposts Reposts1 { get; private set; }

    //    public AppWallItem()
    //    {

    //    }
    //}


    

}
