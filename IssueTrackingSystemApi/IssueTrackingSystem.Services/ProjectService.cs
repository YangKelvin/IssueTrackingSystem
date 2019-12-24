﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IssueTrackingSystemApi.CommonTools;
using IssueTrackingSystemApi.Dao;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Models.Entity;
using IssueTrackingSystemApi.Models.View;

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
        public int CreateProject(ProjectFront project)
        {
            ProjectEntity projectEntity = project.ObjectConvert<ProjectEntity>();
            var projectId = ProjectDao.CreateProject(projectEntity);

            // 新增 UserProjectRelation 的關係
            // manager
            ProjectDao.CreateUserProjectRelation(new UserProjectRelationEntity()
            {
                ProjectId = projectId,
                UserId = project.managerId,
                ProjectCharactorId = 1  // Manager
            });
            // developer
            foreach (var developerId in project.developersId)
            {
                ProjectDao.CreateUserProjectRelation(new UserProjectRelationEntity()
                {
                    ProjectId = projectId,
                    UserId = developerId,
                    ProjectCharactorId = 2
                });
            }
            // general
            foreach (var generalId in project.generalsId)
            {
                ProjectDao.CreateUserProjectRelation(new UserProjectRelationEntity()
                {
                    ProjectId = projectId,
                    UserId = generalId,
                    ProjectCharactorId = 3
                });
            }

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
                if (r.ProjectCharactorId == 1) //manager
                {
                    project.Manager = tmp;
                }
                else if (r.ProjectCharactorId == 2) //developer
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
                    if (r.ProjectCharactorId == 1) //manager
                    {
                        project.Manager = tmp;
                    }
                    else if (r.ProjectCharactorId == 2) //developer
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
        public int UpdateProject(int projectId, ProjectFront project)
        {
            // TODO: udpate會夾manager, developers跟generals的Id(去ProjectFront看), 幫我把它們mapping進UserProjectRelation裡面, 感恩
            ProjectDao.UpdateProject(new ProjectEntity() { Id = projectId }, project.ObjectConvert<ProjectEntity>());

            // 更新 UserProjectRelation
            ProjectDao.DeleteRelationByProjectId(projectId);    // 刪除所有關聯

            // 新增 UserProjectRelation 的關係
            // manager
            ProjectDao.CreateUserProjectRelation(new UserProjectRelationEntity()
            {
                ProjectId = projectId,
                UserId = project.managerId,
                ProjectCharactorId = 1  // Manager
            });
            // developer
            foreach (var developerId in project.developersId)
            {
                ProjectDao.CreateUserProjectRelation(new UserProjectRelationEntity()
                {
                    ProjectId = projectId,
                    UserId = developerId,
                    ProjectCharactorId = 2
                });
            }
            // general
            foreach (var generalId in project.generalsId)
            {
                ProjectDao.CreateUserProjectRelation(new UserProjectRelationEntity()
                {
                    ProjectId = projectId,
                    UserId = generalId,
                    ProjectCharactorId = 3
                });
            }

            return projectId;
        }
        
    }
}
