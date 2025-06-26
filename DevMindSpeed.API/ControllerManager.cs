using DevMindSpeed.Common.Db.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevMindSpeed
{
    [ApiController]
    public class ControllerManager : ControllerBase
    {
        public readonly RequestUserEntity _requestUserEntity;

        public ControllerManager(RequestUserEntity requestUserEntity)
        {
            _requestUserEntity = requestUserEntity;
        }

        protected void ConstructRequestUserEntity()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            _requestUserEntity.UserId = Guid.Parse(userIdClaim.Value);
        }
    }
}