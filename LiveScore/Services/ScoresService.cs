using LiveScore.Contracts;
using System;
using LiveScore.Models.Request;

namespace LiveScore.Services
{
    public class ScoresService : IScoresService
    {
        public GameScore[] GetScores(Filter[] scoreFilters = null)
        {
            throw new NotImplementedException();
        }

        public void SubmitScores(GameScore[] gameScores)
        {
            throw new NotImplementedException();
        }

        public void UpdateScores(GameScore[] scores)
        {
            throw new NotImplementedException();
        }
    }
}
