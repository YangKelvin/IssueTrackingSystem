using System.Collections.Generic;
using IssueTrackingSystemApi.Models.Entity;

namespace IssueTrackingSystemApi.Dao
{
    public interface IIssueDao
    {
        int CreatIssue(IssueEntity issue);
        int DeleteIssue(int id);
        IEnumerable<IssueEntity> Query(IssueEntity conition = null);
        int UpdateIssue(IssueEntity conition, IssueEntity issue);
    }
}