
using PlayByPlayAnalysisService.Interfaces;

namespace PlayByPlayAnalysisService.Services
{
    public class GameService(GameDataService gameDataService) : IGameService
    {
        private readonly GameAnalysisResult _gameAnalysisResult = gameDataService.FetchGameDataAsync().Result;
        public Dictionary<string, List<string>> GetAllPlayersNames() => _gameAnalysisResult.NamesByTeams;
        public List<string> GetAllActionsByPlayerName(string playerName) => _gameAnalysisResult.RelevantActions
                .Where(action => string.Equals(action.PlayerNameI, playerName, StringComparison.OrdinalIgnoreCase))
                .Select(action => action.ActionType)
                .Distinct()
                .ToList();
    }
}