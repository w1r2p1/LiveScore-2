using LiveScore.Contracts;
using LiveScore.Filters.Games;
using LiveScore.Models.Business;
using System;
using System.Collections.Generic;

namespace LiveScoreTests
{
    public class Mocks
    {
        private static readonly IFilter<Game>[] filters;
        private static readonly League league;
        private static readonly Score[] scores;
        private static readonly MatchDay[] matchDays;
        private static readonly Group[] groups;
        private static readonly Team[] teams;
        private static readonly Game[] games;

        static Mocks()
        {
            filters = new IFilter<Game>[]
            {
                new GamesStartDateFilter("2017-09-28T20:00:00"),
                new GamesEndDateFilter("2017-09-28T21:00:00"),
                new GamesGroupFilter("1"),
                new GamesTeamFilter("1"),
            };

            league = new League
            {
                Id = 1,
                StartYear = 2017,
                EndYear = 2018,
                Name = "Champions League"
            };

            scores = new Score[]
            {
                new Score { Id = 1, HomeTeamGoals = 1, AwayTeamGoals = 0 },
                new Score { Id = 2, HomeTeamGoals = 0, AwayTeamGoals = 0 },
                new Score { Id = 3, HomeTeamGoals = 3, AwayTeamGoals = 2 },
                new Score { Id = 4, HomeTeamGoals = 2, AwayTeamGoals = 2 },
                new Score { Id = 5, HomeTeamGoals = 3, AwayTeamGoals = 1 },
                new Score { Id = 6, HomeTeamGoals = 2, AwayTeamGoals = 0 },
                new Score { Id = 7, HomeTeamGoals = 1, AwayTeamGoals = 1 },
                new Score { Id = 8, HomeTeamGoals = 0, AwayTeamGoals = 0 }
            };

            matchDays = new MatchDay[]
            {
                new MatchDay { Id = 1, Number = 1, League = league },
                new MatchDay { Id = 2, Number = 2, League = league },
                new MatchDay { Id = 3, Number = 3, League = league }
            };

            groups = new Group[]
            {
                new Group { Id = 1, Name = "A", League = league },
                new Group { Id = 2, Name = "B", League = league }
            };

            teams = new Team[]
            {
                new Team { Id = 1, Name = "MANUTD", Group = groups[0] },
                new Team { Id = 2, Name = "BASEL", Group = groups[0] },
                new Team { Id = 3, Name = "BENFIKA", Group = groups[0] },
                new Team { Id = 4, Name = "CSKA", Group = groups[0] },
                new Team { Id = 5, Name = "MANCTY", Group = groups[1] },
                new Team { Id = 6, Name = "DINKYV", Group = groups[1] },
                new Team { Id = 7, Name = "PARTIZAN", Group = groups[1] },
                new Team { Id = 8, Name = "OLYMP", Group = groups[1] }
            };

            groups[0].Teams = new List<Team> { teams[0], teams[1], teams[2], teams[3] };
            groups[1].Teams = new List<Team> { teams[4], teams[5], teams[6], teams[7] };

            games = new Game[]
            {
                new Game { Id = 1, HomeTeam = teams[0], AwayTeam = teams[1],
                    Score = scores[0], KickOff = new DateTime(2017, 9, 28, 19, 0, 0),
                    MatchDay = matchDays[0] },
                new Game { Id = 1, HomeTeam = teams[2], AwayTeam = teams[3],
                    Score = scores[1], KickOff = new DateTime(2017, 9, 28, 19, 30, 0),
                    MatchDay = matchDays[0]  },
                new Game { Id = 1, HomeTeam = teams[0], AwayTeam = teams[3],
                    Score = scores[2], KickOff = new DateTime(2017, 9, 28, 20, 0, 0),
                    MatchDay = matchDays[1]  },
                new Game { Id = 1, HomeTeam = teams[2], AwayTeam = teams[1],
                    Score = scores[3], KickOff = new DateTime(2017, 9, 28, 20, 30, 0),
                    MatchDay = matchDays[1]  },
                new Game { Id = 1, HomeTeam = teams[4], AwayTeam = teams[5],
                    Score = scores[4], KickOff = new DateTime(2017, 9, 28, 21, 0, 0),
                    MatchDay = matchDays[0]  },
                new Game { Id = 1, HomeTeam = teams[6], AwayTeam = teams[7],
                    Score = scores[5], KickOff = new DateTime(2017, 9, 28, 21, 30, 0),
                    MatchDay = matchDays[0]  },
                new Game { Id = 1, HomeTeam = teams[5], AwayTeam = teams[7],
                    Score = scores[6], KickOff = new DateTime(2017, 9, 28, 22, 0, 0),
                    MatchDay = matchDays[1]  },
                new Game { Id = 1, HomeTeam = teams[6], AwayTeam = teams[5],
                    Score = scores[7], KickOff = new DateTime(2017, 9, 28, 22, 30, 0),
                    MatchDay = matchDays[1]  }
            };
        }

        public static IFilter<Game>[] GetFilters()
        {
            return filters;
        }

        public static League GetLeague()
        {
            return league;
        }

        public static Score[] GetScores()
        {
            return scores;
        }

        public static MatchDay[] GetMatchDays()
        {
            return matchDays;
        }

        public static Group[] GetGroups()
        {
            return groups;
        }

        public static Team[] GetTeams()
        {
            return teams;
        }

        public static Game[] GetGames()
        {
            return games;
        }
    }
}
