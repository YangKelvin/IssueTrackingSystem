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

        int UpdateUsers(User user);

        int CreateUser(User user);

        bool ValidateUser(LoginInfo loginInfo);
    }
}
