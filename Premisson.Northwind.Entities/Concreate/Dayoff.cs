using Premisson.Northwind.Entities;
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
        public int DayoffTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Dayoff_Location { get; set; }
        public int ProxyUserId { get; set; }
        public int UserId { get; set; }
        public string DayoffDescription { get; set; }
        public bool? IsApprove { get; set; }
        public string FileName { get; set; }

        [ForeignKey("DayoffTypeId")]
        public DayoffType DayoffType { get; set; }

        [ForeignKey("ProxyUserId")]
        public User ProxyUser { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
