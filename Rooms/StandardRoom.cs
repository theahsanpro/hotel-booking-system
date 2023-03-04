using HotelManagementSystem.Enumirations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HotelManagementSystem.Rooms
{
    internal class StandardRoom : Room
    {
        private int windows;

        public StandardRoom(int roomNumber, int floorNumber, RoomSize size, double price, int windows = 1)
            :base(roomNumber, floorNumber, size,price)
        {
            this.windows = windows;
        }

        public int GetWindows()
        {
            return windows;
        }

        // This function is used to display the Room Specs on Console
        // It is an override version of a method present in its parent class
        public override void DisplayRoomSpecs()
        {
            base.DisplayRoomSpecs();
            Console.WriteLine("No. of Windows in the Room: " + this.windows);
        }

        // This function is used to append the Room specs into a File
        // It is an override version of a method present in its parent class
        // It takes a StreamWriter object as a parameter
        public override void WriteToFile(StreamWriter sw)
        {
            base.WriteToFile(sw);
            sw.WriteLine("No. of Windows in the Room: " + this.windows);
        }
    }
}
