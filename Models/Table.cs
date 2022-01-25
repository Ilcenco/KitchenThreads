using DiningHallThreads.StaticModels;
using KitchenThreads.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiningHallThreads.Models
{
    public class Table
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        public string Status { get; set; }
        public Order Order { get; set; }
        public Table(string status)
        {
            this.Id = Guid.NewGuid();
            this.Status = status;
            this.Index = Tables.c;
            Tables.c++;
        }
    }
}
