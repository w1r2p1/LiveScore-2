using LiveScore.Models.Request;

namespace LiveScore.Contracts
{
    /// <summary>
    /// Interface that defines scores-related functionalities.
    /// </summary>
    public interface IScoresService
    {
        /// <summary>
        /// This method allows submition of new scores.
        /// </summary>
        /// <param name="gameScores">Scores to be submited</param>
        void SubmitScores(GameScore[] gameScores);

        /// <summary>
        /// This method allows update of existing scores.
        /// </summary>
        /// <param name="scores">Scores to be updated</param>
        void UpdateScores(GameScore[] scores);

        /// <summary>
        /// This method obtains filtered or all scores.
        /// </summary>
        /// <param name="scoreFilters">Score filters</param>
        /// <returns>Multiple filtered game scores</returns>
        GameScore[] GetScores(Filter[] scoreFilters = null);
    }
}
