using HotelManagementSystem.Enumirations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;

namespace HotelManagementSystem.Rooms
{
    internal abstract class Room: IComparable<Room>
    {
        private int roomNumber;
        private int floor;
        private double price;
        private RoomSize roomSize; // Enum

        public Room(int roomNumber, int floorNumber, RoomSize size, double price)
        {
            this.roomNumber = roomNumber;
            this.floor = floorNumber;
            this.price = price;
            this.roomSize = size;
        }

        public int GetRoomNumber()
        {
            return roomNumber;
        }

        public void SetRoomNumber(int number)
        {
            this.roomNumber = number;
        }

        public int GetFloor()
        {
            return floor;
        }

        public void SetFloor(int floor)
        {
            this.floor= floor;
        }

        public RoomSize GetSize()
        {
            return roomSize;
        }

        public void SetSize(RoomSize size)
        {
            this.roomSize= size;
        }

        public double GetPrice()
        {
             return price;
        }

        public void SetPrice(double price)
        {
            this.price = price;
        }

        // This is the implementation of IComparable interface
        // Takes a Room object as a parameter
        public int CompareTo(Room other)
        {
            return this.price.CompareTo(other.price);
        }

        // This function is used to display the Room Specs on Console
        // This function is overriden in its child class
        public virtual void DisplayRoomSpecs()
        {
            Console.WriteLine("\nRoom Number: " + this.roomNumber);
            Console.WriteLine("Floor Number: " + this.floor);
            Console.WriteLine("Room Size: " + this.roomSize);
            Console.WriteLine("Price per Night: $" + this.price);
        }

        // This function is used to append the Room specs into a File
        // This function is overriden in its child class
        // Takes a StreamWriter object as a parameter
        public virtual void WriteToFile(StreamWriter sw)
        {
            sw.WriteLine("\nRoom Number: " + this.roomNumber);
            sw.WriteLine("Floor Number: " + this.floor);
            sw.WriteLine("Room Size: " + this.roomSize);
            sw.WriteLine("Price per Night: $" + this.price);
        }
    }
}
