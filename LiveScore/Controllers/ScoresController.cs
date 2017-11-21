using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LiveScore.Models.Request;
using LiveScore.Models.Response;
using LiveScore.Contracts;

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

        #region LVL1

        [Authorize("create:scores")]
        [HttpPut("{id}")]
        public IActionResult SubmitGroupScores(int id, [FromBody]GameScore[] gameScores)
        {
            throw new NotImplementedException();
        }

        #endregion LVL1

        #region LVL2

        [Authorize("read:scores")]
        [HttpGet("{id}")]
        public IActionResult GetGroupStandings(int id)
        {
            throw new NotImplementedException();
        }

        #endregion LVL2

        #region LVL3

        [Authorize("create:scores")]
        [HttpPut]
        public IActionResult SubmitGroupScores([FromBody]GameScore[] groupScores)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult GetGroupStandings()
        {
            throw new NotImplementedException();
        }

        #endregion LVL3

        #region LVL4

        [Authorize("read:scores")]
        [HttpGet]
        public GroupStandings[] GetGroupStandings(int[] groupIds)
        {
            throw new NotImplementedException();
        }

        #endregion LVL4

        #region LVL5

        [Authorize("create:scores")]
        [HttpPut]
        public GroupStandings[] SubmitGameScores([FromBody]GameScore[] gameScores)
        {
            throw new NotImplementedException();
        }

        #endregion LVL5

        #region LVL6

        [Authorize("read:scores")]
        [HttpGet]
        public IActionResult GetScores(Filter[] scoreFilters)
        {
            throw new NotImplementedException();
        }

        #endregion LVL6

        #region LVL7

        [Authorize("create:scores")]
        [HttpPost]
        public IActionResult UpdateScores(GameScore[] scores)
        {
            throw new NotImplementedException();
        }

        #endregion LVL7
    }
}
