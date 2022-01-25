using KitchenThreads.Models;
using KitchenThreads.Service;
using KitchenThreads.StaticModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KitchenThreads.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var start = new Start();
            return View();
        }

        
    }
}
