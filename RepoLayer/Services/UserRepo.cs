using CommonLayer.Models;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Services
{
    public class UserRepo:IUserRepo
    {
        private readonly FundooContext _fundooContext;

        public UserRepo(FundooContext fundooContext)
        {
            this._fundooContext = fundooContext;
        }
        public UserEntity UserRegisteration(UserRegisterationModel model)
        {
            try
            {
                UserEntity userTable = new UserEntity();
                userTable.FirstName = model.FirstName;
                userTable.LastName = model.LastName;
                userTable.DateOfBirth = model.DateOfBirth;
                userTable.Email = model.Email;
                userTable.Password = model.Password;

                _fundooContext.User.Update(userTable);
                _fundooContext.SaveChanges();
                if(userTable !=null)
                {
                    return userTable;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
