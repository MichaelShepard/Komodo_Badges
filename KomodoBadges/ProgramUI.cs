using BadgesRepository;
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
                    "2. Show a list of all badges with door access \n" +
                    "3. Show a badge with its door access \n" +
                    "4. Update a badge's door access \n" +
                    "5. Delete all access from a badge \n" +
                    "6. Exit");

                string input = Console.ReadLine();

                switch (input)
                {

                    case "1":
                        CreateBadge();
                        break;
                    //case "2":
                    //    ListAllBadges();
                    //    break;
                    //case "3":
                    //    BadgeDoorAccess();
                    //    break;
                    //case "4":
                    //    UpdateBadgeAccess();
                    //    break;
                    //case "5":
                    //    DeleteBadgeAccess();
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

            bool inBadgecheckLoop = true;

            while (inBadgecheckLoop)
            {
                Console.Clear();
                Console.WriteLine("What is the badge number:");
                var input = Console.ReadLine();


                if (Int32.TryParse(input, out int number))
                {

                    number = newBadge.BadgeID;
                    inBadgecheckLoop = true;
                } else {

                    Console.WriteLine($"The Badge ID, {input}, is not a number. Please try again");
                    inBadgecheckLoop = false;
                }


            }

            bool doorAccessDone = true;

            while(doorAccessDone)
            {
                Console.WriteLine("Which door needs acces?");
                newBadge.DoorAccess = Console.ReadLine();

                Console.WriteLine("Add another door? (y/n)");
                string inputAddAnotherDoor = Console.ReadLine();
                bool yesAddAnotherDoor = "y".Equals(inputAddAnotherDoor, StringComparison.OrdinalIgnoreCase);
                if(yesAddAnotherDoor == true)
                {

                    doorAccessDone = true;
                } else {

                    doorAccessDone = false;
                }


            }

            


        }



        public void SeedBadgeDictionary()
        {
            List<string> list1 = new List<string>();  // Need to create a list
            list1.Add("Room 2");
            list1.Add("Room 4");
            
            var badge1 = new Badges(1, list1);
            _badgesRepo.AddToAccessList(badge1);

            List<string> list2 = new List<string>();
            list1.Add("Room 3");
            list1.Add("Room 4");

            var badge2 = new Badges(2, list2);
            _badgesRepo.AddToAccessList(badge2);


        }


    } // End of ProgramUI Class
}// End of Namespace
