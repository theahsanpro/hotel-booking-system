using HotelManagementSystem.Enumirations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HotelManagementSystem.Rooms
{
    internal class DeluxRoom : Room
    {
        double balconySize;
        View roomView;

        public DeluxRoom(int roomNumber, int floorNumber, RoomSize size, double price, double balconySize, View view)
            :base(roomNumber, floorNumber, size, price)
        {
            this.balconySize = balconySize;
            this.roomView = view;
        }

        public double GetBalconySize()
        {
            return balconySize;
        }

        // View is a Enumiration structure defined in Enumiration class
        public View GetView()
        {
            return roomView;
        }

        // This function is used to display the Room Specs on Console
        // It is an override version of a method present in its parent class
        public override void DisplayRoomSpecs()
        {
            base.DisplayRoomSpecs();
            Console.WriteLine("Balcony Size in sq. meters: " + this.balconySize);
            Console.WriteLine("View from the room: " + this.roomView);
        }

        // This function is used to append the Room specs into a File
        // It is an override version of a method present in its parent class
        // It takes a StreamWriter object as a parameter
        public override void WriteToFile(StreamWriter sw)
        {
            base.WriteToFile(sw);
            sw.WriteLine("Balcony Size in sq. meters: " + this.balconySize);
            sw.WriteLine("View from the room: " + this.roomView);

        }
    }
}
