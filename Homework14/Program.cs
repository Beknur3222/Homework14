using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework14
{
    class Karta
    {
        public string Mast { get; set; }
        public string Tip { get; set; }

        public Karta(string mast, string tip)
        {
            Mast = mast;
            Tip = tip;
        }

        public override string ToString()
        {
            return $"{Tip} {Mast}";
        }
    }

    class Player
    {
        public List<Karta> Cards { get; set; } = new List<Karta>();

        public void ShowCards()
        {
            foreach (var card in Cards)
            {
                Console.WriteLine(card);
            }
        }
    }

    class Game
    {
        private List<Player> players = new List<Player>();
        private List<Karta> deck = new List<Karta>();

        public Game(int playerCount)
        {
            if (playerCount < 2)
            {
                throw new ArgumentException("Количество игроков должно быть не менее 2.");
            }

            InitializeDeck();
            ShuffleDeck();
            DealCards(playerCount);
        }

        private void InitializeDeck()
        {
            string[] masts = { "Черви", "Бубны", "Крести", "Пики" };
            string[] tips = { "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };

            foreach (var mast in masts)
            {
                foreach (var tip in tips)
                {
                    deck.Add(new Karta(mast, tip));
                }
            }
        }

        private void ShuffleDeck()
        {
            var random = new Random();
            deck = deck.OrderBy(card => random.Next()).ToList();
        }

        private void DealCards(int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
            {
                var player = new Player();
                player.Cards.AddRange(deck.Skip(i * (deck.Count / playerCount)).Take(deck.Count / playerCount));
                players.Add(player);
            }
        }

        public void StartGame()
        {
            while (players.Count > 1)
            {
                var cardsOnTable = new List<Karta>();

                foreach (var player in players)
                {
                    var card = player.Cards[0];
                    player.Cards.RemoveAt(0);

                    Console.WriteLine($"Игрок {players.IndexOf(player) + 1} кладет карту: {card}");
                    cardsOnTable.Add(card);
                }

                var maxCard = cardsOnTable.Max(c => GetCardValue(c));
                var winningPlayer = players[cardsOnTable.FindIndex(c => GetCardValue(c) == maxCard)];

                Console.WriteLine($"Выигрывает игрок {players.IndexOf(winningPlayer) + 1}");

                winningPlayer.Cards.AddRange(cardsOnTable);
                players.RemoveAll(player => player.Cards.Count == 0);
            }

            Console.WriteLine($"Игрок {players.IndexOf(players[0]) + 1} выигрывает игру!");
        }

        private int GetCardValue(Karta card)
        {
            string[] tips = { "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };
            return Array.IndexOf(tips, card.Tip);
        }
    }

    class Program
    {
        static void Main()
        {
            Console.Write("Введите количество игроков (минимум 2): ");
            int playerCount = int.Parse(Console.ReadLine());

            try
            {
                Game game = new Game(playerCount);
                game.StartGame();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.ReadKey();
        }
    }

}
