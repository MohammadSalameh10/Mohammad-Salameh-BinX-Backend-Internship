using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionsLinqDay4
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }

        public Product(int Id, string Name, double Price, bool IsAvailable) { 
           this.Id = Id;
           this.Name = Name;
           this.Price = Price;
           this.IsAvailable = IsAvailable;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Price: {Price}, Available: {IsAvailable}";
        }
    }
}
