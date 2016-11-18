using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vkSmartWall.FacebookApiLayer.Model
{
    
    public class OgObject
    {
        public string id { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string updated_time { get; set; }
    }

    public class Share
    {
        public int comment_count { get; set; }
        public int share_count { get; set; }
    }

    public class FbApiAnswer
    {
        public OgObject og_object { get; set; }
        public Share share { get; set; }
        public string id { get; set; }
    }
}
