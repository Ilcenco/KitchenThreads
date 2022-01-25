using KitchenThreads.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KitchenThreads.StaticModels
{
    public static class Orders
    {
        public static int c = 0;
        public static Mutex mutex = new Mutex();
        public static List<Order> OrdersList = new List<Order>()
        {

        };
    }
}
