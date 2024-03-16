
using Newtonsoft.Json.Linq;
using PlayByPlayAnalysisService.Models;
namespace PlayByPlayAnalysisService.Services
{
    public class GameDataService(HttpClient httpClient, IConfiguration configuration)
    {
        private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        private readonly string _url = configuration.GetValue<string>("GameDataUrl") ?? throw new ArgumentNullException(nameof(configuration));

        public async Task<GameAnalysisResult> FetchGameDataAsync()
        {
            var json = await _httpClient.GetStringAsync(_url);
            var actions = JObject.Parse(json)["game"]?["actions"]?.ToObject<List<GameAction>>();

            if (actions == null || actions.Count == 0)
            {
                throw new InvalidOperationException("No actions found in game data.");
            }
            var relevantActions = actions.Where(action => action.PersonId != 0).ToList();
            var namesByTeams = GameDataHelper.GetAllPlayersNames(relevantActions);
            return new GameAnalysisResult(namesByTeams, relevantActions);
        }
    }
}