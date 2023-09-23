using Premisson.Northwind.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Premisson.Northwind.Entities.Concreate
{
    public class Dayoff : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int Dayoff_Type_Id { get; set; }
        [ForeignKey("Dayoff_Type_Id")]
        public DayoffType DayoffType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Dayoff_Location { get; set; }
        public int ProxyUserId { get; set; }
        [ForeignKey("ProxyUserId")]
        public User ProxyUser { get; set; }
        public int User_Id { get; set; }
        [ForeignKey("User_Id")] 
        public User User { get; set; }
        public string Dayoff_Description { get; set; }
        public bool? Is_Approve { get; set; }
        public string File_Name { get; set; }

    }
}
