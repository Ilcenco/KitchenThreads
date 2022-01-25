using DiningHallThreads.Models;
using DiningHallThreads.Service;
using DiningHallThreads.StaticModels;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;

namespace KitchenThreads.Models
{
    public class Waiter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Waiter(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public void Work()
        {
            var rnd = new Random();
            while (true)
            {
                Thread.Sleep(100);

                Tables.mutex.WaitOne();
                var table = Tables.TableList.FirstOrDefault(t => t.Status == "waiting to be served");
                if (table != null)
                {
                    System.Diagnostics.Debug.WriteLine(Name + " entered table " + table.Index);
                    Thread.Sleep(rnd.Next(500, 5000));

                    Order order = new Order(table.Id, Id);
                    System.Diagnostics.Debug.WriteLine(Name + " took order " + JsonConvert.SerializeObject(order) + " from table " + table.Index);
                    table.Order = order;

                    table.Status = "waiting for order";
                    System.Diagnostics.Debug.WriteLine(Name + " is leaving table " + table.Index);
                }
                Tables.mutex.ReleaseMutex();
            }
        }
    }
}
