using KitchenThreads.Models;
using KitchenThreads.StaticModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace KitchenThreads.Controllers
{
    [Route("api")]
    public class OrderController : Controller
    {
        [Route("postOrder")]
        [HttpPost]
        public IActionResult Index([FromBody]Order order)
        {
            lock (Orders.OrdersList)
            {
                lock (OrderItems.ItemsList)
                {
                    foreach (var item in order.ItemsObjectList)
                    {
                        OrderItems.ItemsList.Add(new OrderItem(order, item));
                        OrderItems.c++;
                        OrderItems.ItemsList =  OrderItems.ItemsList.OrderBy(i => i.Max_Wait * i.Priority).ToList();
                    }
                }

                Orders.OrdersList.Add(order);
                Orders.c++;
                Orders.OrdersList =  Orders.OrdersList.OrderBy(i => i.MaxWait * i.Priority).ToList();
            }
            return View();
        }
    }
}
