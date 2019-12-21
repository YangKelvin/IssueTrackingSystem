﻿using System;
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

        int CreateUser(User user);

        bool ValidateUser(LoginInfo loginInfo);

        User GetUserByAccount(string account);

        List<User> GetAllUsers();
    }
}
