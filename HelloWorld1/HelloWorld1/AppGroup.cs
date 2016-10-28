using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace vkSmartWall
{
    public class AppGroup
    {
        public List<AppUser> Users { get;  set; }
        public int Count { get; set; }
        public string GroupName { get; set; }

        public AppGroup(string groupName)
        {
            this.GroupName = groupName;
        }

        /*public AppUser GetMembers()
        {

            return null;
        } */

    }


   
}
