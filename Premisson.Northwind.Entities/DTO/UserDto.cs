using System;
using System.Collections.Generic;
using System.Text;

namespace Premisson.Northwind.Entities.DTO
{
   public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }
}
