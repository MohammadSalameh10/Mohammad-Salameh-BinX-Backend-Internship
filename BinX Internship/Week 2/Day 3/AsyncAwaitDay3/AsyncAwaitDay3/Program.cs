using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitDay3
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            long sequentialTime = await RunSequentialAsync();

            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine();

            long concurrentTime = await RunConcurrentAsync();

            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.WriteLine("Time Comparison:");
            Console.WriteLine($"Sequential Time: {sequentialTime} ms");
            Console.WriteLine($"Concurrent Time: {concurrentTime} ms");
            Console.WriteLine($"Time Saved: {sequentialTime - concurrentTime} ms");

            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine();

            await RunCancellationDemoAsync();
        }

        static async Task<long> RunSequentialAsync()
        {
            Console.WriteLine("Sequential execution started.");
            Console.WriteLine();

            Stopwatch stopwatch = Stopwatch.StartNew();

            string customerData = await GetCustomerDataAsync();

            string orderData = await GetOrderDataAsync();

            string notificationData = await GetNotificationDataAsync();

            stopwatch.Stop();

            Console.WriteLine();
            Console.WriteLine("Sequential Results:");
            Console.WriteLine(customerData);
            Console.WriteLine(orderData);
            Console.WriteLine(notificationData);

            Console.WriteLine();
            Console.WriteLine($"Sequential elapsed time: {stopwatch.ElapsedMilliseconds} ms");

            return stopwatch.ElapsedMilliseconds;
        }

        static async Task<long> RunConcurrentAsync()
        {
            Console.WriteLine("Concurrent execution started.");
            Console.WriteLine();

            Stopwatch stopwatch = Stopwatch.StartNew();

            Task<string> customerTask = GetCustomerDataAsync();

            Task<string> orderTask = GetOrderDataAsync();

            Task<string> notificationTask = GetNotificationDataAsync();

            string[] results = await Task.WhenAll(customerTask, orderTask, notificationTask);

            stopwatch.Stop();

            Console.WriteLine();
            Console.WriteLine("Concurrent Results:");
            Console.WriteLine(results[0]);
            Console.WriteLine(results[1]);
            Console.WriteLine(results[2]);

            Console.WriteLine();
            Console.WriteLine($"Concurrent elapsed time: {stopwatch.ElapsedMilliseconds} ms");

            return stopwatch.ElapsedMilliseconds;
        }

        static async Task RunCancellationDemoAsync()
        {
            Console.WriteLine("Cancellation demo started.");
            Console.WriteLine();

            using CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            Task<string> orderTask = GetOrderDataAsync(cancellationTokenSource.Token);

            await Task.Delay(1500);

            Console.WriteLine("Cancellation requested.");

            cancellationTokenSource.Cancel();

            try
            {
                string orderData = await orderTask;

                Console.WriteLine(orderData);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Order loading was canceled.");
            }
        }

        static async Task<string> GetCustomerDataAsync()
        {
            Console.WriteLine("Loading customer data...");

            await Task.Delay(2000);

            Console.WriteLine("Customer data loaded.");

            return "Customer: Mohammad";
        }

        static async Task<string> GetOrderDataAsync(CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Loading order data...");

            await Task.Delay(3000, cancellationToken);

            Console.WriteLine("Order data loaded.");

            return "Order: 101";
        }

        static async Task<string> GetNotificationDataAsync()
        {
            Console.WriteLine("Loading notification data...");

            await Task.Delay(1000);

            Console.WriteLine("Notification data loaded.");

            return "Notification: Order confirmed";
        }
    }
}