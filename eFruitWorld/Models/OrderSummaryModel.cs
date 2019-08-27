using eFruitWorld.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eFruitWorld.Models
{
    public class OrderSummaryModel
    {

        public Fruit Fruit { get; set; }
        public List<OrderDetails> Orders { get; set; }
        public List<Fruit> Cart = new List<Fruit>();
    }
}