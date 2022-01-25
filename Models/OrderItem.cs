using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenThreads.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Item Item { get; set; }
        public int Priority { get; set; }
        public double Max_Wait { get; set; }
        public int MinutesInKitchen { get; set; }
        
        public OrderItem(Order order, Item item)
        {
            this.Id = Guid.NewGuid();
            this.OrderId = order.Id;
            this.Item = item;
            this.Priority = order.Priority;
            this.Max_Wait = order.MaxWait;
        }
    }
}
