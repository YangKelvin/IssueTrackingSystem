using System.Collections.Generic;
using IssueTrackingSystemApi.Models.Entity;

namespace IssueTrackingSystemApi.Dao
{
    public interface IUserDao
    {
        int CreatUser(UserEntity user);
        int DeleteUser(int id);
        IEnumerable<UserEntity> Query(UserEntity conition = null);
        int UpdateUser(UserEntity conition, UserEntity user);
        IEnumerable<CharactorEntity> GetCharactor(CharactorEntity condition = null);
    }
}