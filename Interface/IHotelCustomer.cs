using HotelManagementSystem.Enumirations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HotelManagementSystem.Interface
{
    internal interface IHotelCustomer 
    {
        public void ListAvailableRooms(Booking wantedBooking, RoomSize roomSize);
        public void ListAvailableRooms(Booking wantedBooking, RoomSize roomSize, int maxPrice);
        public bool BookRoom(int roomNumber, Booking wantedBooking);
    }
}
