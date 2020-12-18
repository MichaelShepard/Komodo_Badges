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


        public bool GetBadgeByID(int number)
        {
            foreach (KeyValuePair<int, List<string>> pair in _badgesDictionary)
            {
                if (pair.Key == number)
                {

                    return true;

                }
            }

            return false;

        }
        public string GetAccessBadgeByID(int number)
        {
            
            foreach (KeyValuePair<int, List<string>> pair in _badgesDictionary)
            {
                if(pair.Key == number)
                {

                    string combinedAccessList = string.Join(", ", pair.Value);
                    return combinedAccessList;

                }
            }

            return null;
        }


        public bool AddRoomToBadge(Badges updateBadge)
        {
            if (_badgesDictionary.ContainsKey(updateBadge.BadgeID))
            // foreach(KeyValuePair<int, List<string>> kvp in _badgesDictionary.Where(kvp => kvp.Key.Equals(updateBadge.BadgeID)))
            {

                foreach (string s in updateBadge.DoorAccess)
                {
                    _badgesDictionary[updateBadge.BadgeID].Add(s);
                }

                return true;

            }
            else
            {

                return false;

            }

        }


        public bool RemoveRoomToBadge(Badges updateBadge)
        {
            if (_badgesDictionary.ContainsKey(updateBadge.BadgeID))
            // foreach(KeyValuePair<int, List<string>> kvp in _badgesDictionary.Where(kvp => kvp.Key.Equals(updateBadge.BadgeID)))
            {

                foreach (string s in updateBadge.DoorAccess)
                {
                    _badgesDictionary[updateBadge.BadgeID].Remove(s);
                }

                return true;

            }
            else
            {

                return false;

            }
        }

        public bool DeleteBadge(int badgeID)
        {
            if (_badgesDictionary.ContainsKey(badgeID))
            {
                _badgesDictionary.Remove(badgeID);
                return true;
            } else {
                return false;
            }
            
        }

    } // End of BadgesRepo Class
} // End of Namespace
