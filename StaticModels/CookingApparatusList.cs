using KitchenThreads.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KitchenThreads.StaticModels
{
    public static class CookingApparatusList
    {
        public static int c = 0;
        public static Mutex mutex = new Mutex();
        public static List<CookingApparatus> apparatusList = new List<CookingApparatus>() 
        {
            new CookingApparatus("oven"),
            new CookingApparatus("oven"),
            new CookingApparatus("stove"),
            new CookingApparatus("stove")
        };
    }
}
