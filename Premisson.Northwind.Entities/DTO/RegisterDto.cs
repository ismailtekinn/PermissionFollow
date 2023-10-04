using System;
using System.Collections.Generic;
using System.Text;

namespace Premisson.Northwind.Entities.DTO
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public bool IsManager { get; set; }
    }
}
