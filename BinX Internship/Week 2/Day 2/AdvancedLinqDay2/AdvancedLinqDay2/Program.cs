using AdvancedLinqDay2.Models;

namespace AdvancedLinqDay2
{
    internal class Program
    {
        static void Main(string[] args)
        {
          List<Customer> customers = new List<Customer>
            {
                new Customer(1, "Mohammad"),
                new Customer(2, "Ahmad"),
                new Customer(3, "Sara"),
                new Customer(4, "Lina"),
                new Customer(5, "Omar"),
                new Customer(6, "Noor")
            };

           List<Order> orders = new List<Order>
            {
                new Order(
                    101,
                    1,
                    1250m,
                    new List<OrderItem>
                    {
                        new OrderItem(1, "Laptop", 1, 1200m),
                        new OrderItem(2, "Mouse", 2, 25m)
                    }
                ),

                new Order(
                    102,
                    2,
                    420m,
                    new List<OrderItem>
                    {
                        new OrderItem(3, "Keyboard", 1, 70m),
                        new OrderItem(4, "Monitor", 1, 350m)
                    }
                ),

                new Order(
                    103,
                    1,
                    150m,
                    new List<OrderItem>
                    {
                        new OrderItem(5, "Webcam", 1, 60m),
                        new OrderItem(6, "Headphones", 1, 90m)
                    }
                ),

                new Order(
                    104,
                    3,
                    600m,
                    new List<OrderItem>
                    {
                        new OrderItem(7, "Desk", 1, 300m),
                        new OrderItem(8, "Chair", 2, 150m)
                    }
                ),

                new Order(
                    105,
                    4,
                    60m,
                    new List<OrderItem>
                    {
                        new OrderItem(9, "Book", 3, 20m)
                    }
                ),

                new Order(
                    106,
                    5,
                    250m,
                    new List<OrderItem>
                    {
                        new OrderItem(10, "Printer", 1, 220m),
                        new OrderItem(11, "Paper", 2, 15m)
                    }
                )
            };

            Console.WriteLine($"Customers: {customers.Count}");
            Console.WriteLine($"Orders: {orders.Count}");

            Console.WriteLine();
            Console.WriteLine("Order Totals by Customer:");

            var orderTotalsByCustomer = orders
                .GroupBy(order => order.CustomerId)
                .Select(group => new 
                { 
                    CustomerId = group.Key,
                    OrderCount = group.Count(),
                    TotalAmount = group.Sum(order => order.Amount)
                });

            foreach (var summary in orderTotalsByCustomer)
            {
                Console.WriteLine(
                    $"Customer ID: {summary.CustomerId} | " +
                    $"Orders: {summary.OrderCount} | " +
                    $"Total Amount: {summary.TotalAmount}"
                );
            }

            Console.WriteLine();
            Console.WriteLine("Customer Orders:");

            var customerOrders = customers.Join(
                orders,
                customer => customer.Id,
                order => order.CustomerId,
                (customer, order) => new
                {
                    CustomerName = customer.Name,
                    OrderId = order.Id,
                    OrderAmount = order.Amount
                }
            );

            foreach (var item in customerOrders)
            {
                Console.WriteLine(
                    $"Customer: {item.CustomerName} | " +
                    $"Order ID: {item.OrderId} | " +
                    $"Amount: {item.OrderAmount}"
                );
            }

            Console.WriteLine();
            Console.WriteLine("All Order Items:");

            var allOrderItems = orders
                .SelectMany(
                    order => order.Items,
                    (order, item) => new
                    {
                        OrderId = order.Id,
                        CustomerId = order.CustomerId,
                        ProductName = item.ProductName,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    }
                );

            foreach (var item in allOrderItems)
            {
                Console.WriteLine(
                    $"Order ID: {item.OrderId} | " +
                    $"Customer ID: {item.CustomerId} | " +
                    $"Product: {item.ProductName} | " +
                    $"Quantity: {item.Quantity} | " +
                    $"Unit Price: {item.UnitPrice}"
                );
            }

            Console.WriteLine();
            Console.WriteLine("Deferred Execution:");

            var highValueOrders = orders
                .Where(order => order.Amount >= 500m);

            // The query has been defined, but it has not been executed yet.
            orders.Add(
                new Order(
                    107,
                    6,
                    900m,
                    new List<OrderItem>
                    {
            new OrderItem(12, "Tablet", 1, 900m)
                    }
                )
            );

            Console.WriteLine("High-Value Orders:");

            foreach (Order order in highValueOrders)
            {
                Console.WriteLine(
                    $"Order ID: {order.Id} | " +
                    $"Customer ID: {order.CustomerId} | " +
                    $"Amount: {order.Amount}"
                );
            }
        }
    }
}
