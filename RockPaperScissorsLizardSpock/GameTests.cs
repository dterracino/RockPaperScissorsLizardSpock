using System;
using System.Diagnostics;
using FluentAssertions;
using Xunit;
using Xunit.Extensions;

namespace RockPaperScissorsLizardSpock
{
    public class GameTests
    {
        public GameTests()
        {
            Game = new Game();
        }

        public Game Game { get; set; }

        [Fact]
        public void All_similar_throws_result_in_a_tie()
        {
            foreach (Game.Throws playersThrow in Enum.GetValues(typeof(Game.Throws)))
            {
                Game.Play(playersThrow, playersThrow)
               .Tied
               .Should()
               .BeTrue();
            }
        }

        [Theory,
         InlineData(Game.Throws.Scissors, Game.Throws.Paper, true),
         InlineData(Game.Throws.Scissors, Game.Throws.Rock, false),
         InlineData(Game.Throws.Scissors, Game.Throws.Lizard, true),
         InlineData(Game.Throws.Scissors, Game.Throws.Spock, false),
         InlineData(Game.Throws.Lizard, Game.Throws.Paper, true),
         InlineData(Game.Throws.Lizard, Game.Throws.Rock, false),
         InlineData(Game.Throws.Lizard, Game.Throws.Scissors, false),
         InlineData(Game.Throws.Lizard, Game.Throws.Spock, true),
         InlineData(Game.Throws.Paper, Game.Throws.Lizard, false),
         InlineData(Game.Throws.Paper, Game.Throws.Rock, true),
         InlineData(Game.Throws.Paper, Game.Throws.Scissors, false),
         InlineData(Game.Throws.Paper, Game.Throws.Spock, true),
         InlineData(Game.Throws.Rock, Game.Throws.Paper, false),
         InlineData(Game.Throws.Rock, Game.Throws.Lizard, true),
         InlineData(Game.Throws.Rock, Game.Throws.Scissors, true),
         InlineData(Game.Throws.Rock, Game.Throws.Spock, false),
         InlineData(Game.Throws.Spock, Game.Throws.Paper, false),
         InlineData(Game.Throws.Spock, Game.Throws.Lizard, false),
         InlineData(Game.Throws.Spock, Game.Throws.Scissors, true),
         InlineData(Game.Throws.Spock, Game.Throws.Rock, true),
        ]
        public void Can_play_the_game(Game.Throws player1, Game.Throws player2, bool expectedResult)
        {
            var result = Game.Play(player1, player2);
            result.Won.Should().Be(expectedResult);
            Debug.WriteLine(result.ToString());
        }

        [Fact]
        public void Can_play_against_computer()
        {
            var result = Game.Play(Game.Throws.Paper);
            result.Should().NotBeNull();
            Debug.WriteLine(result.ToString());
        }

        [Fact]
        public void Can_let_computer_play_for_me_against_computer()
        {
            var result = Game.Play();
            result.Should().NotBeNull();
            Debug.WriteLine(result.ToString());
        }
    }
}
