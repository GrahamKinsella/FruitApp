using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eFruitWorld.Objects
{
    public class OrderDetails
    {

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int ShippingDetailsId { get; set; }

        public double TotalCost { get; set; }

        public double TotalWeight { get; set; }

        public List<Fruit> Fruits { get; set; }
    }
}