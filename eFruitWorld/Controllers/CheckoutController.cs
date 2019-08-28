using eFruitWorld.Context;
using eFruitWorld.Models;
using eFruitWorld.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace eFruitWorld.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View("Checkout");
        }

        [HttpPost]
        public ActionResult CompleteCheckout(ShippingDetailsModel Model)
        {
            var regex = "[0-9]{4}-[0-9]{4}-[0-9]{4}-[0-9]{4}";
            if (Model.CreditCard != null && Regex.IsMatch(Model.CreditCard, regex))
            {
                var details = AddShippingDetails(Model);
                AddFruits(Model, details);

                Session["cart"] = new List<Fruit>();
                Session["weight"] = 0.0;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Credit card number must be in xxxx-xxxx-xxxx-xxxx format";
                return View("Checkout");
            }
        }

        private ShippingDetails AddShippingDetails(ShippingDetailsModel Model)
        {
            ShippingDetails order = null;
            using (var db = new CartContext())
            {
                var ShippingDetails = new ShippingDetails()
                {
                    Name = Model.Name,
                    Address = Model.Address,
                    City = Model.City,
                    Country = Model.Country,
                    Username = Session["Username"].ToString()
                };
                order = db.ShippingDetails.Add(ShippingDetails);
                db.SaveChanges();
            }
            return order;
        }


        private void AddFruits(ShippingDetailsModel Model, ShippingDetails details)
        {
            using (var db = new CartContext())
            {
                var fruits = (List<Fruit>)Session["cart"];
                foreach (var f in fruits)
                {
                    var fruit = new Fruit()
                    {
                        FruitType = f.FruitType,
                        Amount = f.Amount,
                        Price = f.Price,
                        Weight = f.Weight,
                        ShippingDetails = details
                    };

                    db.Fruit.Add(fruit);
                    db.SaveChanges();
                }
            }
        }
    }
}