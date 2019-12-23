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
        private readonly IUserDao _userDao;

        public UserService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public User GetUserById(int id)
        {
            UserEntity userEntity = _userDao.Query(new UserEntity() { Id = id }).FirstOrDefault();
            User user = userEntity.ObjectConvert<User>();

            return user;
        }

        public User GetUserByAccount(string account)
        {
            UserEntity userEntity = _userDao.Query(new UserEntity() { Account = account }).FirstOrDefault();
            User user = userEntity.ObjectConvert<User>();

            return user;
        }

        public int UpdateUser(User user)
        {
            return _userDao.UpdateUser(new UserEntity() { Id = user.Id }, user.ObjectConvert<UserEntity>());
        }

        public int UpdateUserByAccount(User user)
        {
            return _userDao.UpdateUser(new UserEntity() { Account = user.Account, Password=user.Password }, user.ObjectConvert<UserEntity>());
        }

        public int CreateUser(User user)
        {
            UserEntity userEntity = user.ObjectConvert<UserEntity>();
            return _userDao.CreatUser(userEntity);
        }

        public bool ValidateUser(LoginInfo loginInfo)
        {
            //TODO: 驗證待寫
            return loginInfo.account == "chris" && loginInfo.password == "1234";
        }

        public List<User> GetAllUsers()
        {
            List<UserEntity> userEntitys = _userDao.Query().ToList();

            return userEntitys.Select(i => i.ObjectConvert<User>()).ToList();
            
        }
    }
}
