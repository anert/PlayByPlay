namespace PlayByPlayAnalysisService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PlayByPlayAnalysisService.Interfaces;

    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("GetAllPlayersNames")]
        public IActionResult GetAllPlayersNames()
        {
            var result = _gameService.GetAllPlayersNames();
            return Ok(result);
        }

        [HttpGet("GetAllActionsByPlayerName/{playerName}")]
        public IActionResult GetAllActionsByPlayerName(string playerName)
        {
            var result = _gameService.GetAllActionsByPlayerName(playerName);
            return Ok(result);
        }
    }
}