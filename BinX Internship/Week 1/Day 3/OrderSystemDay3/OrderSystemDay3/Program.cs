namespace OrderSystemDay3
{
    internal class Program
    {
        static void Notify(INotifiable target)
        {
            target.SendNotification();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Order System:");
            Console.WriteLine();

            Dictionary<string, Product> products = new Dictionary<string, Product>();

            products.Add("Laptop", new Product("Laptop", 5));
            products.Add("Phone", new Product("Phone", 10));
            products.Add("Keyboard", new Product("Keyboard", 3));

            CreateOrderRequest request = new CreateOrderRequest("Laptop", 2);

            Customer customer = new Customer("Mohammad Salameh", "mohammad@gmail.com");

            Console.WriteLine("Create Order Request:");
            Console.WriteLine($"Product: {request.ProductName}");
            Console.WriteLine($"Requested Quantity: {request.Quantity}");

            Console.WriteLine();
            Console.WriteLine("==============================================================");
            Console.WriteLine();

            if (!products.ContainsKey(request.ProductName))
            {
                Console.WriteLine("The requested product does not exist.");
            }
            else
            {
                Product selectedProduct = products[request.ProductName];

                Console.WriteLine("Product Information:");
                Console.WriteLine(selectedProduct);

                Console.WriteLine();

                if (request.Quantity < 1)
                {
                    Console.WriteLine("The requested quantity must be at least 1.");
                }
                else if (!selectedProduct.IsAvailable(request.Quantity))
                {
                    Console.WriteLine("The requested quantity is not available in stock.");
                }
                else
                {
                    Order order =
                        new Order(selectedProduct, request.Quantity, customer);

                    selectedProduct.ReduceStock(request.Quantity);

                    Console.WriteLine("Customer Information:");
                    Console.WriteLine(customer);

                    Console.WriteLine();
                    Console.WriteLine("Order Information:");
                    Console.WriteLine(order);

                    Console.WriteLine();
                    Console.WriteLine($"Remaining Stock: {selectedProduct.StockQuantity}");

                    Console.WriteLine();
                    Console.WriteLine("==============================================================");
                    Console.WriteLine();

                    Console.WriteLine("Notifications:");

                    Notify(customer);
                    Notify(order);
                }
            }
        }
    }
}
