using PlayByPlayAnalysisService.Models;

namespace PlayByPlayAnalysisService.Services
{
    public static class GameDataHelper
    {
        public static Dictionary<string, List<string>> GetAllPlayersNames(IEnumerable<GameAction> actions)
        {
            var firstScoringAction = GetFirstBasketAction();
            if (firstScoringAction == null)
            {
                throw new InvalidOperationException("Cannot determine first scoring action");
            }

            var firstScoringTeamTricode = firstScoringAction?.TeamTricode;
            var isFirstScoringTeamHosting = firstScoringAction?.ScoreHome != 0;

            const string Home = "home";
            const string Away = "away";

            return actions
                .Where(action => action.PersonId != 0)
                .GroupBy(action => MapGroupForAction(action.TeamTricode))
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(x => x.PlayerNameI).Distinct().ToList()
                    );

            string MapGroupForAction(string teamTricode) => teamTricode.Equals(firstScoringTeamTricode) ?
                isFirstScoringTeamHosting ? Home : Away : isFirstScoringTeamHosting ? Away : Home;

            GameAction? GetFirstBasketAction() => actions.OrderBy(action => action.ActionNumber)
                .FirstOrDefault(action => !string.IsNullOrEmpty(action.ShotResult) && action.ShotResult.Equals("Made"));
        }
    }
}