using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("*Ця програма розвʼязує задачі про знаходження шляху в лабіринті*");

        int choice = GetValidatedChoice(
            "Ви можете генерувати випадковий лабіринт за заданими параметрами (1) або завантажити заздалегідь підготовлений лабіринт з файлу (2): ", 
            1, 2);

        int width = 0, height = 0, difficulty = 0;
        int[,] maze;

        if (choice == 1)
        {
            width = GetValidatedChoice("Введіть ширину лабіринту (від 2 до 20): ", 2, 20);
            height = GetValidatedChoice("Введіть висоту лабіринту (від 2 до 20): ", 2, 20);
            difficulty = GetValidatedChoice("Введіть складність лабіринту (від 1 до 10): ", 1, 10);
            
            maze = MazeGenerator.Generate(width, height, difficulty);
        }
        else
        {
            while (true)
            {
                Console.Write("Введіть шлях до файлу з лабіринтом: ");
                string path = Console.ReadLine();

                try
                {
                    maze = MazeLoader.LoadFromFile(path);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка при завантаженні: {ex.Message}");
                }
            }
        }

        int algorithm = GetValidatedChoice(
            "Оберіть алгоритм пошуку шляху:\n1) Метод Дейкстри\n2) Метод A* (Манхеттенська евристика)\n3) Метод A* (Евклідова евристика): ", 
            1, 3);

        int mode = GetValidatedChoice("Якщо хочете, щоб алгоритм запустився автоматично, натисніть 1, якщо покроково - 2: ", 1, 2);

        int startOption = GetValidatedChoice("Якщо бажаєте задати точку старту і кінця, натисніть 1, для випадкових точок - 2: ", 1, 2);

        (int startX, int startY, int endX, int endY) = startOption == 1 
            ? AskUserForStartEnd(maze)
            : MazeHelper.GetRandomStartEnd(maze);

        MazeSolver.Solve(maze, algorithm, mode == 1, startX, startY, endX, endY);
    }

    static int GetValidatedChoice(string prompt, int min, int max)
    {
        int result;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine().Trim();

            if (int.TryParse(input, out result) && result >= min && result <= max)
                return result;

            Console.WriteLine("Помилка! Введіть коректне значення.");
        }
    }

    static (int, int, int, int) AskUserForStartEnd(int[,] maze)
    {
        Console.WriteLine("Введіть координати стартової точки:");
        int sx = GetValidatedChoice("X: ", 0, maze.GetLength(1) - 1);
        int sy = GetValidatedChoice("Y: ", 0, maze.GetLength(0) - 1);

        Console.WriteLine("Введіть координати кінцевої точки:");
        int ex = GetValidatedChoice("X: ", 0, maze.GetLength(1) - 1);
        int ey = GetValidatedChoice("Y: ", 0, maze.GetLength(0) - 1);

        return (sx, sy, ex, ey);
    }
}
