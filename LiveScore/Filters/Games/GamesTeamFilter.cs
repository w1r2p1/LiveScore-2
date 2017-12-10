using LiveScore.Contracts;
using LiveScore.Exceptions;
using LiveScore.Models.Business;
using System;

namespace LiveScore.Filters.Games
{
    /// <summary>
    /// Filter that checks whether team has participated in provided game.
    /// </summary>
    public class GamesTeamFilter : IFilter<Game>
    {
        private readonly int teamId;

        /// <summary>
        /// Constructor that receives team Id parameter.
        /// </summary>
        /// <param name="groupId">Team Id parameter</param>
        public GamesTeamFilter(string teamId)
        {
            if (Int32.TryParse(teamId, out var tmpTeamId))
            {
                this.teamId = tmpTeamId;
            }
            else
            {
                throw new ClientRequestException(string.Format("Invalid team Id value: {0}", teamId));
            }
        }

        /// <summary>
        /// This method checks whether defined team has participated in provided game.
        /// </summary>
        /// <param name="game">Game to be tested</param>
        /// <returns>Whether or not defined team participated in given game.</returns>
        public bool Check(Game game)
        {
            return game.HomeTeam.Id == teamId || game.AwayTeam.Id == teamId;
        }
    }
}
