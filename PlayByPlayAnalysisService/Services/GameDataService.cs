
using Newtonsoft.Json.Linq;
using PlayByPlayAnalysisService.Models;
namespace PlayByPlayAnalysisService.Services
{
    /// <summary>
    /// Represents a service for fetching game data and performing game analysis.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="GameDataService"/> class.
    /// </remarks>
    /// <param name="httpClient">The HTTP client used for making requests.</param>
    /// <param name="configuration">The configuration containing the game data URL.</param>
    public class GameDataService(HttpClient httpClient, IConfiguration configuration)
    {
        private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        private readonly string _url = configuration.GetValue<string>("GameDataUrl") ?? throw new ArgumentNullException(nameof(configuration));

        /// <summary>
        /// Fetches the game data asynchronously and performs game analysis.
        /// </summary>
        /// <returns>The result of the game analysis.</returns>
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