using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBase_5_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string AddPlayerCommand = "1";
            const string ShowAllPlayersCommand = "2";
            const string RemovePlayerByIdCommand = "3";
            const string BanPlayerByIdCommand = "4";
            const string UnbanPlayerByIdCommand = "5";
            const string ExitCommand = "6";

            var playersDB = new DataBase();

            bool isWorking = true;

            while (isWorking)
            {
                Console.WriteLine("Выберите действие");

                Console.WriteLine($"{AddPlayerCommand}) Add player");
                Console.WriteLine($"{ShowAllPlayersCommand}) Show all players");
                Console.WriteLine($"{RemovePlayerByIdCommand}) Remove player by listIndex");
                Console.WriteLine($"{BanPlayerByIdCommand}) Ban player by listIndex");
                Console.WriteLine($"{UnbanPlayerByIdCommand}) Unban player by listIndex");
                Console.WriteLine($"{ExitCommand}) Exit");

                string command = Console.ReadLine();

                switch (command)
                {
                    case AddPlayerCommand:
                        AddPlayer(playersDB);                        
                        break;
                    case ShowAllPlayersCommand:
                        ShowAllPlayers(playersDB);
                        break;
                    case RemovePlayerByIdCommand:
                        RemovePlayer(playersDB);
                        break;
                    case BanPlayerByIdCommand:
                        BanPlayer(playersDB);
                        break;
                    case UnbanPlayerByIdCommand:
                        UnbanPlayer(playersDB);
                        break;
                    case ExitCommand:
                        isWorking = false;
                        break;
                }
            }
        }

        private static void UnbanPlayer(DataBase playersDB)
        {
            Console.WriteLine("Input player's listIndex");
            int id = int.Parse(Console.ReadLine());
            playersDB.UnbanPlayerById(id);
        }

        private static void BanPlayer(DataBase playersDB)
        {
            Console.WriteLine("Input player's listIndex");
            int id = int.Parse(Console.ReadLine());
            playersDB.BanPlayerById(id);
        }

        private static void RemovePlayer(DataBase playersDB)
        {
            Console.WriteLine("Input player's listIndex");
            int id = int.Parse(Console.ReadLine());
            playersDB.RemovePlayerById(id);
        }

        private static void ShowAllPlayers(DataBase playersDB)
        {
            List<Player> players = playersDB.GetAllPlayers();

            if (players.Count == 0)
            {
                Console.WriteLine("DB is empty!");
                return;
            }

            foreach (Player player in players)
            {
                string nickName = player.GetNickname();
                int level = player.GetLevel();
                bool isBanned = player.IsBanned();
                int id = playersDB.GetPlayerId(player);

                Console.WriteLine($"{id}: {nickName} " +
                    $"| {level}lvl | is banned: {isBanned}");
            }
        }

        public static void AddPlayer(DataBase playersDB)
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

            playersDB.AddPlayer(nickname, level);
        }
    }

    internal class DataBase
    {
        private List<Player> _playersList = new List<Player>();

        public bool NickNameExists(string nickname)
        {
            return _playersList.Any(player => player.GetNickname() == nickname);
        }

        public void AddPlayer(string nickName, int level)
        {
            bool isBanned = false;

            var player = new Player(nickName, level, isBanned);
            
            _playersList.Add(player);
        }

        public void RemovePlayerById(int id)
        {
            _playersList.RemoveAt(id);
        }

        public Player GetPlayerById(int id)
        {
            return _playersList[id];
        }

        public List<Player> GetAllPlayers()
        {
            return _playersList;
        }

        public void BanPlayerById(int id)
        {
            Player player = GetPlayerById(id);

            player.EditBanState(true);

            _playersList[id] = player;
        }

        public void UnbanPlayerById(int id)
        {
            Player player = GetPlayerById(id);

            player.EditBanState(false);

            _playersList[id] = player;
        }

        public int GetPlayerId(Player player)
        {
            return _playersList.IndexOf(player);
        }
    }

    public class Player
    {
        private string _nickname;
        private int _level;
        private bool _isBanned;

        public Player(string nickname, int level, bool isBanned)
        {
            _nickname = nickname;
            _level = level;
            _isBanned = isBanned;
        }

        public string GetNickname()
        {
            return _nickname;
        }

        public int GetLevel()
        {
            return _level;
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