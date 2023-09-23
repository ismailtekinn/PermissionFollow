using Premisson.Northwind.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Premisson.Northwind.Entities.Concreate
{
   public class Role : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string  Name { get; set; }
        public bool IsDelete { get; set; }
    }
}
