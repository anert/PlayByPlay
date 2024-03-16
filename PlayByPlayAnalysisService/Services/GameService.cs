
using PlayByPlayAnalysisService.Interfaces;

namespace PlayByPlayAnalysisService.Services
{
    /// <summary>
    /// Represents a service for game analysis.
    /// </summary>
    public class GameService(GameDataService gameDataService) : IGameService
    {
        private readonly GameAnalysisResult _gameAnalysisResult = gameDataService.FetchGameDataAsync().Result;

        /// <summary>
        /// Gets all players' names.
        /// </summary>
        /// <returns>A dictionary containing the names of all players grouped by teams.</returns>
        public Dictionary<string, List<string>> GetAllPlayersNames() => _gameAnalysisResult.NamesByTeams;

        /// <summary>
        /// Gets all actions performed by a specific player.
        /// </summary>
        /// <param name="playerName">The name of the player.</param>
        /// <returns>A list of all actions performed by the player.</returns>
        public List<string> GetAllActionsByPlayerName(string playerName) => _gameAnalysisResult.RelevantActions
                .Where(action => string.Equals(action.PlayerNameI, playerName, StringComparison.OrdinalIgnoreCase))
                .Select(action => action.ActionType)
                .ToList();
    }
}