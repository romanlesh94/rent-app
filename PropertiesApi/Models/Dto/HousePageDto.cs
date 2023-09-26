using System.Collections;

namespace HouseApi.Models.Dto
{
    public class HousePageDto
    {
        public HousePageDto() { }

        public HousePageDto(House house, IEnumerable properties)
        {
            House = house;
            Properties = properties;
        }

        public House House { get; set; }
        public IEnumerable Properties { get; set; }
    }
}
