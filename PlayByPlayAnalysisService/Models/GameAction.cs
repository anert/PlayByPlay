namespace PlayByPlayAnalysisService.Models
{
    public record GameAction(string ActionType,
                             int ActionNumber,
                             int PersonId,
                             string PlayerNameI,
                             string TeamTricode,
                             string ShotResult,
                             int ScoreHome);
}