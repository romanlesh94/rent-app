using PersonApi.Entities;
using System;

namespace PersonApi.Models
{
    public class PhoneVerification : BaseEntity
    {
        public long PersonId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Code { get; set; }
        public bool IsVerified { get; set; }
    }
}
