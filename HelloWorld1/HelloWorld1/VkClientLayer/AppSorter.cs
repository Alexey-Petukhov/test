using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vkSmartWall
{
    public class AppSorter
    {
        public AppSorter()
        {
            
        }

        public AppWall SortByLikes(AppWall appWall)
        {
            appWall.Items = appWall.Items.OrderBy(o => o.LikesPriority).ThenByDescending(o => o.AppLikes.Count).ToList();
            return appWall;
        }

        public AppWall SortByLikesDesc(AppWall appWall)
        {
            appWall.Items = appWall.Items.OrderByDescending(o => o.LikesPriority).ThenByDescending(o => o.AppLikes.Count).ToList();
            return appWall;
        }

        public AppWall SortByComments(AppWall appWall)
        {
            appWall.Items = appWall.Items.OrderBy(o => o.CommentsPriority).ThenByDescending(o => o.AppComments.Count).ToList();
            return appWall;
        }
        public AppWall SortByCommentsDesc(AppWall appWall)
        {
            appWall.Items = appWall.Items.OrderByDescending(o => o.CommentsPriority).ThenByDescending(o => o.AppComments.Count).ToList();
            return appWall;
        }

        public AppWall SortByReposts(AppWall appWall)
        {
            appWall.Items = appWall.Items.OrderBy(o => o.RepostsPriority).ThenByDescending(o => o.AppReposts.Count).ToList();
            return appWall;
        }

        public AppWall SortByRepostsDesc(AppWall appWall)
        {
            appWall.Items = appWall.Items.OrderByDescending(o => o.RepostsPriority).ThenByDescending(o => o.AppReposts.Count).ToList();
            return appWall;
        }
    }
}
