using System;
using System.Collections.Generic;
using BadgesRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BadgesTest
{
    [TestClass]
    public class BadgesTest
    {
        // Initialize tests
        private Badges _badges;
        private BadgesRepo _repo;

        [TestInitialize]
        public void Arrange()
        {

            List<string> list1 = new List<string>();  // Need to create a lis
            list1.Add("Room 4");

            var badge1 = new Badges(1, list1);
            _repo = new BadgesRepo();
            _repo.AddToAccessList(badge1);

        }

        [TestMethod]
        public void AddToAccessList_SholdGetNull()
        {
            //Arrange
            Badges badge = new Badges();
            badge.BadgeID = 2;
            badge.DoorAccess.Add("Room 9");

            // Act
            BadgesRepo repo = new BadgesRepo();
            repo.AddToAccessList(badge);
            bool updateResult = repo.GetBadgeByID(1);

            // Assert
            Assert.IsTrue(updateResult);

        }

        [TestMethod]
        public void AddRoomToBadge_ShouldGetTrue()
        {

            // Arrange
            Badges badge = new Badges();
            badge.BadgeID = 1;
            badge.DoorAccess.Add("Room 9");

            // Act
            BadgesRepo repo = new BadgesRepo(); 
            repo.AddRoomToBadge(badge);
            bool badgeFromList = repo.GetBadgeByID(1);

            //  Assert
            Assert.AreEqual(badge.DoorAccess, badgeFromList);



        }









    } // End of Badges Test
} // End of Namespace
