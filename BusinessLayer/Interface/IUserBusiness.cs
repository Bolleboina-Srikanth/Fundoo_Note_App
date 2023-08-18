using CommonLayer.Models;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {
        public UserEntity UserRegisteration(UserRegisterationModel model);
        public string UserLogin(UserLoginModel model);
    }
}
