using System;

namespace HouseApi.Models.Dto
{
    public class AddHouseBookingDto
    {
        public long HouseId { get; set; }
        public long GuestId { get; set; }
        public long Price { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }

    }
}
