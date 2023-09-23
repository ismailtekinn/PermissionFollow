using Premisson.Northwind.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Premisson.Northwind.Entities.Concreate
{
    public class UserDepartment: IEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Deparment Deparment { get; set; }
        public bool IsManager { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
