/* A console application that generates 5 lucky numbers and 1 bonus number, stores them, and displays them.
 */
using System.Collections.Generic;
using System.IO;

class LuckyNumberGenerator
{
    // Enum for menu options
    enum MenuOption
    {
        Generate = 1,
        ViewResults = 2,
        Exit = 3
    }

    private static string filePath = "LuckyNumbers.txt"; // File to store lucky numbers

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n--- Lucky Number Generator ---");
            Console.WriteLine("1. Generate Lucky Numbers");
            Console.WriteLine("2. View Past Results");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");

            string input = Console.ReadLine();

            if (Enum.TryParse(input, out MenuOption choice) && Enum.IsDefined(typeof(MenuOption), choice))
            {
                switch (choice)
                {
                    case MenuOption.Generate:
                        GenerateAndSaveNumbers();
                        break;
                    case MenuOption.ViewResults:
                        ViewPastResults();
                        break;
                    case MenuOption.Exit:
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice! Please enter 1, 2, or 3.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a valid number.");
            }
        }
    }
    static void GenerateAndSaveNumbers()
    {
        try
        {
            Random random = new();
            List<int> luckyNumbers = new List<int>();
            Console.WriteLine("\nGenerating your Lucky 5 numbers...");

            while (luckyNumbers.Count < 5)
            {
                int number = random.Next(1, 50);
                if (!luckyNumbers.Contains(number))
                {
                    luckyNumbers.Add(number);
                }
            }
            luckyNumbers.Sort();

            // Generate a bonus number
            int bonusNumber;
            do
            {
                bonusNumber = random.Next(1, 10);
            } while (luckyNumbers.Contains((int)bonusNumber));

            // Display the lucky numbers and bonus number
            Console.WriteLine("Your Lucky Numbers: " + string.Join(", ", luckyNumbers));
            Console.WriteLine("Bonus Number: " + bonusNumber);

            // Save to file
            SaveToFile(luckyNumbers, bonusNumber);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while generating numbers: {ex.Message}");
        }
    }

    static void SaveToFile(List<int> numbers, int bonus)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                //writer.WriteLine($"Lucky Numbers: {string.Join(", ", numbers)} | Bonus: {bonus}");
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                writer.WriteLine($"{dateTime} | Lucky Numbers: {string.Join(", ", numbers)} | Bonus: {bonus}");
            }
            Console.WriteLine($"Your lucky numbers have been saved to {filePath}.\n");
            }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving to file: {ex.Message}");
        }
    }
    static void ViewPastResults()
    {
        try
        {
                if (File.Exists(filePath))
            {
                Console.WriteLine("\n--- Past Lucky Numbers ---");
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine("No past results found!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
        }
    }

}
