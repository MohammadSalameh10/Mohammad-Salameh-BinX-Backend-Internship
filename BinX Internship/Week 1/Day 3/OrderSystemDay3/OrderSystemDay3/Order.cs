using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystemDay3
{
    internal class Order : INotifiable
    {
        private Product product;
        private int quantity;
        private Customer customer;

        public Product Product
        {
            get { return product; }
        }

        public int Quantity
        {
            get { return quantity; }
        }

        public Customer Customer
        {
            get { return customer; }
        }

        public Order(Product product, int quantity, Customer customer)
        {
            this.product = product;
            this.quantity = quantity;
            this.customer = customer;
        }

        public void SendNotification()
        {
            Console.WriteLine(
                $"Order notification sent for {product.Name} with quantity {quantity}."
            );
        }

        public override string ToString()
        {
            return $"Product: {product.Name} - Quantity: {quantity} - Customer: {customer.Name}";
        }
    }
}
