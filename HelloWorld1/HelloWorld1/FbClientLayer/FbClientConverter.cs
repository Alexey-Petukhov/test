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
    public class FbClientConverter
    {
        FbAPI fbApi;

        public FbClientConverter()
        {
            fbApi = new FbAPI();
        }

        public AppFbShareCount Convert(FbApiAnswer fbApiAnswer)
        {
            var appFbShareCount = new AppFbShareCount();
            appFbShareCount.ShareCount = fbApiAnswer.share != null ? fbApiAnswer.share.share_count : 0;
            return appFbShareCount;
        }
    }
}
