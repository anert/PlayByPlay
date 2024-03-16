namespace PlayByPlayAnalysisService.Interfaces
{
    public interface IGameService
    {
        List<string> GetAllActionsByPlayerName(string playerName);
        Dictionary<string, List<string>> GetAllPlayersNames();
    }
}