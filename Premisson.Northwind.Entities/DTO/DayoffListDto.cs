using System;
using System.Collections.Generic;
using System.Text;

namespace Premisson.Northwind.Entities.DTO
{
   public class DayoffListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }   
        public int DayoffTypeId { get; set; }
        public string DayoffTypeName { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Dayoff_Location { get; set; }
        public int ProxyUser_Id { get; set; }
        public string Dayoff_Description { get; set; }
        public bool? IsApprove { get; set; }

    }
}
