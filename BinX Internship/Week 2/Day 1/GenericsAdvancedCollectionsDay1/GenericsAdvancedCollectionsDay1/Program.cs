namespace GenericsAdvancedCollectionsDay1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Repository<Product> productRepository = new Repository<Product>();

            productRepository.Add(new Product("Laptop",900));
            productRepository.Add(new Product("Phone",600));
            productRepository.Add(new Product("Keyboard",80));

            IReadOnlyList<Product> products = productRepository.GetAll();
            // IReadOnlyList<Product> does not allow adding or removing items.
            // Uncommenting this line causes a compile-time error.
            // products.Add(new Product("Mouse", 40));

            Console.WriteLine("All Products:");
            foreach (Product product in products) 
            {
                Console.WriteLine($"{product.Name} - {product.Price}");
            }

            Product? foundProduct = productRepository.Find(product => product.Name == "Phone");

            Console.WriteLine();

            if (foundProduct != null) 
            {
                Console.WriteLine($"{foundProduct.Name} - {foundProduct.Price}");
            }

            Repository<Customer> customerRepository = new Repository<Customer>();

            customerRepository.Add(new Customer("Mohammad","mohammad@gmail.com"));
            customerRepository.Add(new Customer("Ahmad","ahmad@gmail.com"));

            IReadOnlyList<Customer> customers = customerRepository.GetAll();

            Console.WriteLine();
            Console.WriteLine("All Customers:");

            foreach (Customer customer in customers)
            {
                Console.WriteLine($"{customer.Name} - {customer.Email}");
            }

            Customer? foundCustomer = customerRepository.Find(customer => customer.Email == "ahmad@gmail.com");

            Console.WriteLine();

            if (foundCustomer != null)
            {
                Console.WriteLine($"{foundCustomer.Name} - {foundCustomer.Email}");
            }
        }
    }
}
