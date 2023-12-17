using System;
using System.Runtime.Serialization;

namespace CardGame_Cons
{
    [DataContract]
    /// <summary> Игровая логика </summary>
    class Controller
    {
        [DataMember]
        GameManager game; // Экземпляр игрового менеджера

        EnemyAI enemyAI; // Экземпляр противника

        [DataMember]
        public bool WaitFlag { get; set; } // Флаг ожидания

        [DataMember]
        public bool BattleFlag { get; set; } // Флаг состояния битвы

        [DataMember]
        private string logs; // Логи игры

        public Controller()
        {
            game = new GameManager();
            enemyAI = new EnemyAI(game);
            Debug.DebugInit(game);
            logs = "";
        }

        public string GameOutPut() // Вывод информации об игре
        {
            return game.GameStatus() + game.BorderStatus() + game.PlayerHandStatus();
        }

        public void PlayCard()
        {
            try
            {
                Console.WriteLine("\nСписок карт в руке");

                if (game.PlayerCards.Count == 0)
                    throw new Exception("В руке нет карт");

                for (int i = 0; i < game.PlayerCards.Count; i++)
                    Console.WriteLine($"{i + 1}. {game.PlayerCards[i].CardStatus()}");

                Console.WriteLine("\nВыберите какую карту сыграть:");
                int j = Convert.ToInt32(Console.ReadLine());

                game.PutCard(j); // Сыграть выбранную карту
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey();
            }
        }

        public void Skip()
        {
            if (enemyAI.GameMove()) // Противник делает ход
            {
                if (WaitFlag)
                {
                    WaitFlag = false; // Сбросить флаг ожидания
                    return;
                }

                game.RoundBegin(); // Начало раунда
                Info();

                game.RoundStart(); // Начало хода игрока
                Info();

                game.RoundEnd(); // Завершение хода игрока
                Info();

                game.NewRound(); // Новый раунд
            }

            Info();
        }

        private void Info() // Вывод информации на консоль
        {
            Console.Clear();
            Console.WriteLine(GameOutPut());
            Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
        }

        public void Save() // Сохранение игры
        {
            logs = Debug.SavingLogs(); // Сохранить логи
            game.Save(); // Сохранить состояние игры
        }

        public void Load() // Загрузка игры
        {
            Debug.LoadLogs(logs); // Загрузить логи
            game.Load(); // Загрузить состояние игры
            enemyAI = new EnemyAI(game); // Создать новый экземпляр противника
        }
    }
}
