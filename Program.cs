using System;
using System.Collections.Generic;

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
                        Console.WriteLine("Input player's nickname");
                        string nickname = Console.ReadLine();
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
                        break;
                    case ShowAllPlayersAction:
                        List<Player> players = playersDB.GetAllPlayers();

                        if (players.Count == 0)
                        {
                            Console.WriteLine("DB is empty!");
                            break;
                        }

                        foreach (Player player in players)
                        {
                            Console.WriteLine($"{player.Id}: {player.Nickname} | {player.Level}lvl | is banned: {player.IsBanned}");
                        }
                        break;
                    case RemovePlayerByIdAction:
                        Console.WriteLine("Input player's id");
                        int id = int.Parse(Console.ReadLine());
                        playersDB.RemovePlayerById(id);
                        break;
                    case EditPlayerAction:
                        Console.WriteLine("Input player's id");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Input new player's nickname");
                        nickname = Console.ReadLine();
                        Console.WriteLine("Input new player's level");
                        level = int.Parse(Console.ReadLine());
                        Console.WriteLine("Is player banned? Press y/n");
                        pressedKeyInfo = Console.ReadKey();
                        isPlayerBanned = false;

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
                        break;
                    case BanPlayerByIdAction:
                        Console.WriteLine("Input player's id");
                        id = int.Parse(Console.ReadLine());
                        playersDB.BanPlayerById(id);
                        break;
                    case UnbanPlayerByIdAction:
                        Console.WriteLine("Input player's id");
                        id = int.Parse(Console.ReadLine());
                        playersDB.UnbanPlayerById(id);
                        break;
                    case ExitAction:
                        isWorking = false;
                        break;
                }
            }
        }
    }

    internal class PlayersDataBase
    {
        private List<Player> _table = new List<Player>();

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

            player.Nickname = nickName;
            player.Level = level;
            player.IsBanned = banFlag;

            _table[id] = player;
        }

        public void BanPlayerById(int id)
        {
            Player player = GetPlayerById(id);

            player.IsBanned = true;

            _table[id] = player;
        }

        public void UnbanPlayerById(int id)
        {
            Player player = GetPlayerById(id);

            player.IsBanned = false;

            _table[id] = player;
        }
    }

    public class Player
    {
        public int Id;
        public string Nickname;
        public int Level;
        public bool IsBanned;

        public Player(int id, string nickname, int level, bool isBanned)
        {
            Id = id;
            Nickname = nickname;
            Level = level;
            IsBanned = isBanned;
        }
    }
}