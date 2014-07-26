using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RockPaperScissorsLizardSpock
{
    public class Game
    {
        public enum Throws
        {
            Scissors,
            Paper,
            Rock,
            Lizard,
            Spock
        }

        public static Random Random = new Random();

        public IReadOnlyDictionary<KeyValuePair<Throws, Throws>, GameResult>
            Rules = new ReadOnlyDictionary<KeyValuePair<Throws, Throws>, GameResult>(
                new Dictionary<KeyValuePair<Throws, Throws>, GameResult>
                {
                    // Scissors
                    { new KeyValuePair<Throws, Throws>(Throws.Scissors, Throws.Scissors), new GameResult { Tied = true } },
                    { new KeyValuePair<Throws, Throws>(Throws.Scissors, Throws.Paper), new GameResult { Won = true, Description = "Scissors cut paper." } },
                    { new KeyValuePair<Throws, Throws>(Throws.Scissors, Throws.Rock), new GameResult { Description = "Scissors are crushed by rock."} },
                    { new KeyValuePair<Throws, Throws>(Throws.Scissors, Throws.Lizard), new GameResult { Won = true, Description = "Scissors decapte lizard."} },
                    { new KeyValuePair<Throws, Throws>(Throws.Scissors, Throws.Spock), new GameResult { Description = "Scissors are smashed by Spock."} },
                    // Paper
                    { new KeyValuePair<Throws, Throws>(Throws.Paper, Throws.Scissors), new GameResult { Description = "Paper is cut by scissors."} },
                    { new KeyValuePair<Throws, Throws>(Throws.Paper, Throws.Paper), new GameResult { Tied = true } },
                    { new KeyValuePair<Throws, Throws>(Throws.Paper, Throws.Rock), new GameResult { Won = true, Description = "Paper covers rock."} },
                    { new KeyValuePair<Throws, Throws>(Throws.Paper, Throws.Lizard), new GameResult { Description = "Paper is eaten by lizard."} },
                    { new KeyValuePair<Throws, Throws>(Throws.Paper, Throws.Spock), new GameResult { Won = true, Description = "Paper disproves Spock."} },
                    // Rock
                    { new KeyValuePair<Throws, Throws>(Throws.Rock, Throws.Scissors), new GameResult { Won = true, Description = "Rock crushes scissors."} },
                    { new KeyValuePair<Throws, Throws>(Throws.Rock, Throws.Paper), new GameResult { Description = "Rock is covered by paper."} },
                    { new KeyValuePair<Throws, Throws>(Throws.Rock, Throws.Rock), new GameResult { Tied = true } },
                    { new KeyValuePair<Throws, Throws>(Throws.Rock, Throws.Lizard), new GameResult { Won = true, Description = "Rock crushes lizard."} },
                    { new KeyValuePair<Throws, Throws>(Throws.Rock, Throws.Spock), new GameResult { Description = "Rock is evaporated by Spock."} },
                    // Lizard
                    { new KeyValuePair<Throws, Throws>(Throws.Lizard, Throws.Scissors), new GameResult { Description = "Lizard is decapetated by scissors."} },
                    { new KeyValuePair<Throws, Throws>(Throws.Lizard, Throws.Paper), new GameResult { Won = true, Description = "Lizard eats paper."} },
                    { new KeyValuePair<Throws, Throws>(Throws.Lizard, Throws.Rock), new GameResult { Description = "Lizard is crushed by paper."} },
                    { new KeyValuePair<Throws, Throws>(Throws.Lizard, Throws.Lizard), new GameResult { Tied = true } },
                    { new KeyValuePair<Throws, Throws>(Throws.Lizard, Throws.Spock), new GameResult { Won = true, Description = "Lizard poisons Spock."} },
                    // Spock
                    { new KeyValuePair<Throws, Throws>(Throws.Spock, Throws.Scissors), new GameResult { Won = true, Description = "Spock crushes scissors."} },
                    { new KeyValuePair<Throws, Throws>(Throws.Spock, Throws.Paper), new GameResult { Description = "Spock is disproved by paper."} },
                    { new KeyValuePair<Throws, Throws>(Throws.Spock, Throws.Rock), new GameResult { Won = true, Description = "Spock evaporates rock."} },
                    { new KeyValuePair<Throws, Throws>(Throws.Spock, Throws.Lizard), new GameResult { Description = "Spock is poisoned by lizard."} },
                    { new KeyValuePair<Throws, Throws>(Throws.Spock, Throws.Spock), new GameResult { Tied = true} },
                });

        public GameResult Play(Throws player1, Throws player2)
        {
            var result =
            Rules
                .Where(x => x.Key.Key == player1 && x.Key.Value == player2)
                .Select(x => new GameResult(x.Value) { Player1 = x.Key.Key, Player2 = x.Key.Value })
                .FirstOrDefault();

            return result ?? new GameResult { Description = "Oops... play again." };
        }

        public GameResult Play(Throws you)
        {
            var computer = GetRandomThrow();
            return Play(you, computer);
        }

        public GameResult Play()
        {
            return Play(GetRandomThrow(), GetRandomThrow());
        }

        private Game.Throws GetRandomThrow()
        {
            return (Throws)Random.Next(0, Enum.GetValues(typeof(Throws)).Length);
        }
    }

    public class GameResult
    {
        public GameResult()
        {
            Description = "Play again!";
        }

        public GameResult(GameResult value)
            :this()
        {
            Won = value.Won;
            Tied = value.Tied;
            Description = value.Description;
        }

        public Game.Throws Player1 { get; set; }
        public Game.Throws Player2 { get; set; }

        public bool Won { get; set; }
        public bool Tied { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Tied 
                ? Description 
                : string.Format("Player 1: {2}\nPlayer 2: {3}\nResult: {0}, {1}", Won ? "You won :)" : "You lost :(", Description, Player1, Player2);
        }
    }
}
