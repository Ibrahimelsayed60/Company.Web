using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data.Models
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public decimal Salary { get; set; }

        public String Email { get; set; }

        public String PhoneNumber { get; set; }

        public DateTime HiringDate { get; set; }

        //public IFormFile Image { get; set; }

        public string? ImgeUrl { get; set; }

        public Department? Department { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
    }
}
