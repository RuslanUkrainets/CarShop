using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Title{ get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int? Year { get; set; }
        public double? Price { get; set; }
        public string Image { get; set; }
    }
}
