using System;
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
        private readonly IIssueDao _issueDao;
        private readonly IUserDao _userDao;

        public IssueService(IIssueDao issueDao, IUserDao userDao)
        {
            _issueDao = issueDao;
            _userDao = userDao;
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

            return _issueDao.CreatIssue(issueEntity);
        }

        /// <summary>
        /// Get a specific issue
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Issue GetIssueById(int id)
        {
            IssueEntity issueEntity = _issueDao.Query(new IssueEntity() { Id = id }).FirstOrDefault();
            Issue issue = issueEntity.ObjectConvert<Issue>();
            issue.CreateUser = _userDao.Query(new UserEntity() { Id = issueEntity.CreateUesr }).FirstOrDefault().ObjectConvert<User>();
            issue.ModifyUser = _userDao.Query(new UserEntity() { Id = issueEntity.ModifyUser }).FirstOrDefault().ObjectConvert<User>();

            return issue;
        }

        /// <summary>
        /// 透過問題號碼取得問題
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public Issue GetIssueByNumber(string number)
        {
            IssueEntity issueEntity = _issueDao.Query(new IssueEntity() { Number = number }).FirstOrDefault();
            Issue issue = issueEntity.ObjectConvert<Issue>();
            issue.CreateUser = _userDao.Query(new UserEntity() { Id = issueEntity.CreateUesr }).FirstOrDefault().ObjectConvert<User>();
            issue.ModifyUser = _userDao.Query(new UserEntity() { Id = issueEntity.ModifyUser }).FirstOrDefault().ObjectConvert<User>();

            return issue;
        }

        /// <summary>
        /// Get all issues
        /// </summary>
        /// <returns></returns>
        public List<Issue> GetAllIssues()
        {
            List<IssueEntity> issueEntitys = _issueDao.Query().ToList();
            List<UserEntity> userEntitys = _userDao.Query().ToList();

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
            return _issueDao.UpdateIssue(new IssueEntity() { Id = issue.Id }, issue.ObjectConvert<IssueEntity>(i =>
            {
                i.CreateUesr = issue.CreateUser?.Id;
                i.ModifyUser = issue.ModifyUser?.Id;
            }));
        }
    }
}
