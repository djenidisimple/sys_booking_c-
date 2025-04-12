using System;

namespace Booking_tiket
{
    public class DataEventArgs : EventArgs
    {
        public string Data { get; private set; }

        public DataEventArgs(string data)
        {
            Data = data;
        }
    }
}
