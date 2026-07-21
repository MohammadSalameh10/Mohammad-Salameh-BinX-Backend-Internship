using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystemDay3
{
    internal class Product
    {
        private string name;
        private int stockQuantity;

        public string Name
        {
            get { return name; }
        }

        public int StockQuantity
        {
            get { return stockQuantity; }
        }

        public Product(string name, int stockQuantity)
        {
            this.name = name;

            if (stockQuantity < 0)
            {
                this.stockQuantity = 0;
            }
            else
            {
                this.stockQuantity = stockQuantity;
            }
        }

        public bool IsAvailable(int requestedQuantity)
        {
            return requestedQuantity > 0 && requestedQuantity <= stockQuantity;
        }

        public void ReduceStock(int requestedQuantity)
        {
            if (IsAvailable(requestedQuantity))
            {
                stockQuantity -= requestedQuantity;
            }
        }

        public override string ToString()
        {
            return $"Product: {name} - Available Quantity: {stockQuantity}";
        }
    }
}
