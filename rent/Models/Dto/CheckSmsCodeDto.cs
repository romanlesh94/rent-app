namespace PersonApi.Models.Dto
{
    public class CheckSmsCodeDto
    {
        public string Code { get; set; }
        public long PersonId { get; set; }
    }
}
