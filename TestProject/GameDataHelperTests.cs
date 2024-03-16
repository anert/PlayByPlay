using PlayByPlayAnalysisService.Models;
using PlayByPlayAnalysisService.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PlayByPlayAnalysisService.Tests
{
    public class GameDataHelperTests
    {
        [Fact]
        public void GetAllPlayersNames_ShouldReturnCorrectPlayerNames()
        {
            // Arrange
            var actions = new List<GameAction>
                {
                    new GameAction("actionType", 1, 1, "James Harden", "HOU", "Made",1),
                    new GameAction("actionType", 2, 2,  "Russell Westbrook",  "HOU", null, 1),
                    new GameAction("actionType", 3, 3, "LeBron James",  "LAL", null, 1),
                    new GameAction("actionType", 4, 4, "Anthony Davis", "LAL", null, 1),
                    new GameAction("actionType", 5, 5, "Eric Gordon", "HOU",null, 1),
                    new GameAction("actionType", 6, 6, "Dwight Howard", "LAL", null, 1)
                };

            // Act
            var result = GameDataHelper.GetAllPlayersNames(actions);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(3, result["home"].Count);
            Assert.Equal(3, result["away"].Count);
            Assert.Contains("James Harden", result["home"]);
            Assert.Contains("Russell Westbrook", result["home"]);
            Assert.Contains("Eric Gordon", result["home"]);
            Assert.Contains("LeBron James", result["away"]);
            Assert.Contains("Anthony Davis", result["away"]);
            Assert.Contains("Dwight Howard", result["away"]);
        }

          
        [Fact]
        public void GetAllPlayersNames_ShouldReturnCorrectPlayerNamesAway()
        {
            // Arrange
            var actions = new List<GameAction>
                {
                    new GameAction("actionType", 1, 1, "James Harden", "HOU", "Made",0),
                    new GameAction("actionType", 2, 2,  "Russell Westbrook",  "HOU", null, 0),
                    new GameAction("actionType", 3, 3, "LeBron James",  "LAL", null, 0),
                    new GameAction("actionType", 4, 4, "Anthony Davis", "LAL", null, 0),
                    new GameAction("actionType", 5, 5, "Eric Gordon", "HOU",null, 0),
                    new GameAction("actionType", 6, 6, "Dwight Howard", "LAL", null, 0)
                };

            // Act
            var result = GameDataHelper.GetAllPlayersNames(actions);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(3, result["home"].Count);
            Assert.Equal(3, result["away"].Count);
            Assert.Contains("James Harden", result["away"]);
            Assert.Contains("Russell Westbrook", result["away"]);
            Assert.Contains("Eric Gordon", result["away"]);
            Assert.Contains("LeBron James", result["home"]);
            Assert.Contains("Anthony Davis", result["home"]);
            Assert.Contains("Dwight Howard", result["home"]);
        }

        [Fact]
        public void GetAllPlayersNames_ShouldReturnEmptyDictionary_WhenNoActions()
        {
            // Arrange
            var actions = new List<GameAction>();

            Assert.Throws<InvalidOperationException>(() => GameDataHelper.GetAllPlayersNames(actions));
        }

        [Fact]
        public void GetAllPlayersNames_ShouldFail_WhenNoScoringAction()
        {
            // Arrange
            var actions = new List<GameAction>
                {
                    new GameAction("actionType", 1, 0, "James Harden", "HOU", "James Harden", 0),
                    new GameAction("actionType", 2, 0, "LeBron James", "LAL", "LeBron James", 0)
                };

            Assert.Throws<InvalidOperationException>(() => GameDataHelper.GetAllPlayersNames(actions));

       
       
        }
    }
}
