using System;
using vkSmartWall;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace vkSmartWall.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var vkApi = new VkAPI();
            var uid1 = "71985644";
            //var uid2 = vkApi.GetUserById("mr.pavlichenkov").GetUid().ToString();

            User testUser = vkApi.GetUserById(uid1);
            testUser.SetFriends(vkApi.GetFriends(testUser.GetUid().ToString()));

            Assert.AreEqual(117, testUser.GetFriends().Count);
        }
    }
}
