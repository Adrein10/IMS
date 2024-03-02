using System;
using System.Collections.Generic;

namespace IMS.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public string Image { get; set; } = null!;
    }
}
