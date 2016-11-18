using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace vkSmartWall
{
    public class AppUser
    {
        public int Uid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<AppUser> Friends { get; set; }
        public AppWall Wall { get; set; }

        public AppUser(int uid)
        {
            this.Uid = uid;
        }

    }

}
