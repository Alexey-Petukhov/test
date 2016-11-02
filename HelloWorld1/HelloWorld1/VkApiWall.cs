using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vkSmartWall
{
    //class VkApiWall
    //{
    //}
    public class Photo
    {
        public int id { get; set; }
        public int album_id { get; set; }
        public int owner_id { get; set; }
        public int user_id { get; set; }
        public string photo_75 { get; set; }
        public string photo_130 { get; set; }
        public string photo_604 { get; set; }
        public string photo_807 { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string text { get; set; }
        public int date { get; set; }
        public string access_key { get; set; }
        public string photo_1280 { get; set; }
        public int? post_id { get; set; }
    }

    public class Audio
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public int duration { get; set; }
        public int date { get; set; }
        public string url { get; set; }
        public int lyrics_id { get; set; }
        public int genre_id { get; set; }
    }

    public class Attachment
    {
        public string type { get; set; }
        public Photo photo { get; set; }
        public Link link { get; set; }
        public Audio audio { get; set; }
    }
    public class Link
    {
        public string url { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Photo photo { get; set; }
    }

    public class PostSource
    {
        public string type { get; set; }
        public string platform { get; set; }
    }

    public class CopyHistory
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public int from_id { get; set; }
        public int date { get; set; }
        public string post_type { get; set; }
        public string text { get; set; }
        public List<Attachment> attachments { get; set; }
        public PostSource post_source { get; set; }
    }

    public class Comments
    {
        public int count { get; set; }
    }

    public class Likes
    {
        public int count { get; set; }
    }

    public class Reposts
    {
        public int count { get; set; }
    }

    public class Photo2
    {
        public int id { get; set; }
        public int album_id { get; set; }
        public int owner_id { get; set; }
        public string photo_75 { get; set; }
        public string photo_130 { get; set; }
        public string photo_604 { get; set; }
        public string photo_807 { get; set; }
        public string photo_1280 { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string text { get; set; }
        public int date { get; set; }
        public string access_key { get; set; }
        public string photo_2560 { get; set; }
        public int? post_id { get; set; }
    }

    public class Audio2
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public int duration { get; set; }
        public int date { get; set; }
        public string url { get; set; }
        public int genre_id { get; set; }
    }

    public class Attachment2
    {
        public string type { get; set; }
        public Photo2 photo { get; set; }
        public Link link { get; set; }
        public Audio2 audio { get; set; }
    }

    public class Place
    {
        public int id { get; set; }
        public string title { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int created { get; set; }
        public string icon { get; set; }
        public string country { get; set; }
        public string city { get; set; }
    }

    public class Geo
    {
        public string type { get; set; }
        public string coordinates { get; set; }
        public Place place { get; set; }
    }

    public class Item
    {
        public int id { get; set; }
        public int from_id { get; set; }
        public int owner_id { get; set; }
        public int date { get; set; }
        public string post_type { get; set; }
        public string text { get; set; }
        public List<CopyHistory> copy_history { get; set; }
        public Comments comments { get; set; }
        public Likes likes { get; set; }
        public Reposts reposts { get; set; }
        public List<Attachment2> attachments { get; set; }
        public Geo geo { get; set; }
    }

    public class WallItems
    {
        public int count { get; set; }
        public List<Item> items { get; set; }
    }

    public class VkApiWall
    {
        public WallItems response { get; set; }
    }
}
