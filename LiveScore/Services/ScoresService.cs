using LiveScore.Contracts;
using System;
using LiveScore.Models.Request;
using System.Text.RegularExpressions;
using LiveScore.Models.Business;
using System.Linq;
using LiveScore.Utils;
using System.Collections.Generic;

namespace LiveScore.Services
{
    /// <summary>
    /// Implementation of scores-related service functionalities.
    /// </summary>
    public class ScoresService : IScoresService
    {
        private readonly IUnitOfWork dbUnit;
        private readonly IGroupService groupService;
        private readonly IFilterFactory<Game> filters;

        private const string scoreRegex = @"(\d{1,2}):(\d{1,2})\s*(\((\d{1,2}):(\d{1,2})\))*\s*(\((\d{1,2}):(\d{1,2})\))*";

        /// <summary>
        /// Constructor that receives unit of work, group service, and game filter factory as parameters.
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// /// <param name="service">Group service</param>
        /// /// <param name="filterFactory">Games filter factory</param>
        public ScoresService(IUnitOfWork unitOfWork, IGroupService service, IFilterFactory<Game> filterFactory)
        {
            this.dbUnit = unitOfWork;
            this.groupService = service;
            this.filters = filterFactory;
        }

        /// <summary>
        /// This method obtains filtered or all scores.
        /// </summary>
        /// <param name="scoreFilters">Score filters</param>
        /// <returns>Multiple filtered game scores</returns>
        public GameScore[] GetScores(Filter[] scoreFilters = null)
        {
            var games = dbUnit.Games.Get();

            if (scoreFilters == null || !scoreFilters.Any())
            {
                return CreateGameScores(games);
            }

            var filteredGames = new List<Game>();
            var firstFilter = true;

            foreach (var filter in scoreFilters)
            {
                var testFilter = filters.Create(filter);
                var tempGames = games.Where(g => testFilter.Check(g));

                if (firstFilter)
                {
                    filteredGames = tempGames.ToList();
                    firstFilter = false;
                    continue;
                }

                if (filter.Relation == FilterRelation.AND)
                {
                    filteredGames = filteredGames.Intersect(tempGames).ToList();
                }
                else
                {
                    filteredGames = filteredGames.Union(tempGames).ToList();
                }
            }

            return CreateGameScores(filteredGames);
        }

        /// <summary>
        /// This method allows submition of new scores.
        /// </summary>
        /// <param name="gameScores">Scores to be submited</param>
        public void SubmitScores(GameScore[] gameScores)
        {
            foreach (var gs in gameScores)
            {
                var score = ConvertScore(gs);
                var game = ConvertGame(gs);
                game.Score = score;

                dbUnit.Scores.Insert(score);
                dbUnit.Games.Insert(game);

                dbUnit.Save();
            }
        }

        /// <summary>
        /// This method allows update of existing scores.
        /// </summary>
        /// <param name="scores">Scores to be updated</param>
        public void UpdateScores(GameScore[] gameScores)
        {
            foreach (var gs in gameScores)
            {
                var newScore = ConvertScore(gs);
                var refGame = ConvertGame(gs);

                var oldGame = dbUnit.Games
                    .Get(g => g.Equals(refGame))
                    .Single();

                var oldScore = oldGame.Score;
                oldScore.HomeTeamGoals = newScore.HomeTeamGoals;
                oldScore.AwayTeamGoals = newScore.AwayTeamGoals;

                dbUnit.Scores.Update(oldScore);

                dbUnit.Save();
            }
        }

        private GameScore[] CreateGameScores(IEnumerable<Game> games)
        {
            var gameScores = new List<GameScore>();

            foreach (var game in games)
            {
                var gameScore = new GameScore
                {
                    LeagueTitle = groupService.GetLeagueName(GetLeagueIdFromGame(game)),
                    Group = GetGroupFromGame(game).Name,
                    HomeTeam = game.HomeTeam.Name,
                    AwayTeam = game.AwayTeam.Name,
                    MatchDay = game.MatchDay.Number,
                    KickOffAt = DateTimeParser.PrintDate(game.KickOff),
                    Score = string.Format("{0}:{1}", game.Score.HomeTeamGoals, game.Score.AwayTeamGoals)
                };
            }

            return gameScores.ToArray();
        }

        private int GetLeagueIdFromGame(Game game)
        {
            return dbUnit.MatchDays
                .Get(game.MatchDay.Id)
                .League
                .Id;
        }

        private Models.Business.Group GetGroupFromGame(Game game)
        {
            return dbUnit.Teams
                .Get(game.HomeTeam.Id)
                .Group;
        }

        private Score ConvertScore(GameScore gs)
        {
            var matchGroups = Regex.Matches(gs.Score, scoreRegex).FirstOrDefault().Groups;

            return new Score
            {
                HomeTeamGoals = Int32.Parse(matchGroups[1].Value),
                AwayTeamGoals = Int32.Parse(matchGroups[2].Value)
            };
        }

        private Game ConvertGame(GameScore gs)
        {
            return new Game
            {
                KickOff = DateTimeParser.ParseDate(gs.KickOffAt),
                HomeTeam = dbUnit.Teams
                    .Get(t => t.Name.Equals(gs.HomeTeam))
                    .FirstOrDefault(),
                AwayTeam = dbUnit.Teams
                    .Get(t => t.Name.Equals(gs.AwayTeam))
                    .FirstOrDefault(),
                MatchDay = GetMatchday(gs) ?? CreateMatchDay(gs)
            };
        }

        private MatchDay GetMatchday(GameScore gs)
        {
            return dbUnit.MatchDays
                .Get(md =>
                    md.Number == gs.MatchDay &&
                    groupService.GetLeagueName(md.League.Id).Equals(gs.LeagueTitle))
                .FirstOrDefault();
        }

        private MatchDay CreateMatchDay(GameScore gs)
        {
            return new MatchDay
            {
                Number = gs.MatchDay,
                League = dbUnit.Leagues
                    .Get(l => groupService.GetLeagueName(l.Id).Equals(gs.LeagueTitle))
                    .Single()
            };
        }
    }
}
