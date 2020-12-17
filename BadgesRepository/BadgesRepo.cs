using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadgesRepository
{
    public class BadgesRepo
    {

        private Dictionary<int, List<string>> _badgesDictionary = new Dictionary<int, List<string>>();

        public void AddToAccessList(Badges newBadge)
        {
            _badgesDictionary.Add(newBadge.BadgeID, newBadge.DoorAccess);
        }


        public Dictionary<int, List<string>> GetBadges()
        {

            return _badgesDictionary;
        }


    } // End of BadgesRepo Class
} // End of Namespace
