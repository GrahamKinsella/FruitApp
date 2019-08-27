using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eFruitWorld.Objects
{
    public class Fruit
    {
        [Key]
        public int FruitId { get; set; }

        public string FruitType { get; set; }

        public double Weight { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }

        public int ShippingDetailsId { get; set; }

        [ForeignKey("ShippingDetailsId")]
        public virtual ShippingDetails ShippingDetails { get; set; }

    }
}