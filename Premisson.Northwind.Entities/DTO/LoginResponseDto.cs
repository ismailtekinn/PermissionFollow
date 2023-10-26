using System;
using System.Collections.Generic;
using System.Text;

namespace Premisson.Northwind.Entities.DTO
{
   public  class LoginResponseDto
    {
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleId { get; set; }
        public string IsActive { get; set; }
    }
}
