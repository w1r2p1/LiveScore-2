using LiveScore.Contracts;
using LiveScore.Models.Response;
using System.Linq;
using System.Collections.Generic;
using LiveScore.Models.Business;

namespace LiveScore.Services
{
    /// <summary>
    /// Implementation of group-related service functionalities.
    /// </summary>
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork dbUnit;

        /// <summary>
        /// Constructor that receives unit of work parameter.
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        public GroupService(IUnitOfWork unitOfWork)
        {
            dbUnit = unitOfWork;
        }

        /// <summary>
        /// This method checks whether givren group Id is valid.
        /// </summary>
        /// <param name="groupId">Test group Id</param>
        /// <returns>Whether or not group with givrn Id exists</returns>
        public bool CheckGroupId(int groupId)
        {
            return dbUnit.Groups.GetById(groupId) != null;
        }

        /// <summary>
        /// This method gets standigs for given group Ids or all existing groups.
        /// </summary>
        /// <param name="groupIds">Gruop Ids for result filtering</param>
        /// <returns>Standings for requested or all groups</returns>
        public GroupStandings[] GetStandings(int[] groupIds = null)
        {
            var filtered = groupIds != null && groupIds.Any();
            var groups = filtered ?
                dbUnit.Groups.Query()
                    .Where(g => groupIds.Any(gid => gid == g.Id))
                    .Include(g => g.Teams)
                    .Execute() :
                dbUnit.Groups.Query()
                    .Include(g => g.Teams)
                    .Execute();

            var resultModels = new List<GroupStandings>();

            foreach (var group in groups)
            {
                var resultModel = new GroupStandings
                {
                    LeagueTitle = GetLeagueNameByGroupId(group.Id),
                    Group = group.Name,
                    MatchDay = dbUnit.MatchDays.Query()
                        .Where(md => md.League == group.League)
                        .Execute()
                        .OrderByDescending(md => md.Number)
                        .First()
                        .Number
                };

                var resultSubModels = new List<ClubStandings>();

                foreach (var team in group.Teams)
                {
                    var resultSubModel = new ClubStandings
                    {
                        Team = team.Name
                    };

                    var games = dbUnit.Games.Query()
                        .Where(g => g.HomeTeam.Id == team.Id || g.AwayTeam.Id == team.Id)
                        .Include(g => g.HomeTeam)
                        .Include(g => g.AwayTeam)
                        .Include(g => g.Score)
                        .Execute();

                    foreach (var game in games)
                    {
                        var gameOutcome = game.Score.Winner;

                        if (gameOutcome == MatchWinner.Home)
                        {
                            if (game.HomeTeam.Id == team.Id)
                            {
                                resultSubModel.Win++;
                                resultSubModel.Points += 3;
                            }
                            else
                            {
                                resultSubModel.Lose++;
                            }
                        }
                        else if (gameOutcome == MatchWinner.Draw)
                        {
                            resultSubModel.Draw++;
                            resultSubModel.Points += 1;
                        }
                        else
                        {
                            if (game.AwayTeam.Id == team.Id)
                            {
                                resultSubModel.Win++;
                                resultSubModel.Points += 3;
                            }
                            else
                            {
                                resultSubModel.Lose++;
                            }
                        }

                        resultSubModel.Goals += (game.HomeTeam.Id == team.Id) ? game.Score.HomeTeamGoals : game.Score.AwayTeamGoals;
                        resultSubModel.GoalsAgainst += (game.HomeTeam.Id == team.Id) ? game.Score.AwayTeamGoals : game.Score.HomeTeamGoals;

                        resultSubModel.PlayedGames++;
                        resultSubModel.GoalDifference = resultSubModel.Goals - resultSubModel.GoalsAgainst;
                    }

                    resultSubModels.Add(resultSubModel);
                }

                resultModel.Standings = resultSubModels
                    .OrderByDescending(cs => cs.Points)
                    .ThenByDescending(cs => cs.Goals)
                    .ThenByDescending(cs => cs.GoalDifference)
                    .ToArray();

                for (int i = 0; i < resultModel.Standings.Count(); i++)
                {
                    resultModel.Standings[i].Rank = i + 1;
                }

                resultModels.Add(resultModel);
            }

            return resultModels.ToArray();
        }

        /// <summary>
        /// This method obtains formated league name from given league Id.
        /// </summary>
        /// <param name="leagueId">Given league Id</param>
        /// <returns>Formated league name</returns>
        public string GetLeagueName(int leagueId)
        {
            var league = dbUnit.Leagues.GetById(leagueId);
            var endYearTxt = league.EndYear.ToString();

            return string.Format("{0} {1}/{2}", league.Name, league.StartYear, endYearTxt.Substring(endYearTxt.Length - 2));
        }

        private string GetLeagueNameByGroupId(int groupId)
        {
            var league = dbUnit.Groups.GetById(groupId).League.Id;
            return GetLeagueName(league);
        }
    }
}
