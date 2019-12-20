using System;
using System.Collections.Generic;
using System.Text;
using IssueTrackingSystemApi.Models;
using IssueTrackingSystemApi.Models.View;

namespace IssueTrackingSystemApi.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {

        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateUsers(User user)
        {
            throw new NotImplementedException();
        }

        public int CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool ValidateUser(LoginInfo loginInfo)
        {
            //TODO: 驗證待寫
            return loginInfo.account == "chris" && loginInfo.password == "1234";
        }
    }
}
