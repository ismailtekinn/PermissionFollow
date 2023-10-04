using System;
using System.Collections.Generic;
using System.Text;

namespace Premisson.Northwind.Entities.DTO
{
    public class PersonelListDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Birim { get; set; }
        public string RoleName { get; set; }
        public string IsDelete { get; set; }
        public bool IsActive { get; set; }
        public string CreatedAt { get; set; }
        public int DepartmentId { get; set; }
        public bool IsManager { get; set; }
    }
}
