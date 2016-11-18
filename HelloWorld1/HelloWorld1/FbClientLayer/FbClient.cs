using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vkSmartWall.FacebookApiLayer;
using vkSmartWall.FacebookApiLayer.Model;
using vkSmartWall.FbClientLayer.Model;

namespace vkSmartWall.FbClientLayer
{
    class FbClient
    {
        FbAPI fbApi;
        FbClientConverter fbClientConverter;

        public FbClient(FbAPI fbApi)
        {
            this.fbApi = fbApi;
            fbClientConverter = new FbClientConverter();
        }

        public AppFbShareCount GetFbSharesFromLink(string link)
        {
            var fbApiAnswer = fbApi.GetFbSharesFromLink(link);
            var appFbShareCount = fbClientConverter.Convert(fbApiAnswer);
            return appFbShareCount;
        }

    }
}
