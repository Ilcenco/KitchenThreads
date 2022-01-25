using KitchenThreads.StaticModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace KitchenThreads.Models
{
    public class Cook
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public int Proficiency { get; set; }
        public string CatchPhrase { get; set; }

        public Cook(string name, int rank, int proficiency)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Rank = rank;
            this.Proficiency = proficiency;
            this.CatchPhrase = "Okaaay let's gooo";
        }

        public void WorkMain()
        {
            var rnd = new Random();
            while (true)
            {
                Thread.Sleep(rnd.Next(5, 100));
                var item = PickItem();
                if (item != null)
                {
                    if (item.Item.CookingApparatus != null)
                    {
                        var cookingApparatus = PickCookingApparatus(item.Item.CookingApparatus);

                        if (cookingApparatus != null)
                        {
                            lock (OrderItems.ItemsList)
                            {
                                System.Diagnostics.Debug.WriteLine(Environment.NewLine);
                                System.Diagnostics.Debug.WriteLine(Name + " took item " + JsonConvert.SerializeObject(item) + " from order " + item.OrderId);
                                System.Diagnostics.Debug.WriteLine(Environment.NewLine);
                                //item.Item = null;
                            }

                            Thread.Sleep((int)item.Item.PreparationTime * 1000);
                            //sleep time

                            lock (OrderItems.ItemsList)
                            {
                                System.Diagnostics.Debug.WriteLine(Environment.NewLine);
                                System.Diagnostics.Debug.WriteLine(Name + " prepared item " + JsonConvert.SerializeObject(item) + " from order " + item.OrderId);
                                System.Diagnostics.Debug.WriteLine(Environment.NewLine);
                                OrderItems.ItemsList.Remove(item);
                            }

                            
                            lock (CookingApparatusList.apparatusList)
                            {
                                CookingApparatusList.apparatusList.FirstOrDefault(c => c.Id == cookingApparatus.Id).IsBeingUsed = false;
                            }
                        }

                    }
                    else
                    {
                        lock (OrderItems.ItemsList)
                        {
                            System.Diagnostics.Debug.WriteLine(Environment.NewLine);
                            System.Diagnostics.Debug.WriteLine(Name + " took item " + JsonConvert.SerializeObject(item) + " from order " + item.OrderId);
                            System.Diagnostics.Debug.WriteLine(Environment.NewLine);
                            //item.Item = null;
                        }

                        // sleep time
                        lock (OrderItems.ItemsList)
                        {
                            System.Diagnostics.Debug.WriteLine(Environment.NewLine);
                            System.Diagnostics.Debug.WriteLine(Name + " prepared item " + JsonConvert.SerializeObject(item) + " from order " + item.OrderId);
                            System.Diagnostics.Debug.WriteLine(Environment.NewLine);
                            //OrderItems.ItemsList.Remove(item);
                        }

                    }
                    
                }
                
            }
        }
        private OrderItem PickItem()
        {
            OrderItem item;
            lock (OrderItems.ItemsList)
            {
                if(OrderItems.ItemsList.Count != 0)
                {
                    var orderItems = OrderItems.ItemsList.ToList();
                    item = orderItems.FirstOrDefault(i => i.Item.Complexity <= Rank && i.Item != null);
                    OrderItems.ItemsList.Remove(item);
                    return item;
                }
            }
            return item = null;
        }
        private CookingApparatus PickCookingApparatus(string type)
        {
            CookingApparatus cookingApparatus;
            List<CookingApparatus> cookingApparatusList;

            lock (CookingApparatusList.apparatusList)
            {
                if (CookingApparatusList.apparatusList.ToList() != null)
                {
                    cookingApparatusList = CookingApparatusList.apparatusList.ToList();
                    cookingApparatus = cookingApparatusList.Where(c => c.Name == type && c.IsBeingUsed == false).FirstOrDefault();
                    if(cookingApparatus != null)
                    {
                        cookingApparatus.IsBeingUsed = true;
                        return cookingApparatus;
                    }
                }
            }
            return cookingApparatus = null;
        }

    }
}
