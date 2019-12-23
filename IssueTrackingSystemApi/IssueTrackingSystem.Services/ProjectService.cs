using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IssueTrackingSystemApi.CommonTools;
using IssueTrackingSystemApi.Dao;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Models.Entity;

namespace IssueTrackingSystemApi.Services
{
    public class ProjectService : IProjectService
    {
        private IProjectDao ProjectDao { get => new ProjectDao(); }
        private IUserDao UserDao { get => new UserDao(); }

        public ProjectService()
        {

        }

        /// <summary>
        /// Create a new project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public int CreateProject(Project project, User createUser)
        {
            ProjectEntity projectEntity = project.ObjectConvert<ProjectEntity>();

            var projectId = ProjectDao.CreateProject(projectEntity);

            // 新增 UserProjectRelation 的關係
            ProjectDao.CreateUserProjectRelation(new UserProjectRelationEntity()
            {
                ProjectId = projectId,
                UserId = createUser.Id,
                ProjectCharactorId = 1  // Manager
            });

            return projectId;
        }

        /// <summary>
        /// Get a specific project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Project GetProjectById(int id)
        {
            ProjectEntity projectEntity = ProjectDao.Query(new ProjectEntity() { Id = id }).FirstOrDefault();
            List<UserEntity> userEntitys = UserDao.Query().ToList();

            Project project = projectEntity.ObjectConvert<Project>();

            var relations = ProjectDao.GetRelationByProjectId(project.Id);

            List<User> developers = new List<User>();
            List<User> generals = new List<User>();
            // 取得該 project 下的 developer
            foreach (var r in relations)
            {
                User tmp = userEntitys.Find(u => u.Id == r.UserId).ObjectConvert<User>();
                if (r.ProjectCharactorId == 2) //developer
                {
                    developers.Add(tmp);
                }
                else if(r.ProjectCharactorId == 3) // general
                {
                    generals.Add(tmp);
                }
            }
            project.Developers = developers;
            project.General = generals;

            return project;
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns></returns>
        public List<Project> GetAllProjects()
        {
            List<ProjectEntity> projectEntities = ProjectDao.Query().ToList();
            List<UserEntity> userEntitys = UserDao.Query().ToList();
            var projects = projectEntities.Select(i => i.ObjectConvert<Project>()).ToList();

            foreach (var project in projects )
            {
                var relations = ProjectDao.GetRelationByProjectId(project.Id);
                List<User> developers = new List<User>();
                List<User> generals = new List<User>();
                foreach (var r in relations)
                {
                    User tmp = userEntitys.Find(u => u.Id == r.UserId).ObjectConvert<User>();
                    if (r.ProjectCharactorId == 2) //developer
                    {
                        developers.Add(tmp);
                    }
                    else if (r.ProjectCharactorId == 3) // general
                    {
                        generals.Add(tmp);
                    }
                }
                project.Developers = developers;
                project.General = generals;
            }

            return projects;
        }

        /// <summary>
        /// Update a specific project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public int UpdateProject(Project project)
        {
            return ProjectDao.UpdateProject(new ProjectEntity() { Id = project.Id }, project.ObjectConvert<ProjectEntity>());
        }
        
    }
}
