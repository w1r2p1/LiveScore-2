using LiveScore.Models.Request;
using LiveScore.Models.Response;
using LiveScore.Utils;

namespace LiveScore.Contracts
{
    public interface IScoresService
    {
        GroupStandings[] SubmitScores(GameScore[] gameScores);
        ErrorCode UpdateScores(GameScore[] scores);
        GameScore[] GetScores(Filter[] scoreFilters = null);
    }
}
