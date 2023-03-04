using HotelManagementSystem.Enumirations;
using HotelManagementSystem.Interface;
using HotelManagementSystem.Rooms;
using HotelManagementSystem.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HotelManagementSystem
{
    internal class WestminsterHotel : IHotelManager, IHotelCustomer, IOverlapable
    {
        private User user;
        private Room toRemove;

        private List<Room> roomList = new List<Room>(); // A List of Rooms
        private Dictionary<Booking, Room> bookedRooms = new Dictionary<Booking, Room>(); // A Dictionary of Active Bookings

        public WestminsterHotel(User user)
        {
            this.InitialRoomList(); // Create a list of rooms initially when the object is created
            this.user = user;
            Console.WriteLine();
        }

        // This function is used to Add a new Room to the List of The Rooms
        // Takes Room object as a parameter
        public bool AddRoom(Room room)
        {
            // The Following check of (List.Exist) has been taken from the Official Microsoft Documentation
            // Link --> https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.find?view=net-7.0
            bool isPresent = roomList.Exists(x => x.GetRoomNumber() == room.GetRoomNumber());

            try
            {
                if (isPresent)
                {
                    Console.WriteLine("This room already exist");
                    return false;
                }
                else
                {
                    Console.WriteLine("Room created Successfully");
                    Console.WriteLine("The details of the newly added room are as follows:");
                    room.DisplayRoomSpecs();
                    return true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to add a new Room. Please try again.");
                return false;
            }
        }

        // This function is used to Delete an existing Room from the List of The Rooms
        // Takes RoomNumber as a Parameter
        public bool DeleteRoom(int roomNumber)
        {            
            foreach (Room r in roomList)
            {
                toRemove = null;
                int rnum = r.GetRoomNumber();
                if(rnum == roomNumber)
                {
                    toRemove = r;
                    break;
                }
            }

            if(toRemove == null)
            {
                Console.WriteLine("The entered room number does not exist. Please try again");
                return false;
            }

            roomList.Remove(toRemove);
            Console.WriteLine("The Room has been deleted successfully");
            Console.WriteLine("The details of the newly deleted room are as follows:");
            toRemove.DisplayRoomSpecs();

            return true;
        }

        // This function is used to display a list of all the available rooms and their Booking Information
        // Iterates in the Dictionary to Display the Booking Information about certain Booking
        public void ListRooms()
        {
            bool isBooked = false;

            foreach(Room r in roomList)
            {
                r.DisplayRoomSpecs();

                Console.WriteLine("Booking Status of this Room:");

                foreach (KeyValuePair<Booking, Room> reservation in bookedRooms)
                {
                    if(reservation.Value.Equals(r))
                    {
                        reservation.Key.DisplayDates();
                        isBooked = true;
                    }
                }

                if(isBooked == false)
                    Console.WriteLine("This room has not yet Booked");
            }
        }

        // This function is used to display a list of all the available rooms and their Booking Information
        // The results are displayed in ascending order with respect to Price
        public void ListRoomsOrderedByPrice()
        {
            // Sort the list. IComparable Interface also plays a part here.
            roomList.Sort();
            bool isBooked = false;

            foreach (Room r in roomList)
            {
                r.DisplayRoomSpecs();

                Console.WriteLine("Booking Status of this Room:");

                foreach (KeyValuePair<Booking, Room> reservation in bookedRooms)
                {
                    if (reservation.Value.Equals(r))
                    {
                        reservation.Key.DisplayDates();
                        isBooked = true;
                    }
                }

                if (isBooked == false)
                    Console.WriteLine("This Room has not yet Booked");
            }
        }

        // This function generates a report of all the available Rooms and their respective Booking Details
        // It Creates a new file and saves the data in it.
        // Takes FileName as a parameter
        public void GenerateReport(string fileName)
        {
            bool isBooked = false;

            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                StreamWriter sw = new StreamWriter("D:\\" + fileName);

                foreach (Room r in roomList)
                {
                    r.WriteToFile(sw);

                    sw.WriteLine("Booking Status of this Room:");

                    foreach (KeyValuePair<Booking, Room> reservation in bookedRooms)
                    {
                        if (reservation.Value.Equals(r))
                        {
                            reservation.Key.WriteDatesToFile(sw);
                            isBooked = true;
                        }
                    }

                    if (isBooked == false)
                        sw.WriteLine("This Room has not yet Booked");
                }

                // User to Dispose the StreamWriter Object. Adding this is important.
                sw.Dispose();

                Console.WriteLine("The Report is generated successfully and can be found at D:\\" + fileName);
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error accoured while generating Report");
            }
        }

        // This function displays a list of rooms that are not yet booked in specified dates
        // Takes a Booking object and roomSize as a parameter
        public void ListAvailableRooms(Booking wantedBooking, RoomSize roomSize)
        {
            // List of not available rooms
            List<int> notAvailable = new List<int>();

            foreach (KeyValuePair<Booking, Room> reservation in bookedRooms)
            {
                if (reservation.Key.GetRoomSize() == roomSize)
                {
                    // The Following check of (DateTime.Compare) has been taken from the Official Microsoft Documentation
                    // https://learn.microsoft.com/en-us/dotnet/api/system.datetime.compare?view=net-7.0#system-datetime-compare(system-datetime-system-datetime)
                    int compareCheckOut = DateTime.Compare(wantedBooking.GetCheckInDate(), reservation.Key.GetCheckOutDate());

                    // Overlap
                    if (compareCheckOut < 0)
                    {
                        // If there is an overlap, Add the roomNumber to the List of notAvailable rooms
                        notAvailable.Add(reservation.Key.GetRoomNumber());
                    }
                }
            }

            foreach (Room r in roomList)
            {
                if (r.GetSize() == roomSize && !notAvailable.Contains(r.GetRoomNumber()))
                {
                    r.DisplayRoomSpecs();
                    Console.WriteLine("");
                }
            }
        }

        // This function displays a list of rooms that are not yet booked in specified dates
        // Only rooms cheaper than the specified maxPrice are displayed
        // Takes a Booking object, macPrice and roomSize as a parameter
        public void ListAvailableRooms(Booking wantedBooking, RoomSize roomSize, int maxPrice)
        {
            // List of not available rooms
            List<int> notAvailable = new List<int>();

            foreach (KeyValuePair<Booking, Room> reservation in bookedRooms)
            {
                if (reservation.Key.GetRoomSize() == roomSize)
                {
                    // The Following check of (DateTime.Compare) has been taken from the Official Microsoft Documentation
                    // https://learn.microsoft.com/en-us/dotnet/api/system.datetime.compare?view=net-7.0#system-datetime-compare(system-datetime-system-datetime)
                    int compareCheckOut = DateTime.Compare(wantedBooking.GetCheckInDate(), reservation.Key.GetCheckOutDate());

                    // Overlap
                    if (compareCheckOut < 0)
                    {
                        notAvailable.Add(reservation.Key.GetRoomNumber());
                    }
                }
            }

            // The Following check of (List.OrderBy) has been taken from the Official Microsoft Documentation. Help from StackOverflow was also taken
            // https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.orderby?view=net-7.0
            // https://stackoverflow.com/questions/3309188/how-to-sort-a-listt-by-a-property-in-the-object
            List<Room> sortedlist = roomList.OrderBy(x => x.GetPrice()).ToList();


            foreach (Room r in sortedlist)
            {
                if (r.GetSize() == roomSize && r.GetPrice() <= maxPrice && !notAvailable.Contains(r.GetRoomNumber()))
                {
                    r.DisplayRoomSpecs();
                    Console.WriteLine("");
                }
            }
        }

        // This function is used to make a Booking
        // Add a Key value pair into the Dictionary that confirms the booking
        // Takes a Booking object and roomNumber as a parameter
        public bool BookRoom(int roomNumber, Booking wantedBooking)
        {
            bool alraedyBooked = Overlaps(wantedBooking);

            if (alraedyBooked)
            {
                Console.WriteLine("You can't book this room in the requested dates as it is overlapping with another booking.");
                return false;
            }
            else
            {
                foreach (Room r in roomList)
                {
                    if (r.GetRoomNumber() == roomNumber)
                    {
                        wantedBooking.SetRoomSize(r.GetSize());
                        bookedRooms.Add(wantedBooking, r);

                        Console.WriteLine("You have successfully booked a room");
                        Console.WriteLine("Details of the booked Room are as follows: ");
                        r.DisplayRoomSpecs();
                        Console.WriteLine("\nReservation Details:");
                        wantedBooking.DisplayDates();
                        Console.WriteLine();
                    }
                }
               
                return true;
            }
        }

        // This function checks if a new Booking overlaps with existing bookings
        // Takes a Booking object as a parameter
        public bool Overlaps(Booking other)
        {
            bool foundOverlap = false;

            foreach (KeyValuePair<Booking, Room> reservation in bookedRooms)
            {
                if (reservation.Value.GetRoomNumber() == other.GetRoomNumber())
                {
                    // The Following check of (DateTime.Compare) has been taken from the Official Microsoft Documentation
                    // https://learn.microsoft.com/en-us/dotnet/api/system.datetime.compare?view=net-7.0#system-datetime-compare(system-datetime-system-datetime)
                    int compareCheckOut = DateTime.Compare(other.GetCheckInDate(), reservation.Key.GetCheckOutDate());

                    // Overlap
                    if (compareCheckOut < 0)
                    {
                        foundOverlap = true;
                        break;
                    }
                }
            }

            return foundOverlap;
        }

        // This funcion is used to Add some rooms in the Room List.
        // This will be called in the constructor to add some rooms initially when the program starts
        private void InitialRoomList()
        {
            Room room1 = new StandardRoom(1, 1, RoomSize.SINGLE, 59.00);
            Room room2 = new StandardRoom(2, 1, RoomSize.DOUBLE, 139.00, 2);
            Room room3 = new StandardRoom(3, 2, RoomSize.DOUBLE, 109.00, 2);

            Room room4 = new DeluxRoom(4, 3, RoomSize.TRIPLE, 39.00, 20.00, View.MOUNTAIN);
            Room room5 = new DeluxRoom(5, 3, RoomSize.TRIPLE, 89.00, 40.00, View.SEA);
            Room room6 = new DeluxRoom(6, 3, RoomSize.DOUBLE, 249.00, 20.00, View.LANDMARK);

            roomList.Add(room1);
            roomList.Add(room2);
            roomList.Add(room3);
            roomList.Add(room4);
            roomList.Add(room5);
            roomList.Add(room6);
        }
    }
}