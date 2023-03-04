using HotelManagementSystem.Enumirations;
using HotelManagementSystem.Interface;
using HotelManagementSystem.Rooms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace HotelManagementSystem
{
    internal class Booking
    {
        private int roomNumber;
        private DateTime checkinDate;
        private DateTime checkoutDate;
        private RoomSize roomSize; // Enum

        public Booking(DateTime checkinDate, DateTime checkoutDate, RoomSize size)
        {
            this.checkinDate = checkinDate;
            this.checkoutDate = checkoutDate;
            this.roomSize = size;
        }

        // Constructor Overloading with a different parameter
        public Booking(DateTime checkinDate, DateTime checkoutDate, int roomNumber)
        {
            this.checkinDate = checkinDate;
            this.checkoutDate = checkoutDate;
            this.roomNumber = roomNumber;
        }

        public DateTime GetCheckOutDate()
        {
            return checkoutDate;
        }

        public DateTime GetCheckInDate()
        {
            return checkinDate;
        }

        public RoomSize GetRoomSize() 
        {
            return roomSize;
        }

        public void SetRoomSize(RoomSize size)
        {
            this.roomSize = size;
        }

        public int GetRoomNumber()
        {
            return roomNumber;
        }

        // This function is used to display the Check-in/Check-out Dates on the Console
        public void DisplayDates()
        {
            Console.WriteLine("From " + this.checkinDate.Date.ToShortDateString() + " to " + this.checkoutDate.Date.ToShortDateString());
        }

        // This function is used to append the Check-in/Check-out Dates in a file
        // It takes a StreamWriter object as a parameter
        public void WriteDatesToFile(StreamWriter sw)
        {
            sw.WriteLine("From " + this.checkinDate.Date + " to " + this.checkoutDate.Date);
        }
    }
}
