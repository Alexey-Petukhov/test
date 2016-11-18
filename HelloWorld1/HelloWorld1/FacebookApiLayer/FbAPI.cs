using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using vkSmartWall.FacebookApiLayer.Model;

namespace vkSmartWall.FacebookApiLayer
{
    public class FbAPI
    {
        private String serviceUrl = "";

        public FbAPI()
        {
            serviceUrl = "http://graph.facebook.com/?id=";
        }

        public FbApiAnswer GetFbSharesFromLink(string link)
        {
            var url = serviceUrl + link;
            var jsonAnswer = DoReqGet(url);
            FbApiAnswer fbApiAnswer = new FbApiAnswer();
            if (jsonAnswer != null)
            {
                fbApiAnswer = JsonConvert.DeserializeObject<FbApiAnswer>(jsonAnswer);
            }

            return fbApiAnswer;

        }

        public String DoReqGet(String url) // do GET
        {
            string reply = null;
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                try
                {
                    reply = client.DownloadString(url);
                }
                catch (Exception e)
                {

                }
            }
            return reply;
        }
    }
}
