using System.Collections.Generic;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Models.Entity;

namespace IssueTrackingSystemApi.Dao
{
    public interface IProjectDao
    {
        int CreateProject(ProjectEntity project);
        int DeleteProject(int id);
        IEnumerable<ProjectEntity> Query(ProjectEntity condition = null);
        int UpdateProject(ProjectEntity condition, ProjectEntity project);
        int CreateUserProjectRelation(UserProjectRelationEntity entity);
        IEnumerable<UserProjectRelationEntity> GetRelationByProjectId(int? projectId);
        int DeleteRelationByProjectId(int projectId);
    }
}