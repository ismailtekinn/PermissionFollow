using System;
using System.Collections.Generic;
using System.Text;

namespace Premisson.Northwind.Entities.DTO
{
    public class MainInformationDto
    {
        public MainInformationDto(string text, int count)
        {
            Text = text;
            Count = count;
        }
        public string Text { get; set; }
        public int Count { get; set; }
    }
}
