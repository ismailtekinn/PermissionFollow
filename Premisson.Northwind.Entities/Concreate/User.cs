using Premisson.Northwind.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Premisson.Northwind.Entities.Concreate
{
    public class User:IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public UserDepartment UserDepartment { get; set; }



    }
}
