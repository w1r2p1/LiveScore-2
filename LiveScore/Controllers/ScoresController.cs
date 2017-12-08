using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LiveScore.Models.Request;
using LiveScore.Contracts;
using LiveScore.Utils;
using System.Linq;

namespace LiveScore.Controllers
{
    /// <summary>
    /// Controller with score- and standings-related actions.
    /// </summary>
    [Produces("application/json")]
    [Route("api/scores")]
    public class ScoresController : Controller
    {
        private readonly IScoresService scoresService;
        private readonly IGroupService groupsService;

        /// <summary>
        /// Constructor of scores controller with injected reference to required services.
        /// </summary>
        /// <param name="scoresService">Reference to scores service</param>
        /// <param name="groupsService">Reference to group service</param>
        public ScoresController(IScoresService scoresService, IGroupService groupsService)
        {
            this.scoresService = scoresService;
            this.groupsService = groupsService;
        }

        /// <summary>
        /// This action allows users with certain permission to submit new scores.
        /// </summary>
        /// <param name="gameScores">Multiple game scores</param>
        /// <returns>Http status response</returns>
        [Authorize("create:scores")]
        [HttpPut("submit")]
        public IActionResult SubmitScores([FromBody]GameScore[] gameScores)
        {
            if (gameScores == null || !gameScores.Any())
            {
                return CreateErrorCodeMessage(ErrorCode.MissingScores);
            }

            scoresService.SubmitScores(gameScores);
            return Ok();
        }

        /// <summary>
        /// This action allows users with certain permission to update existing scores.
        /// </summary>
        /// <param name="gameScores">Multiple game scores</param>
        /// <returns>Http status response</returns>
        [Authorize("create:scores")]
        [HttpPost("update")]
        public IActionResult UpdateScores(GameScore[] gameScores)
        {
            if (gameScores == null || !gameScores.Any())
            {
                return CreateErrorCodeMessage(ErrorCode.MissingScores);
            }

            scoresService.UpdateScores(gameScores);
            return Ok();
        }

        /// <summary>
        /// This action allows users with certain permission to fetch existing scores.
        /// </summary>
        /// <param name="scoreFilters">Score filters</param>
        /// <returns>Filtered game scores</returns>
        [Authorize("read:scores")]
        [HttpGet("fetch")]
        public IActionResult GetScores(Filter[] scoreFilters)
        {
            return Ok(scoresService.GetScores(scoreFilters));
        }

        /// <summary>
        /// This action allows users with certain permission to fetch group standings.
        /// </summary>
        /// <param name="groupIds">Group Ids</param>
        /// <returns>Group standings for provided Ids</returns>
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
