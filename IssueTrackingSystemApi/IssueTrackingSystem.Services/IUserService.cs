using System;
using System.Collections.Generic;
using System.Text;
using IssueTrackingSystemApi.Models;

namespace IssueTrackingSystemApi.Services
{
    public interface IUserService
    {
        /// <summary>
        /// 依照Id取得User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetUserById(int id);

        /// <summary>
        /// 更新User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int UpdateUsers(int id);

        /// <summary>
        /// 創建User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        int CreateUser(User user);
    }
}
