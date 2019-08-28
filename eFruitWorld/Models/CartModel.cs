using eFruitWorld.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eFruitWorld.Models
{
    public class CartModel
    {

        public List<Fruit> Cart = new List<Fruit>();
        public int TotalAmount { get; set; }

        public string Type { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public int Amount { get; set; }

        public Fruit GetBanana()
        {
            Fruit fruit = new Fruit
            {
                FruitType = "Banana",
                Price = .35,
                Weight = .25,
                Amount = 1
            };

            return fruit;
        }

        public Fruit GetApple()
        {
            Fruit fruit = new Fruit
            {
                FruitType = "Apple",
                Price = .50,
                Weight = .15,
                Amount = 1
            };

            return fruit;
        }

        public Fruit GetOrange()
        {
            Fruit fruit = new Fruit
            {
                FruitType = "Orange",
                Price = .40,
                Weight = .2,
                Amount = 1
            };

            return fruit;
        }
    }
}