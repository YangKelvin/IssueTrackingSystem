using System;
using System.Collections.Generic;
using System.Text;
using IssueTrackingSystemApi.Models.Entity;
using IssueTrackingSystemApi.CommonTools;

namespace IssueTrackingSystemApi.Dao
{
    public class UserDao : IUserDao
    {
        public int CreatUser(UserEntity user)
        {
            return SqlHelper.Insert(user);
        }

        public int DeleteUser(int id)
        {
            return SqlHelper.Delete(new UserEntity() { Id = id });
        }

        public int UpdateUser(UserEntity conition, UserEntity user)
        {
            return SqlHelper.Update(conition, user);
        }

        public IEnumerable<UserEntity> Query(UserEntity conition = null)
        {
            return SqlHelper.Select(conition);
        }
    }
}
