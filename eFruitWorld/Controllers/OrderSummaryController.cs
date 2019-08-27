using eFruitWorld.Context;
using eFruitWorld.Models;
using eFruitWorld.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eFruitWorld.Controllers
{
    public class OrderSummaryController : Controller
    {
        // GET: OrderSummary
        public ActionResult OrderSummary()
        {
            return View();
        }

        public ActionResult GetOrders()
        {
            OrderSummaryModel Model = new OrderSummaryModel();
            using (var db = new CartContext())
            {
                Model.Cart = (List<Fruit>)Session["cart"];
                var results = db.Fruit.GroupJoin(db.ShippingDetails, x => x.ShippingDetailsId, y => y.ShippingDetailsId, (x, y) => x);
                Model.Orders = new List<OrderDetails>();
                foreach (var f in results)
                {
                    if (f.ShippingDetails.Username == Session["Username"].ToString())
                    {
                        OrderDetails details = new OrderDetails
                        {
                            Name = f.ShippingDetails.Name,
                            Address = f.ShippingDetails.Address,
                            City = f.ShippingDetails.City,
                            Country = f.ShippingDetails.Country,
                            ShippingDetailsId = f.ShippingDetails.ShippingDetailsId,
                            TotalCost = f.Price,
                            TotalWeight = f.Weight,
                            Fruits = new List<Fruit>()
                        };

                        int i = CheckList(f, Model);
                        if (i != -1)
                        {
                            Model.Orders[i].Fruits.Add(f);
                            Model.Orders[i].TotalCost += f.Price;
                            Model.Orders[i].TotalWeight += f.Weight;
                        }
                        else
                        {
                            details.Fruits.Add(f);
                            Model.Orders.Add(details);
                        }
                    }
                }
                return View("OrderSummary", Model);
            }
        }


        private int CheckList(Fruit fruit, OrderSummaryModel Model)
        {

            if (Model.Orders != null)
            {
                for (int i = 0; i < Model.Orders.Count; i++)
                    if (Model.Orders[i].ShippingDetailsId.Equals(fruit.ShippingDetailsId))
                        return i;
            }
            return -1;
        }
    }
}