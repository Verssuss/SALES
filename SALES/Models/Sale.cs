using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALES.Models
{
    class Sale
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime SaleDate { get; set; }

    }
}
