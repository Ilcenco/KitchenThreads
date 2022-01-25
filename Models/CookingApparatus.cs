using KitchenThreads.StaticModels;
using System;

namespace KitchenThreads.Models
{
    public class CookingApparatus
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public bool IsBeingUsed { get; set; }

        public CookingApparatus(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Index = CookingApparatusList.c;
            CookingApparatusList.c++;
        }
    }
}
