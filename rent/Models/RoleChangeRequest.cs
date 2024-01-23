using PersonApi.Entities;
using System;

namespace PersonApi.Models
{
    public class RoleChangeRequest : BaseEntity
    {
        public long PersonId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsApproved { get; set; }
    }
}
