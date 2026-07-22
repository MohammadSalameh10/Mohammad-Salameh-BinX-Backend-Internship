using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionsLinqDay4
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            Console.WriteLine("List:");

            List<Product> products = new List<Product>
            {
                new Product(1, "Laptop", 1200, true),
                new Product(2, "Mouse", 25, true),
                new Product(3, "Keyboard", 70, false),
                new Product(4, "Monitor", 350, true),
                new Product(5, "Headphones", 90, false),
                new Product(6, "Webcam", 60, true),
                new Product(7, "Printer", 220, false),
                new Product(8, "Tablet", 500, true)
            };

            Console.WriteLine("All Products:");

            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }

            Console.WriteLine();
            Console.WriteLine("==============================================================");
            Console.WriteLine("Dictionary:");
            Console.WriteLine();

            Dictionary<int, Product> productsById =
                products.ToDictionary(product => product.Id);

            if (productsById.TryGetValue(4, out Product? foundProduct))
            {
                Console.WriteLine($"Product found: {foundProduct}");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }

            Console.WriteLine();
            Console.WriteLine("==============================================================");
            Console.WriteLine("HashSet:");
            Console.WriteLine();

            HashSet<int> processedOrderIds = new HashSet<int>
            {
                101,
                102,
                103
            };

            bool firstAdd = processedOrderIds.Add(104);
            bool secondAdd = processedOrderIds.Add(104);

            Console.WriteLine($"First add of order 104: {firstAdd}");
            Console.WriteLine($"Second add of order 104: {secondAdd}");

            Console.WriteLine();
            Console.WriteLine("Processed Order IDs:");

            foreach (int orderId in processedOrderIds)
            {
                Console.WriteLine(orderId);
            }

            Console.WriteLine();
            Console.WriteLine("==============================================================");
            Console.WriteLine("LINQ Filtering:");
            Console.WriteLine();

            List<Product> availableProducts = products
                .Where(product =>
                    product.IsAvailable &&
                    product.Price > 100
                )
                .ToList();

            Console.WriteLine("Available Products Over 100:");

            foreach (Product product in availableProducts)
            {
                Console.WriteLine(product);
            }

            Console.WriteLine();
            Console.WriteLine("==============================================================");
            Console.WriteLine("LINQ Projection:");
            Console.WriteLine();

            List<string> productNames = products
                .Select(product => product.Name)
                .ToList();

            Console.WriteLine("Product Names:");

            foreach (string productName in productNames)
            {
                Console.WriteLine(productName);
            }

            Console.WriteLine();
            Console.WriteLine("==============================================================");
            Console.WriteLine("LINQ Ordering:");
            Console.WriteLine();

            List<Product> productsOrderedByPrice = products
                .OrderBy(product => product.Price)
                .ToList();

            Console.WriteLine("Products Ordered by Price:");

            foreach (Product product in productsOrderedByPrice)
            {
                Console.WriteLine(product);
            }

            Console.WriteLine();
            Console.WriteLine("==============================================================");
            Console.WriteLine("LINQ Aggregation:");
            Console.WriteLine();

            int productCount = products.Count();

            int availableProductCount = products
                .Count(product => product.IsAvailable);

            double totalPrice = products
                .Sum(product => product.Price);

            double averagePrice = products
                .Average(product => product.Price);

            Console.WriteLine($"Total Products: {productCount}");
            Console.WriteLine(
                $"Available Products Count: {availableProductCount}"
            );
            Console.WriteLine($"Total Product Prices: {totalPrice:F2}");
            Console.WriteLine($"Average Product Price: {averagePrice:F2}");

            Console.WriteLine();
            Console.WriteLine("==============================================================");
            Console.WriteLine("LINQ Query Syntax:");
            Console.WriteLine();

            List<string> availableProductNamesQuery =
                (
                    from product in products
                    where product.IsAvailable
                    orderby product.Name
                    select product.Name
                )
                .ToList();

            Console.WriteLine("Available Product Names:");

            foreach (string productName in availableProductNamesQuery)
            {
                Console.WriteLine(productName);
            }

            Console.WriteLine();
            Console.WriteLine("==============================================================");
            Console.WriteLine("Async and Await:");
            Console.WriteLine();

            string loadResult = await LoadProductsAsync();

            Console.WriteLine(loadResult);

            Console.WriteLine();
            Console.WriteLine("==============================================================");
            Console.WriteLine("Exception Handling:");
            Console.WriteLine();

            Console.Write("Enter product quantity: ");

            string? quantityInput = Console.ReadLine();

            try
            {
                int quantity = Convert.ToInt32(quantityInput);

                Console.WriteLine($"Quantity: {quantity}");
            }
            catch (FormatException)
            {
                Console.WriteLine(
                    "Invalid input. Please enter a whole number."
                );
            }
            catch (OverflowException)
            {
                Console.WriteLine(
                    "The number is too large or too small."
                );
            }
            finally
            {
                Console.WriteLine(
                    "Quantity input processing finished."
                );
            }
        }
        static async Task<string> LoadProductsAsync()
        {
            Console.WriteLine("Loading products...");

            await Task.Delay(2000);

            return "Products loaded successfully.";
        }
    }
}