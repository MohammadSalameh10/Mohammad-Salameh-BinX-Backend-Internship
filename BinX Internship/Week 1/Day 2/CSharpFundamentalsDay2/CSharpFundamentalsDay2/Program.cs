namespace CSharpFundamentalsDay2
{
    internal class Program
    {
        static void DemonstrateCopyBehavior()
        {
            Console.WriteLine("Copy Behavior:");

            int originalNumber = 10;
            int copiedNumber = originalNumber;

            Console.WriteLine("Value Type before change:");
            Console.WriteLine($"Original number: {originalNumber}");
            Console.WriteLine($"Copied number: {copiedNumber}");

            copiedNumber = 20;

            Console.WriteLine("Value Type after change:");
            Console.WriteLine($"Original number: {originalNumber}");
            Console.WriteLine($"Copied number: {copiedNumber}");

            Console.WriteLine();

            int[] originalNumbers = { 10, 20, 30 };
            int[] copiedNumbers = originalNumbers;

            Console.WriteLine("Reference Type before change:");
            Console.WriteLine($"Original first value: {originalNumbers[0]}");
            Console.WriteLine($"Copied first value: {copiedNumbers[0]}");

            copiedNumbers[0] = 100;

            Console.WriteLine("Reference Type after change:");
            Console.WriteLine($"Original first value: {originalNumbers[0]}");
            Console.WriteLine($"Copied first value: {copiedNumbers[0]}");
        }

        static string DescribeGrade(int score)
        {
            return score switch
            {
                >= 90 => "Excellent",
                >= 70 => "Proficient",
                >= 50 => "Developing",
                _ => "Below Standard"
            };
        }

        static void HandleNullableInput()
        {
            Console.WriteLine("Nullable Input Handling:");

            Console.Write("Enter your name: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("No name was entered.");
            }
            else
            {
                Console.WriteLine($"Hello, {name}");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Value Types:");

            int age = 24;
            double salary = 1500.50;
            bool isActive = true;

            Console.WriteLine($"age: {age} - Type: {age.GetType()}");
            Console.WriteLine($"salary: {salary} - Type: {salary.GetType()}");
            Console.WriteLine($"isActive: {isActive} - Type: {isActive.GetType()}");

            Console.WriteLine();
            Console.WriteLine("Reference Types:");

            string name = "Mohammad";
            int[] numbers = { 10, 20, 30 };
            List<string> skills = new List<string>
            {
                "C#",
                ".NET",
                "Git"
            };

            Console.WriteLine($"name: {name} - Type: {name.GetType()}");
            Console.WriteLine($"numbers Type: {numbers.GetType()}");
            Console.WriteLine($"skills Type: {skills.GetType()}");

            Console.WriteLine();
            Console.WriteLine("==============================================================");

            Console.WriteLine();
            DemonstrateCopyBehavior();

            Console.WriteLine();
            Console.WriteLine("==============================================================");

            Console.WriteLine();
            Console.WriteLine("Grade Classifier:");

            int score = 90;
            string gradeDescription = DescribeGrade(score);

            Console.WriteLine($"Score: {score}");
            Console.WriteLine($"Grade: {gradeDescription}");

            Console.WriteLine();
            Console.WriteLine("==============================================================");

            Console.WriteLine();
            HandleNullableInput();
        }
    }
}
