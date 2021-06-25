using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALES.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime SaleDate { get; set; }
        public int Sum { get; set; }

    }
}
