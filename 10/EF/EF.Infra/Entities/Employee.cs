using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Infra.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<CreditCard> CreditCards { get; set; } = new List<CreditCard>();
    }
}
