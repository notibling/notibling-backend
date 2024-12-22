 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotiblingBackend.Domain.Enums;


namespace NotiblingBackend.Domain.Entities
{
    public class Customer : User
    {
        public Guid CustomerId { get; private set; }
        public string? IdentityDocument { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
    }
}
