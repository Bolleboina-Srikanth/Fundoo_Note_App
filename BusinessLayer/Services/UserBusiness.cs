﻿using BusinessLayer.Interface;
using CommonLayer.Models;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBusiness:IUserBusiness
    {
        private readonly IUserRepo _userRepo;
        public UserBusiness(IUserRepo userRepo)
        {
            this._userRepo = userRepo;
        }
        public UserEntity UserRegisteration(UserRegisterationModel model)
        {
            try
            {
                return _userRepo.UserRegisteration(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string UserLogin(UserLoginModel model)
        {
            try
            {
                return _userRepo.UserLogin(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            try
            {
                return _userRepo.ForgotPassword(forgotPasswordModel);
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }
        public bool ResetPasword(string Email, string NewPassword, string ConfirmPassword)
        {
            try
            {
                return _userRepo.ResetPasword(Email, NewPassword, ConfirmPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
