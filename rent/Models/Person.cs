namespace rent.Entities
{
    public class Person : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
    }
}
