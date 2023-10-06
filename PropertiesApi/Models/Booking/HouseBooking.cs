using System;

namespace HouseApi.Models.Booking
{
    public class HouseBooking: BaseEntity
    {
        public long HouseId { get; set; }
        public long GuestId { get; set; }
        public long Price { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set;}

    }
}
