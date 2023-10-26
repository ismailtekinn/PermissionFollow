using System;
using System.Collections.Generic;
using System.Text;

namespace Premisson.Northwind.Entities.DTO
{
    public class PermissionDto
    {
        public int DayoffTypeId { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Dayoff_Location { get; set; }
        public int ProxyUserId { get; set; }
        public string DayoffDescription { get; set; }
    }
}
