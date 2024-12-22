using NotiblingBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Domain.Entities
{
    public class Employee : User
    {
        public Guid EmployeeId { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
    }
}
