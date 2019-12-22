using System;
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
    public class UserService : IUserService
    {
        private IUserDao UserDao { get => new UserDao(); }

        public UserService()
        {

        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByAccount(string account)
        {
            UserEntity userEntity = UserDao.Query(new UserEntity() { Account = account }).FirstOrDefault();
            User user = userEntity.ObjectConvert<User>();

            return user;
        }

        public int UpdateUser(User user)
        {
            return UserDao.UpdateUser(new UserEntity() { Id = user.Id }, user.ObjectConvert<UserEntity>(i =>
            {
                i.CharactorId = Convert.ToInt32(user.Charactor);
            }));
            throw new NotImplementedException();
        }

        /// <summary>
        /// 新增 User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int CreateUser(User user)
        {
            UserEntity userEntity = user.ObjectConvert<UserEntity>();
            userEntity.CharactorId = Convert.ToInt32(user.Charactor);
            return UserDao.CreatUser(userEntity);
        }

        public bool ValidateUser(LoginInfo loginInfo)
        {
            //TODO: 驗證待寫
            return loginInfo.account == "chris" && loginInfo.password == "1234";
        }

        public List<User> GetAllUsers()
        {
            List<UserEntity> userEntitys = UserDao.Query().ToList();

            return userEntitys.Select(i => i.ObjectConvert<User>()).ToList();
            
        }

        public bool IsUserExist(User user)
        {
            UserEntity userEntity = UserDao.Query(new UserEntity() { Account = user.Account, Password = user.Password }).FirstOrDefault();
            
            return userEntity != null;
        }
    }
}
