using System;
using System.Collections.Generic;
using System.Text;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Models.View;

namespace IssueTrackingSystemApi.Services
{
    public interface IUserService
    {
        User GetUserById(int id);

        int UpdateUser(User user);

        int UpdateUserByAccount(User user);

        int CreateUser(User user);

        int DeleteUser(int id);

        int? ValidateUser(LoginInfo loginInfo);

        User GetUserByAccount(string account);

        List<User> GetAllUsers();
    }
}
