using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Users
{
    internal class User
    {
        private string role; // Role can be a User or an Admin 

        public User(string role)
        {
            this.role = role;
        }

        public void SetRole(string role)
        {
            this.role = role;
        }

        public string GetRole()
        {
            return role;
        }

        // This function is used to display the User Menu on Console
        // Takes a string "role" as a parameter to display a user specific menu on the screen
        public virtual void DisplayUserMenu(string role)
        {
            Console.WriteLine("*********************"+role.ToUpper()+" MENU*********************");
            Console.WriteLine("\nKindly select one of the following option");

            if(role == "admin")
            {
                Console.WriteLine("1 - Add a new Room");
                Console.WriteLine("2 - Delete an Existing Room");
                Console.WriteLine("3 - List All Rooms");
                Console.WriteLine("4 - List all room ordered by Price");
                Console.WriteLine("5 - Print Reservation Details");
                Console.WriteLine("6 - Access the Customer Menu");
                Console.WriteLine("7 - Exit");
                Console.WriteLine("\n*****************************************************");

            }
            else
            {
                Console.WriteLine("1 - List Available Rooms");
                Console.WriteLine("2 - List Available rooms under a certain price");
                Console.WriteLine("3 - Book a Room");
                Console.WriteLine("4 - Access the Admin Menu");
                Console.WriteLine("5 - Exit");
                Console.WriteLine("\n*******************************************************");
            }
        }
        
    }
}
