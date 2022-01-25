using KitchenThreads.Models;
using KitchenThreads.StaticModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace KitchenThreads.Service
{
    public class Start
    {
        public List<Cook> Cookers = new List<Cook>()
        {
            new Cook("Chief 4", 3, 4),
            new Cook("Chief 3", 2 , 3),
            new Cook("Saucier1 2", 2, 2),
            new Cook("Line 1", 1, 2),
        };
        public Start()
        {
            Thread checkCompleted = new Thread(CheckForCompleteOrders);
            checkCompleted.Start();
            lock (Cookers)
            {
                foreach (var cook in Cookers)
                {
                    for (int i = 0; i < cook.Proficiency; i++)
                    {
                        Thread cookerThread = new Thread(cook.WorkMain);
                        cookerThread.Start();
                        System.Diagnostics.Debug.WriteLine(cook.Name + " MainThread" + " started");
                    }
                }
            }
            
        }

        public void CheckForCompleteOrders()
        {

            while (true)
            {
                lock (Orders.OrdersList)
                {
                    var list = Orders.OrdersList.ToList();
                    foreach (var order in list)
                    {
                        if (order != null && OrderItems.ItemsList.Where(i => i.OrderId == order.Id).ToList().Count == 0)
                        {
                            System.Diagnostics.Debug.WriteLine(Environment.NewLine);
                            System.Diagnostics.Debug.WriteLine(order.Id + " order is ready");
                            System.Diagnostics.Debug.WriteLine(Environment.NewLine);
                            Orders.OrdersList.Remove(order);
                            PushOrder(order);
                        }
                    }
                }
            }
            
        }
        public void PushOrder(Order order)
        {
            using (var client = new HttpClient())
            {
                var postTask = client.PostAsJsonAsync<Order>("https://localhost:44337/api/distributeOrder", order);
                postTask.Wait();
                var result = postTask.Result;
            }

        }

    }
}
