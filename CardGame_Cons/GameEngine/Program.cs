using System;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace CardGame_Cons
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller game = new Controller();

            Console.WriteLine("Нажмите '1' - Начать новую игру");
            Console.WriteLine("Нажмите '2' - Загрузить незавершенную игру");

            ConsoleKey key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.D1: game = new Controller(); break; // Создание новой игры
                case ConsoleKey.D2:

                    if (File.Exists("GameSave.json"))
                    {
                        // Загрузка сохраненной игры
                        JsonSerializerSettings settings = new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.Auto
                        };
                        DataContractSerializer jsonF = new DataContractSerializer(typeof(Controller));
                        using (FileStream fileStream = new FileStream("GameSave.json", FileMode.Open))
                            game = (Controller)jsonF.ReadObject(fileStream);

                        game.Load();
                    }
                    else
                    {
                        // Если сохраненной игры нет, начинается новая игра
                        Console.WriteLine("Нет сохраненной игры");
                        Console.WriteLine("Будет запущена новая игра. Нажмите любую клавишу для продолжения или клавишу ESC для выхода");
                        if (Console.ReadKey().Key == ConsoleKey.Escape)
                            Environment.Exit(0);
                    }
                    break;

                default: break;
            }

            // Основной игровой цикл
            for (; ; )
            {
                Console.Clear();
                Console.WriteLine(game.GameOutPut());

                Console.WriteLine("1 - Сыграть карту из руки");
                Console.WriteLine("2 - Пропуск");
                Console.WriteLine("3 - Вывод сведений");
                Console.WriteLine("ESC - Прервать игру и выйти");

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1: game.PlayCard(); break; // Сыграть карту из руки
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2: game.Skip(); break; // Пропустить ход
                    case ConsoleKey.D3:
                        Console.Clear();
                        Console.WriteLine(Debug.GetLogs());
                        Console.ReadKey();
                        break; // Вывести логи игры
                    case ConsoleKey.Escape:
                        // Сохранение игры и выход из программы
                        DataContractSerializer jsonF = new DataContractSerializer(typeof(Controller));
                        game.Save();
                        using (FileStream fileStream = new FileStream("GameSave.json", FileMode.Create))
                            jsonF.WriteObject(fileStream, game);

                        Environment.Exit(0);
                        break;

                    default: break;
                }
            }
        }
    }
}
