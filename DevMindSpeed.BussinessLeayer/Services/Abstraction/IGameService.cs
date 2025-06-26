
using DevMindSpeed.BusinessLayer.Dtos;
using DevMindSpeed.Entity.Api.Game;
using DevMindSpeed.Entity.Api.Question;
using DevMindSpeed.Common.Api.Models.Response;

namespace DevMindSpeed.BusinessLayer.Services.Abstraction
{
    public interface IGameService
    {
        Task<ApiResponse<StartGameResponseDto>> StartGameAsync(StartGameRequest request);
        Task<ApiResponse<SubmitAnswerResponseDto>> SubmitAnswerAsync(Guid gameId,SubmitAnswerRequest request);
        Task<ApiResponse<EndGameResponseDto>> EndGameAsync(Guid gameId); 
    }
}
