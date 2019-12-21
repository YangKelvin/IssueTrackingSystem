﻿using System;
using System.Collections.Generic;
using System.Text;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Models.Entity;
using IssueTrackingSystemApi.CommonTools;
using IssueTrackingSystemApi.Dao;
using System.Linq;

namespace IssueTrackingSystemApi.Services
{
    public class IssueService : IIssueService
    {
        private IIssueDao IssueDao { get => new IssueDao(); }
        private IUserDao UserDao { get => new UserDao(); }

        public IssueService()
        {
        }

        /// <summary>
        /// Create issue
        /// </summary>
        /// <param name="issue"></param>
        /// <returns></returns>
        public int CreateIssue(Issue issue)
        {
            IssueEntity issueEntity = issue.ObjectConvert<IssueEntity>();
            issueEntity.CreateUesr = issue.CreateUser.Id;

            return IssueDao.CreatIssue(issueEntity);
        }

        /// <summary>
        /// Get a specific issue
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Issue GetIssueById(int id)
        {
            IssueEntity issueEntity = IssueDao.Query(new IssueEntity() { Id = id }).FirstOrDefault();
            Issue issue = issueEntity.ObjectConvert<Issue>();
            issue.CreateUser = UserDao.Query(new UserEntity() { Id = issueEntity.CreateUesr }).FirstOrDefault().ObjectConvert<User>();
            issue.ModifyUser = UserDao.Query(new UserEntity() { Id = issueEntity.ModifyUser }).FirstOrDefault().ObjectConvert<User>();

            return issue;
        }

        /// <summary>
        /// Get all issues
        /// </summary>
        /// <returns></returns>
        public List<Issue> GetAllIssues()
        {
            List<IssueEntity> issueEntitys = IssueDao.Query().ToList();
            List<UserEntity> userEntitys = UserDao.Query().ToList();

            return issueEntitys.Select(i => i.ObjectConvert<Issue>(iss =>
            {
                iss.CreateUser = userEntitys.Find(u => u.Id == i.CreateUesr).ObjectConvert<User>();
                iss.ModifyUser = userEntitys.Find(u => u.Id == i.ModifyUser).ObjectConvert<User>();
            })).ToList();
        }

        /// <summary>
        /// Create issue
        /// </summary>
        /// <param name="issue"></param>
        /// <returns></returns>
        public int UpdateIssue(Issue issue)
        {
            return IssueDao.UpdateIssue(new IssueEntity() { Id = issue.Id }, issue.ObjectConvert<IssueEntity>(i =>
            {
                i.CreateUesr = issue.CreateUser?.Id;
                i.ModifyUser = issue.ModifyUser?.Id;
            }));
        }
    }
}
