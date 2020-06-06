using System.Threading.Tasks;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Produces("application/json")]
    [Route("api/GameOfLife")]
    public class GameOfLifeController : Controller
    {
        private GameOfLife _gameOfLife;
        public GameOfLifeController()
        {
            _gameOfLife = GameOfLife.Instance;
        }

        [HttpGet("Start")]
        public async Task<IActionResult> Start()
        {
            var zeroPlayer = new ZeroPlayer();
            _gameOfLife.AddPlayer(zeroPlayer);
            await _gameOfLife.Start();
            return Ok("Started");
        }

        [HttpGet("Stop")]
        public async Task<IActionResult> Stop()
        {
            await _gameOfLife.Stop();
            return Ok("Stopped");
        }
    }
}