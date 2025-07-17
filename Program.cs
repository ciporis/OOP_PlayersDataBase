using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBase_5_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string AddPlayerAction = "Add player";
            const string ShowAllPlayersAction = "Show all players";
            const string RemovePlayerByIdAction = "Remove player by id";
            const string EditPlayerAction = "Edit player";
            const string BanPlayerByIdAction = "Ban player by id";
            const string UnbanPlayerByIdAction = "Unban player by id";
            const string ExitAction = "Exit";

            PlayersDataBase playersDB = new PlayersDataBase();

            string[] actions = 
            {  
                AddPlayerAction, 
                ShowAllPlayersAction, 
                RemovePlayerByIdAction,
                EditPlayerAction,
                BanPlayerByIdAction, 
                UnbanPlayerByIdAction,
                ExitAction
            };

            bool isWorking = true;

            while (isWorking)
            {
                Console.WriteLine("Выберите действие");

                for (int i = 0; i < actions.Length; i++)
                {
                    Console.WriteLine($"{i}) {actions[i]}");
                }

                int actionIndex = int.Parse(Console.ReadLine());

                switch (actions[actionIndex])
                {
                    case AddPlayerAction:
                        AddPlayer(playersDB);                        
                        break;
                    case ShowAllPlayersAction:
                        ShowAllPlayers(playersDB);
                        break;
                    case RemovePlayerByIdAction:
                        RemovePlayer(playersDB);
                        break;
                    case EditPlayerAction:
                        EditPlayer(playersDB);
                        break;
                    case BanPlayerByIdAction:
                        BanPlayer(playersDB);
                        break;
                    case UnbanPlayerByIdAction:
                        UnbanPlayer(playersDB);
                        break;
                    case ExitAction:
                        isWorking = false;
                        break;
                }
            }
        }

        private static void UnbanPlayer(PlayersDataBase playersDB)
        {
            Console.WriteLine("Input player's id");
            int id = int.Parse(Console.ReadLine());
            playersDB.UnbanPlayerById(id);
        }

        private static void BanPlayer(PlayersDataBase playersDB)
        {
            Console.WriteLine("Input player's id");
            int id = int.Parse(Console.ReadLine());
            playersDB.BanPlayerById(id);
        }

        private static void EditPlayer(PlayersDataBase playersDB)
        {
            Console.WriteLine("Input player's id");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Input new player's nickname");
            string nickname = Console.ReadLine();

            while (playersDB.NickNameExists(nickname) == true)
            {
                Console.WriteLine("This nickname already exists! Input new one");
                nickname = Console.ReadLine();
            }

            Console.WriteLine("Input new player's level");
            int level = int.Parse(Console.ReadLine());
            Console.WriteLine("Is player banned? Press y/n");
            ConsoleKeyInfo pressedKeyInfo = Console.ReadKey();
            bool isPlayerBanned = false;

            switch (pressedKeyInfo.Key)
            {
                case ConsoleKey.Y:
                    isPlayerBanned = true;
                    break;
                case ConsoleKey.N:
                    isPlayerBanned = false;
                    break;
            }

            playersDB.EditPlayer(id, nickname, level, isPlayerBanned);
        }

        private static void RemovePlayer(PlayersDataBase playersDB)
        {
            Console.WriteLine("Input player's id");
            int id = int.Parse(Console.ReadLine());
            playersDB.RemovePlayerById(id);
        }

        private static void ShowAllPlayers(PlayersDataBase playersDB)
        {
            List<Player> players = playersDB.GetAllPlayers();

            if (players.Count == 0)
            {
                Console.WriteLine("DB is empty!");
                return;
            }

            foreach (Player player in players)
            {
                Console.WriteLine($"{player.GetId()}: {player.GetNickname()} " +
                    $"| {player.GetLevel()}lvl | is banned: {player.IsBanned()}");
            }
        }

        public static void AddPlayer(PlayersDataBase playersDB)
        {
            Console.WriteLine("Input player's nickname");

            string nickname = Console.ReadLine();

            while(playersDB.NickNameExists(nickname) == true)
            {
                Console.WriteLine("This nickname already exists! Input new one");
                nickname = Console.ReadLine();
            }

            Console.WriteLine("Input player's level");
            int level = int.Parse(Console.ReadLine());
            Console.WriteLine("Is player banned? Press y/n");
            ConsoleKeyInfo pressedKeyInfo = Console.ReadKey();
            bool isPlayerBanned = false;

            switch (pressedKeyInfo.Key)
            {
                case ConsoleKey.Y:
                    isPlayerBanned = true;
                    break;
                case ConsoleKey.N:
                    isPlayerBanned = false;
                    break;
            }

            playersDB.AddPlayer(nickname, level, isPlayerBanned);
        }
    }

    internal class PlayersDataBase
    {
        private List<Player> _table = new List<Player>();

        public bool NickNameExists(string nickname)
        {
            return _table.Any(player => player.GetNickname() == nickname);
        }

        public void AddPlayer(string nickName, int level, bool isBanned)
        {
            int id;

            if (_table.Count == 0)
                id = 0;
            else
                id = _table.Count;

            var player = new Player(id, nickName, level, isBanned);

            _table.Add(player);
        }

        public void RemovePlayerById(int id)
        {
            _table.RemoveAt(id);
        }

        public Player GetPlayerById(int id)
        {
            return _table[id];
        }

        public List<Player> GetAllPlayers()
        {
            return _table;
        }

        public void EditPlayer(int id, string nickName, int level, bool banFlag)
        {
            Player player = GetPlayerById(id);

            player.EditNickname(nickName);
            player.EditLevel(level);
            player.EditBanState(banFlag);

            _table[id] = player;
        }

        public void BanPlayerById(int id)
        {
            Player player = GetPlayerById(id);

            player.EditBanState(true);

            _table[id] = player;
        }

        public void UnbanPlayerById(int id)
        {
            Player player = GetPlayerById(id);

            player.EditBanState(false);

            _table[id] = player;
        }
    }

    public class Player
    {
        private int _id;
        private string _nickname;
        private int _level;
        private bool _isBanned;

        public Player(int id, string nickname, int level, bool isBanned)
        {
            _id = id;
            _nickname = nickname;
            _level = level;
            _isBanned = isBanned;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetNickname()
        {
            return _nickname;
        }

        public void EditNickname(string nickname)
        {
            _nickname = nickname;
        }

        public int GetLevel()
        {
            return _level;
        }

        public void EditLevel(int level)
        {
            _level = level;
        }

        public bool IsBanned()
        {
            return _isBanned;
        }

        public void EditBanState(bool isBanned)
        {
            _isBanned = isBanned;
        }
    }
}