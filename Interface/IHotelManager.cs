using HotelManagementSystem.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Interface
{
    internal interface IHotelManager
    {
        public bool AddRoom(Room room);
        public bool DeleteRoom(int roomNumber);
        public void ListRooms();
        public void ListRoomsOrderedByPrice();
        public void GenerateReport(string fileName);
    }
}
