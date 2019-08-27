
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
    public class HomeController : Controller
    {
        CartModel Model = new CartModel();

        public ActionResult Index()
        {
            Model.Cart = (List<Fruit>)Session["cart"];
            if (Session["message"] != null)
            {
                ViewBag.Message = Session["message"].ToString();
                Session.Remove("message");
            }
            return View(Model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddToCart(string fruitType)
        {
            Fruit fruit = null;
            var weight = Convert.ToDouble(Session["weight"]);
            switch (fruitType.ToLower())
            {
                case "banana":
                    fruit = Model.GetBanana();
                    break;
                case "apple":
                    fruit = Model.GetApple();
                    break;
                case "orange":
                    fruit = Model.GetOrange();
                    break;
            }
 
            //check if the session object exists and create if not
            if (Session["cart"] == null)
            {
                Model.Cart.Add(fruit);
                Session["cart"] = Model.Cart;
                weight = fruit.Weight;
                Session["weight"] = weight;
            }

            //exists so get it and increase amount if its not over limit of 3kg
            else
            {
                Model.Cart = (List<Fruit>)Session["cart"];
                int i = GetFruitIndex(fruit.FruitType);
                if (i != -1)
                {
                    if (weight + fruit.Weight <= 3.0)
                    {
                        Model.Cart[i].Amount++;
                        weight += fruit.Weight;
                        Model.Cart[i].Price = Model.Cart[i].Price += fruit.Price;
                        Model.Cart[i].Weight = Model.Cart[i].Weight += fruit.Weight;
                        Session["weight"] = weight;
                    }
                    else
                    {
                        Session["message"] = "Weight Limit Reached";
                    }
                }

                //if the list has been initialised in session but fruit hasnt been added yet
                else
                {
                    if (weight + fruit.Weight <= 3.0)
                    {
                        Model.Cart.Add(fruit);
                        weight += fruit.Weight;
                        Session["weight"] = weight;
                    }
                    else
                    {
                        Session["message"] = "Weight Limit Reached";
                    }
                }

                Session["cart"] = Model.Cart;
            }
            return RedirectToAction("Index");
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult RemoveFromCart(string fruitType)
        {
            Fruit fruit = null;
            var weight = Convert.ToDouble(Session["weight"]);
            switch (fruitType.ToLower())
            {
                case "banana":
                    fruit = Model.GetBanana();
                    break;
                case "apple":
                    fruit = Model.GetApple();
                    break;
                case "orange":
                    fruit = Model.GetOrange();
                    break;
            }
            Model.Cart = (List<Fruit>)Session["cart"];
            int i = GetFruitIndex(fruit.FruitType);
            if (i >= 0 && Model.Cart[i].Amount != 0)
            {
                Model.Cart[i].Amount--;
                Model.Cart[i].Price = Model.Cart[i].Price - fruit.Price;
                Session["cart"] = Model.Cart;
                weight = weight - fruit.Weight;
                Model.Cart[i].Weight = Model.Cart[i].Weight - fruit.Weight;
                Session["weight"] = weight;
            }
            if(Model.Cart[i].Amount == 0)
            {
                Model.Cart.RemoveAt(i);
            }
            else
            {
                //handle this better
            }

            return RedirectToAction("Index");
        }

        private int GetFruitIndex(string fruitType)
        {

            Model.Cart = (List<Fruit>)Session["cart"];
            if (Model.Cart != null)
            {
                for (int i = 0; i < Model.Cart.Count; i++)
                    if (Model.Cart[i].FruitType.Equals(fruitType))
                        return i;
            }
            return -1;
        }

        public ActionResult ClearCart()
        {
            Session["cart"] = new List<Fruit>();
            Session["weight"] = 0.0;
            return RedirectToAction("Index");
        }

        public ActionResult Checkout()
        {
            return View("Checkout");
        }
    }
}