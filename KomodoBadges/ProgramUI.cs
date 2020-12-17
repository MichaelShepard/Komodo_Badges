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
                    "3. Delete all access from a badge \n" +
                    "4. Show a badge with its door access \n" +
                    "5. Show a list of all badges with door access \n" +
                    "6. Exit");

                string input = Console.ReadLine();

                switch (input)
                {

                    case "1":
                        CreateBadge();
                        break; 
                    //case "2":
                    //    UpdateBadgeAccess();
                    //    break;
                    //case "3":
                    //    DeleteBadgeAccess();
                    //case "4":
                    //    BadgeDoorAccess();
                    //    break;
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


                if (Int32.TryParse(input, out int number))
                {

                    newBadge.BadgeID = number;
                    inBadgecheckLoop = false;

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
                newBadge.DoorAccess = newAccess;

               

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

            //var newBadgeToAdd = new Badges()
            _badgesRepo.AddToAccessList(newBadge);

        } // End of Create New Badge



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
