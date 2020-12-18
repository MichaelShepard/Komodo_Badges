using BadgesRepository;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoBadges
{
    class ProgramUI
    {

        private BadgesRepo _badgesRepo = new BadgesRepo();

        public void Run()
        {
            SeedBadgeDictionary();
            Menu();
        }

        public void Menu()
        {

            bool keepRunning = true;

            while (keepRunning)
            {

                Console.WriteLine("Select an option: \n" +
                    "1. Create a new badge \n" +
                    "2. Update a badge's door access \n" +
                    "3. Delete a badge and its access \n" +
                    "4. Show a badge with its door access \n" +
                    "5. Show a list of all badges with door access \n" +
                    "6. Exit");

                string input = Console.ReadLine();

                switch (input)
                {

                    case "1":
                        CreateBadge();
                        break; 
                    case "2":
                        UpdateBadgeAccess();
                        break;
                    case "3":
                        DeleteBadgeAccess();
                        break;
                    case "4":
                        BadgeDoorAccess();
                        break;
                    case "5":
                        ListAllBadges();
                        break;

                    case "6":
                        Console.WriteLine("Good Bye!");
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid selection.");
                        break;

                }

                Console.WriteLine("Press any key to conitnue...");
                Console.ReadKey();
                Console.Clear();

            }
        } //End of Menu

        public void CreateBadge()
        {
            
            Badges newBadge = new Badges();
            List<string> newAccess = new List<string>();
            

            bool inBadgecheckLoop = true;

            while (inBadgecheckLoop)
            {
                Console.Clear();
                Console.WriteLine("What is the badge number:");
                var input = Console.ReadLine();


                if (Int32.TryParse(input, out int number))  // turns a string into int if it can
                {

                    newBadge.BadgeID = number; // takes the number and adds it to Badges
                    inBadgecheckLoop = false; // sets check the badge to false

                } else {

                    Console.WriteLine($"The Badge ID, {input}, is not a number. Please try again. Press Enter to continue...." );
                    Console.ReadKey();
                    inBadgecheckLoop = true;
                }


            }

            bool doorAccessDone = true;

            while(doorAccessDone)
            {
                Console.WriteLine("Which door needs access?");
                newAccess.Add(Console.ReadLine());
                newBadge.DoorAccess = newAccess; // adds door to List Door Acccess

               

                Console.WriteLine("Add another door? (y/n)");
                string inputAddAnotherDoor = Console.ReadLine();
                bool yesAddAnotherDoor = "y".Equals(inputAddAnotherDoor, StringComparison.OrdinalIgnoreCase);
                if (yesAddAnotherDoor == true)
                {

                    doorAccessDone = true;
                }
                else
                {

                    doorAccessDone = false;
                }
            }

           
             _badgesRepo.AddToAccessList(newBadge); // Sends BadgeID and DoorAccess to repo for insertion

        } // End of Create New Badge


        public void UpdateBadgeAccess()
        {
           
            bool inBadgecheckLoop = true;

            while (inBadgecheckLoop)
            {
                Console.Clear();
                Console.WriteLine("What is the badge number:");
                var input = Console.ReadLine();
                

                if (Int32.TryParse(input, out int number))
                {

                    string badgeAccess = _badgesRepo.GetAccessBadgeByID(Convert.ToInt32(input));
                    inBadgecheckLoop = false;

                    if(badgeAccess != null)
                    {
                        Console.Write($"Badge Number: {number} has access to room(s) {badgeAccess}.");

                        Console.WriteLine("What would you like to do? \n" +
                        "1. Remove a room \n" +
                        "2. Add a door");

                        string nexStepInput = Console.ReadLine();

                        switch (nexStepInput)
                        {

                            case "1":
                                RemoveARoom(Convert.ToInt32(input));
                                break;
                            case "2":
                                AddARoom(Convert.ToInt32(input));
                                break;
                            default:
                                Console.WriteLine("Please enter a valid selection.");
                                break;

                        }

                        Console.WriteLine("Press any key to conitnue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("I could not find that badge number");

                    }

                }
                else
                {

                    Console.WriteLine($"The Badge ID, {input}, is not a number. Please try again. Press Enter to continue....");
                    Console.ReadKey();
                    inBadgecheckLoop = true;
                }

            }  // check if badge is just a number

            

        }


        public void RemoveARoom(int badgeID)
        {
            Badges newUpdateBadge = new Badges();
            List<string> removeAccess = new List<string>();

            newUpdateBadge.BadgeID = badgeID;

            bool doorAccessDone = true;

            while (doorAccessDone)
            {
                Console.WriteLine("Which room do we need to remove?");
                removeAccess.Add(Console.ReadLine());

                newUpdateBadge.DoorAccess = removeAccess;

                Console.WriteLine("Remove another room? (y/n)");
                string inputRemoveAnotherDoor = Console.ReadLine();
                bool yesRemoveAnotherDoor = "y".Equals(inputRemoveAnotherDoor, StringComparison.OrdinalIgnoreCase);
                
                if (yesRemoveAnotherDoor == true)
                {

                    doorAccessDone = true;

                } else {

                    doorAccessDone = false;
                }

            }
                bool wasUpdated = _badgesRepo.RemoveRoomToBadge(newUpdateBadge);

            if (wasUpdated)
            {
                Console.WriteLine("Room was successfully removed.");

            } else {

                Console.WriteLine("Room access was not updated.");
            }

        }

        public void AddARoom(int badgeID)
        {
            Badges newUpdateBadge = AddRoomHelper(badgeID);

            bool wasUpdated = _badgesRepo.AddRoomToBadge(newUpdateBadge);

            if (wasUpdated)
            {
                Console.WriteLine("Room was successfully added.");
            }
            else
            {
                Console.WriteLine("Room access was not updated.");
            }

        }


    
        public void DeleteBadgeAccess()
        {
            Console.Clear();

            Console.WriteLine("which badge would you like to delete?");
            int badgeID = Convert.ToInt32(Console.ReadLine());

           bool wasDeleted = _badgesRepo.DeleteBadge(badgeID);

            if (wasDeleted)
            {
                Console.WriteLine("Badge was succesfully deleted.");
            }
            else
            {
                Console.WriteLine("Badge was not deleted. You will need to try again.");
            }


        }

        public void BadgeDoorAccess()
        {
            Console.Clear();
            Console.WriteLine("What is the badge number:");
            int badgeID = Convert.ToInt32(Console.ReadLine());


            string badgeAccess = _badgesRepo.GetAccessBadgeByID(badgeID);


            if (badgeAccess != null)
            {
                Console.Write($"Badge Number: {badgeID} has access to room(s) {badgeAccess}. \n");
            } else {
                Console.WriteLine("Badge does not exist. Try again.");
            }
        }

        public void ListAllBadges()
        {

            Console.Clear();

            Dictionary<int, List<string>> _listOfBadges = _badgesRepo.GetBadges(); // creates a new object from the data. 

            var table = new ConsoleTable("Badge #", "Door Access"); // uses ConsoleTable to print out dat table and this is the header

            foreach (KeyValuePair<int, List<string>> pair in _listOfBadges)
            {
                string combinedAccessList = string.Join(", ", pair.Value); // takes the values from the list and converts them to a string
                table.AddRow(pair.Key, combinedAccessList);
            }

            table.Write();  // prints table
            Console.WriteLine();
        }

        
        public Badges AddRoomHelper(int badgeID)
        {

            Badges newUpdateBadge = new Badges();
            List<string> addAccess = new List<string>();

            newUpdateBadge.BadgeID = badgeID;

            bool doorAccessDone = true;

            while (doorAccessDone)
            {
                Console.WriteLine("Which room do we need to Add?");
                addAccess.Add(Console.ReadLine());

                newUpdateBadge.DoorAccess = addAccess;

                Console.WriteLine("Add another room? (y/n)");
                string inputaddAnotherDoor = Console.ReadLine();
                bool yesAddAnotherDoor = "y".Equals(inputaddAnotherDoor, StringComparison.OrdinalIgnoreCase);
                if (yesAddAnotherDoor == true)
                {

                    doorAccessDone = true;
                }
                else
                {

                    doorAccessDone = false;
                }

            }
            
            return newUpdateBadge;

        }
        public void SeedBadgeDictionary()
        {
            List<string> list1 = new List<string>();  // Need to create a list
            list1.Add("Room 2");
            list1.Add("Room 4");
            
            var badge1 = new Badges(1, list1);
            _badgesRepo.AddToAccessList(badge1);

            List<string> list2 = new List<string>();
            list2.Add("Room 3");
            list2.Add("Room 4");

            var badge2 = new Badges(2, list2);
            _badgesRepo.AddToAccessList(badge2);


        }


    } // End of ProgramUI Class
}// End of Namespace
