using eFruitWorld.Models;
using eFruitWorld.Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eFruitWorld.Context
{
    public class CartContext : DbContext
    {
        public CartContext()
        {

        }
        public DbSet<Fruit> Fruit { get; set; }
        public DbSet<ShippingDetails> ShippingDetails { get; set; }
        public DbSet<User> User { get; set; }
    }
}