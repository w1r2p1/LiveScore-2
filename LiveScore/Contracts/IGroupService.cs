using LiveScore.Models.Request;
using LiveScore.Models.Response;

namespace LiveScore.Contracts
{
    public interface IGroupService
    {
        bool CheckGroupId(int groupId);
        string GetGroupName(int groupId);
        string GetLeagueName(int groupId);
        GroupStandings[] GetStandings(Filter[] standingsFilters = null);
    }
}
