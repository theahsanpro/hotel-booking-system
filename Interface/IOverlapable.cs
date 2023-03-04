using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem.Interface
{
    internal interface IOverlapable
    {
        public bool Overlaps(Booking other);
    }
}
