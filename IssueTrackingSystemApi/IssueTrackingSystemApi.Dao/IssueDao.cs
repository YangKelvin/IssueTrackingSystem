using System;
using System.Collections.Generic;
using System.Text;
using IssueTrackingSystemApi.Models.Entity;
using IssueTrackingSystemApi.CommonTools;

namespace IssueTrackingSystemApi.Dao
{
    public class IssueDao : IIssueDao
    {
        public int CreatIssue(IssueEntity issue)
        {
            return SqlHelper.Insert(issue);
        }

        public int DeleteIssue(int id)
        {
            return SqlHelper.Delete(new IssueEntity() { Id = id });
        }

        public int UpdateIssue(IssueEntity conition, IssueEntity issue)
        {
            return SqlHelper.Update(conition, issue);
        }

        public IEnumerable<IssueEntity> Query(IssueEntity conition = null)
        {
            return SqlHelper.Select(conition);
        }

    }
}
