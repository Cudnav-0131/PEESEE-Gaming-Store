using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PEESEE_GAMING.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Series { get; set; }
        public string Type { get; set; }

        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public int Discount { get; set; }

        public string Image { get; set; }

        public decimal Rating { get; set; }
        public int Sold { get; set; }
        public int Stock { get; set; }

        public string Description { get; set; }

        public Dictionary<string, string> Specifications
        {
            get;
            set;
        }

        public Product()
        {
            Specifications = new Dictionary<string, string>();
        }
    }
}