using LiveScore.Models.Response;

namespace LiveScore.Contracts
{
    /// <summary>
    /// Interface that defines group-related service functionalities.
    /// </summary>
    public interface IGroupService
    {
        /// <summary>
        /// This method checks whether givren group Id is valid.
        /// </summary>
        /// <param name="groupId">Test group Id</param>
        /// <returns>Whether or not group with givrn Id exists</returns>
        bool CheckGroupId(int groupId);

        /// <summary>
        /// This method gets standigs for given group Ids or all existing groups.
        /// </summary>
        /// <param name="groupIds">Gruop Ids for result filtering</param>
        /// <returns>Standings for requested or all groups</returns>
        GroupStandings[] GetStandings(int[] groupIds = null);

        /// <summary>
        /// This method obtains formated league name from given league Id.
        /// </summary>
        /// <param name="leagueId">Given league Id</param>
        /// <returns>Formated league name</returns>
        string GetLeagueName(int leagueId);
    }
}
