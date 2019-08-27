using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eFruitWorld.Models
{
    public class FruitModel
    {
        public int Id { get; set; }

        public int OrderNo { get; set; }

        public string FruitType { get; set; }

        public double Weight { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }
    }
}