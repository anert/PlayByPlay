namespace PlayByPlayAnalysisService.Interfaces
{
    public interface IGameService
    {
        Dictionary<string, List<string>> GetAllPlayersNames();
        List<string> GetAllActionsByPlayerName(string playerName);
    }
}