using LiveScore.Models.Request;

namespace LiveScore.Contracts
{
    public interface IScoresService
    {
        void SubmitScores(GameScore[] gameScores);
        void UpdateScores(GameScore[] scores);
        GameScore[] GetScores(Filter[] scoreFilters = null);
    }
}
