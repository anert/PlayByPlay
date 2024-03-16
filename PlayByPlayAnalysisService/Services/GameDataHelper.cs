using PlayByPlayAnalysisService.Models;

namespace PlayByPlayAnalysisService.Services
{
    public static class GameDataHelper
    {
        /// <summary>
        /// Retrieves the names of all players involved in the game.
        /// </summary>
        /// <param name="actions">The collection of game actions.</param>
        /// <returns>A dictionary containing the names of players grouped by their team affiliation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the first scoring action cannot be determined.</exception>
        public static Dictionary<string, List<string>> GetAllPlayersNames(IEnumerable<GameAction> actions)
        {
            // Retrieve the first scoring action
            var firstScoringAction = GetFirstBasketAction();
            if (firstScoringAction == null)
            {
                throw new InvalidOperationException("Cannot determine first scoring action");
            }

            // Determine the team tricode and hosting status of the first scoring team
            var firstScoringTeamTricode = firstScoringAction?.TeamTricode;
            var isFirstScoringTeamHosting = firstScoringAction?.ScoreHome != 0;

            // Define constants for team affiliations
            const string Home = "home";
            const string Away = "away";

            // Group the actions by team affiliation and retrieve the distinct player names
            return actions
                .Where(action => action.PersonId != 0)
                .GroupBy(action => MapGroupForAction(action.TeamTricode))
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(x => x.PlayerNameI).Distinct().ToList()
                );

            // Maps the team tricode to the corresponding team affiliation
            string MapGroupForAction(string teamTricode) => teamTricode.Equals(firstScoringTeamTricode) ?
                isFirstScoringTeamHosting ? Home : Away : isFirstScoringTeamHosting ? Away : Home;

            // Retrieves the first basket action (Ordering by action number just in case)
            GameAction? GetFirstBasketAction() => actions.OrderBy(action => action.ActionNumber)
                .FirstOrDefault(action => !string.IsNullOrEmpty(action.ShotResult) && action.ShotResult.Equals("Made"));
        }
    }
}
