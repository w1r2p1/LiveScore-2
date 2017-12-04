using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LiveScore.Models.Request;
using LiveScore.Contracts;
using LiveScore.Utils;
using System.Linq;

namespace LiveScore.Controllers
{
    [Produces("application/json")]
    [Route("api/scores")]
    public class ScoresController : Controller
    {
        private readonly IScoresService scoresService;
        private readonly IGroupService groupsService;

        public ScoresController(IScoresService scoresService, IGroupService groupsService)
        {
            this.scoresService = scoresService;
            this.groupsService = groupsService;
        }

        [Authorize("create:scores")]
        [HttpPut("submit")]
        public IActionResult SubmitScores([FromBody]GameScore[] groupScores)
        {
            if (groupScores == null || !groupScores.Any())
            {
                return CreateErrorCodeMessage(ErrorCode.MissingScores);
            }

            scoresService.SubmitScores(groupScores);
            return Ok();
        }

        [Authorize("create:scores")]
        [HttpPost("update")]
        public IActionResult UpdateScores(GameScore[] scores)
        {
            if (scores == null || !scores.Any())
            {
                return CreateErrorCodeMessage(ErrorCode.MissingScores);
            }

            scoresService.UpdateScores(scores);
            return Ok();
        }

        [Authorize("read:scores")]
        [HttpGet("fetch")]
        public IActionResult GetScores(Filter[] scoreFilters)
        {
            return Ok(scoresService.GetScores(scoreFilters));
        }

        [Authorize("read:scores")]
        [HttpGet("standings")]
        public IActionResult GetStandings(int[] groupIds)
        {
            if (groupIds != null && groupIds.Any() && !groupIds.All(g => groupsService.CheckGroupId(g)))
            {
                return CreateErrorCodeMessage(ErrorCode.InvalidGroupId);
            }

            return Ok(groupsService.GetStandings(groupIds));
        }

        private BadRequestObjectResult CreateErrorCodeMessage(ErrorCode errorCode)
        {
            return BadRequest(new { error = errorCode });
        }
    }
}
