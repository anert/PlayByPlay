using PlayByPlayAnalysisService.Models;

public record GameAnalysisResult(Dictionary<string, List<string>> NamesByTeams, List<GameAction> RelevantActions);