using HotelManagementSystem.Enumirations;
using HotelManagementSystem.Rooms;
using HotelManagementSystem.Users;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HotelManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Westminster Hotel Management System");

            string userRole = "customer";

            User activeUser = new User(userRole);
            WestminsterHotel westminsterHotel = new WestminsterHotel(activeUser);

        StartOver:

            // Display the User meny with respect to the Role
            activeUser.DisplayUserMenu(activeUser.GetRole());

            int selectedOption = Convert.ToInt32(Console.ReadLine());

            if (activeUser.GetRole() == "admin")
            {
                switch (selectedOption)
                {
                    // This case is for the ADMIN to Enter a new Room 
                    case 1:
                        // Implement a Try/Catch to handle the error that arrises when the user enters anything outside the Enumiration
                        try
                        {
                            Console.WriteLine("Specify Room Type (Standard/Delux)");
                            Enumirations.Type rt = (Enumirations.Type)Enum.Parse(typeof(Enumirations.Type), Console.ReadLine().ToUpper());

                            Console.WriteLine("Specify Room Number");
                            int rn = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Specify Floor Number");
                            int fn = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Specify Room Size (Single/Double/Triple)");
                            Enumirations.RoomSize rs = (Enumirations.RoomSize)Enum.Parse(typeof(Enumirations.RoomSize), Console.ReadLine().ToUpper());

                            Console.WriteLine("Specify Price Per Night");
                            double ppn = Convert.ToDouble(Console.ReadLine());

                            if (rt == Enumirations.Type.STANDARD)
                            {
                                Console.WriteLine("Specify No. of Windows");
                                int win = Convert.ToInt32(Console.ReadLine());

                                Console.Clear();
                                westminsterHotel.AddRoom(new StandardRoom(rn, fn, rs, ppn, win));
                            }
                            else
                            {
                                Console.WriteLine("Specify Size of Balcony in Square meters");
                                int sob = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Specify View from the room (Landmark/Mountain/Sea)");
                                Enumirations.View roomView = (Enumirations.View)Enum.Parse(typeof(Enumirations.View), Console.ReadLine().ToUpper());

                                Console.Clear();
                                westminsterHotel.AddRoom(new DeluxRoom(rn, fn, rs, ppn, sob, roomView));
                            }

                            Console.WriteLine("\nPress any key to go back to the menu");
                            Console.ReadKey();
                            Console.Clear();
                            goto StartOver;
                        }
                        catch (Exception ex)
                        {
                            Console.Clear();
                            Console.WriteLine("Something went wrong. Make sure you type the values specified in the options.");
                            Console.WriteLine("Please Try again\n");
                            goto StartOver;
                        }

                        break;
                    // This case is for the ADMIN to Delete an Existing Room
                    case 2:
                        Console.WriteLine("Specify Room Number you want to delete");
                        int roomNumber = Convert.ToInt16(Console.ReadLine());

                        westminsterHotel.DeleteRoom(roomNumber);

                        Console.WriteLine("\nPress any key to go back to the menu");
                        Console.ReadKey();
                        Console.Clear();
                        goto StartOver;

                        break;
                    // This case is for the ADMIN to List all the available Rooms
                    case 3:
                        Console.Clear();
                        Console.WriteLine("The list of all the rooms is given below: \n");

                        westminsterHotel.ListRooms();

                        Console.WriteLine("\nPress any key to go back to the menu");
                        Console.ReadKey();
                        Console.Clear();
                        goto StartOver;

                        break;
                    // This case is for the ADMIN to List all the available Rooms in a sorted order
                    case 4:
                        Console.Clear();
                        Console.WriteLine("The list of all the rooms is given below: \n");

                        westminsterHotel.ListRoomsOrderedByPrice();

                        Console.WriteLine("\nPress any key to go back to the menu");
                        Console.ReadKey();
                        Console.Clear();
                        goto StartOver;

                        break;
                    // This case is for the ADMIN to Print the details of all the rooms in a file
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Please enter the name of the file");

                        string filename = Console.ReadLine();
                        westminsterHotel.GenerateReport(filename);

                        Console.WriteLine("\nPress any key to go back to the menu");
                        Console.ReadKey();
                        Console.Clear();
                        goto StartOver;

                        break;
                        // This case is used by ADMIN to switch to Customer menu
                    case 6:
                        activeUser.SetRole("customer");
                        Console.Clear();
                        Console.WriteLine("You are logged in as : " + activeUser.GetRole().ToUpper());
                        Console.WriteLine("");
                        goto StartOver;

                        break;
                    default:
                        break;
                }
            }
            else
            {
                DateTime check_in = new DateTime();
                DateTime check_out = new DateTime();
                Enumirations.RoomSize roomSize = 0;
                double maxPrice = 0.0;
                int roomNumber = 0;
                bool correctInfo = false;

                switch (selectedOption)
                {
                    // This case is used by CUSTOMER to List available rooms
                    case 1:
                        while (!correctInfo)
                        {
                            Console.WriteLine("\nPlease specify check-in date (dd/mm/yy)");
                            check_in = Convert.ToDateTime(Console.ReadLine());

                            Console.WriteLine("Please specify check-out date (dd/mm/yy)");
                            check_out = Convert.ToDateTime(Console.ReadLine());

                            Console.WriteLine("Please specify size of Room (Single/Double/Triple)");
                            roomSize = (Enumirations.RoomSize)Enum.Parse(typeof(Enumirations.RoomSize), Console.ReadLine().ToUpper());

                            // The Following check of (DateTime.Compare) has been taken from the Official Microsoft Documentation
                            // https://learn.microsoft.com/en-us/dotnet/api/system.datetime.compare?view=net-7.0#system-datetime-compare(system-datetime-system-datetime)
                            int compareDate = DateTime.Compare(check_in, check_out);
                            int isFuture = DateTime.Compare(check_in, DateTime.Now);

                            if (compareDate >= 0)
                            {
                                Console.WriteLine("Check-out Date can not be less than Check-in Date. Try Again. \n");
                            }
                            else if(isFuture < 0)
                            {
                                Console.WriteLine("Check-in Date can not be less than Today's Date. Try Again. \n");
                            }
                            else
                            {
                                correctInfo = true;
                            }
                        }

                        Console.Clear();
                        Console.WriteLine("List of all the Available " + roomSize + " rooms from " + check_in.Date + " to " + check_out.Date + " are");

                        Booking wantedBooking = new Booking(check_in, check_out, roomSize);
                        westminsterHotel.ListAvailableRooms(wantedBooking, roomSize);

                        Console.WriteLine("Press any key to go back to the menu");
                        Console.ReadKey();
                        Console.Clear();
                        goto StartOver;

                        break;
                    // This case is used by CUSTOMER to List available rooms under a certain price
                    case 2:
                        while (!correctInfo)
                        {
                            Console.WriteLine("\nPlease specify check-in date (dd/mm/yy)");
                            check_in = Convert.ToDateTime(Console.ReadLine());

                            Console.WriteLine("Please specify check-out date (dd/mm/yy)");
                            check_out = Convert.ToDateTime(Console.ReadLine());

                            Console.WriteLine("Please specify size of Room (Single/Double/Triple)");
                            roomSize = (Enumirations.RoomSize)Enum.Parse(typeof(Enumirations.RoomSize), Console.ReadLine().ToUpper());

                            Console.WriteLine("Please enter the maximum price you are willing to pay");
                            maxPrice = Convert.ToDouble(Console.ReadLine());

                            // The Following check of (DateTime.Compare) has been taken from the Official Microsoft Documentation
                            // https://learn.microsoft.com/en-us/dotnet/api/system.datetime.compare?view=net-7.0#system-datetime-compare(system-datetime-system-datetime)
                            int compareDate = DateTime.Compare(check_in, check_out);
                            int isFuture = DateTime.Compare(check_in, DateTime.Now);

                            if (compareDate >= 0)
                            {
                                Console.WriteLine("Check-out Date can not be less than Check-in Date. Try Again. \n");
                            }
                            else if (isFuture < 0)
                            {
                                Console.WriteLine("Check-in Date can not be less than Today's Date. Try Again. \n");
                            }
                            else
                            {
                                correctInfo = true;
                            }
                        }

                        Console.Clear();
                        Console.WriteLine("List of all the Available " + roomSize + " rooms under the price of $" + maxPrice + " from " + check_in.Date + " to " + check_out.Date+ " are");

                        Booking wantedBooking1 = new Booking(check_in, check_out, roomSize);
                        westminsterHotel.ListAvailableRooms(wantedBooking1, roomSize, (int)maxPrice);

                        Console.WriteLine("Press any key to go back to the menu");
                        Console.ReadKey();
                        Console.Clear();
                        goto StartOver;

                        break;
                    // This case is used by CUSTOMER to make a booking
                    case 3:
                        while (!correctInfo)
                        {
                            Console.WriteLine("\nPlease specify check-in date (dd/mm/yy)");
                            check_in = Convert.ToDateTime(Console.ReadLine());

                            Console.WriteLine("Please specify check-out date (dd/mm/yy)");
                            check_out = Convert.ToDateTime(Console.ReadLine());

                            Console.WriteLine("Please specify the Room Number");
                            roomNumber = Convert.ToInt32(Console.ReadLine());

                            // The Following check of (DateTime.Compare) has been taken from the Official Microsoft Documentation
                            // https://learn.microsoft.com/en-us/dotnet/api/system.datetime.compare?view=net-7.0#system-datetime-compare(system-datetime-system-datetime)
                            int compareDate = DateTime.Compare(check_in, check_out);
                            int isFuture = DateTime.Compare(check_in, DateTime.Now);

                            if (compareDate >= 0)
                            {
                                Console.WriteLine("Check-out Date can not be less than Check-in Date. Try Again. \n");
                            }
                            else if (isFuture < 0)
                            {
                                Console.WriteLine("Check-in Date can not be less than Today's Date. Try Again. \n");
                            }
                            else
                            {
                                correctInfo = true;
                            }
                        }

                        Console.Clear();

                        Booking wantedBooking2 = new Booking(check_in, check_out, roomNumber);
                        westminsterHotel.BookRoom(roomNumber, wantedBooking2);

                        Console.WriteLine("Press any key to go back to the menu");
                        Console.ReadKey();
                        Console.Clear();
                        goto StartOver;

                        break;
                        // This case is used by CUSTOMER to switch to admin menu
                    case 4:
                        activeUser.SetRole("admin");
                        Console.Clear();
                        Console.WriteLine("You are logged in as : " + activeUser.GetRole().ToUpper());
                        Console.WriteLine("");
                        goto StartOver;

                        break;
                    default:
                        break;
                }
            }

        }
    }
}
