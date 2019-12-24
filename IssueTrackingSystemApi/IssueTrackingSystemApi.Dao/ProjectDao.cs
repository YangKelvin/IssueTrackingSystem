using IssueTrackingSystemApi.CommonTools;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IssueTrackingSystemApi.Dao
{
    public class ProjectDao : IProjectDao
    {
        private IUserDao UserDao { get => new UserDao(); }

        /// <summary>
        /// 創建 project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public int CreateProject(ProjectEntity project)
        {
            return SqlHelper.Insert(project);
        }

        /// <summary>
        /// 依據 id 刪除 project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteProject(int id)
        {
            return SqlHelper.Delete(new ProjectEntity() { Id = id });
        }

        /// <summary>
        /// 更新 project
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public int UpdateProject(ProjectEntity condition, ProjectEntity project)
        {
            return SqlHelper.Update(condition, project);
        }

        /// <summary>
        /// 查詢 project(s)
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<ProjectEntity> Query(ProjectEntity condition = null)
        {
            return SqlHelper.Select(condition);
        }

        /// <summary>
        /// 建立專案和使用者之間的關係
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int CreateUserProjectRelation(UserProjectRelationEntity entity)
        {
            return SqlHelper.Insert(entity);    
        }

        public IEnumerable<UserProjectRelationEntity> GetRelationByProjectId(int? projectId)
        {
            UserProjectRelationEntity condition = new UserProjectRelationEntity()
            {
                ProjectId = projectId
            };
            return SqlHelper.Select(condition);
        }

        /// <summary>
        /// 刪除 relation
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId"></param>
        /// <param name="charactorId"></param>
        /// <returns></returns>
        public int DeleteRelationByProjectId(int projectId)
        {
            return SqlHelper.Delete(new UserProjectRelationEntity() { ProjectId = projectId });
        }

    }
}
