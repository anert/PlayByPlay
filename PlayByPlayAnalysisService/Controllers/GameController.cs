namespace PlayByPlayAnalysisService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PlayByPlayAnalysisService.Interfaces;

    [ApiController]
    [Route("[controller]")]
    public class GameController(IGameService gameService) : ControllerBase
    {
        private readonly IGameService _gameService = gameService;

        [HttpGet("players")]
        public IActionResult GetAllPlayersNames()
        {
            var result = _gameService.GetAllPlayersNames();
            return Ok(result);
        }

        [HttpGet("{playerName}/actions")]
        public IActionResult GetAllActionsByPlayerName(string playerName)
        {
            var result = _gameService.GetAllActionsByPlayerName(playerName);
            return Ok(result);
        }
    }
}