using LiveScore.Models.Request;
using LiveScore.Models.Response;

namespace LiveScore.Contracts
{
    public interface IGroupService
    {
        bool CheckGroupId(int groupId);
        GroupStandings[] GetStandings(int[] groupIds = null);
        string GetLeagueName(int leagueId);
    }
}
