using System;

namespace HouseApi.Models.Dto
{
    public class GuestBookingDto
    {
        public long HouseId { get; set; }
        public long GuestId { get; set; }
        public long Price { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string HouseName { get; set; }
        public string HouseAddress { get; set; }
    }
}
