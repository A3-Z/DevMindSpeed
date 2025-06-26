using DevMindSpeed.BusinessLayer.Dtos;
using DevMindSpeed.BusinessLayer.Services.Abstraction;
using DevMindSpeed.Common.Api.Models.Request;
using DevMindSpeed.Entity.Api.Game;
using DevMindSpeed.Entity.Api.Question;
using DevMindSpeed.Common.Api.Models.Response;
using DevMindSpeed.Common.Db.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevMindSpeed.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerManager
    {
       
        private readonly IGameService _gameService;
        public GameController(IGameService gameService, RequestUserEntity requestUserEntity)
            : base(requestUserEntity)
        {
            _gameService = gameService;
        }

        [HttpPost ("start")]
        public async Task <ApiResponse<StartGameResponseDto>> Start([FromBody] ApiRequest<StartGameRequest> request)
        {
            return await _gameService.StartGameAsync (request.Request);
        }
        [HttpPost ("{gameId}/submit")]
        public async Task <ApiResponse<SubmitAnswerResponseDto>> SubmitAnswer (Guid gameId, [FromBody] ApiRequest<SubmitAnswerRequest> request)
        {
            return  await _gameService.SubmitAnswerAsync (gameId, request.Request);
        }
        [HttpGet ("{gameId}/end")]
        public async Task <ApiResponse<EndGameResponseDto>> EndGame(Guid gameId)
        {
            return await _gameService.EndGameAsync (gameId);
        }
    } 
}
