using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vkSmartWall
{

    public class VkApiUser
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int? hidden { get; set; }
        public string deactivated { get; set; }
    }

    public class VkApiUsers
    {
        public List<VkApiUser> response { get; set; }
    }
}
