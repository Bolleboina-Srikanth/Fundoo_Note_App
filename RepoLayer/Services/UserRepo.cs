using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepoLayer.Services
{
    public class UserRepo:IUserRepo
    {
        private readonly FundooContext _fundooContext;
        private readonly IConfiguration _configuration;

        public UserRepo(FundooContext fundooContext, IConfiguration configuration)
        {
            this._fundooContext = fundooContext;
            this._configuration = configuration;
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
                if (userTable != null)
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
            public string GenerateJwtToken(string Email, long UserId)
            {
                var claims = new List<Claim>
                {
                new Claim("UserId", UserId.ToString()),
                new Claim("Email" , Email)
                };
                // You can add more claims as needed, such as roles or custom claims.


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(_configuration["JwtSettings:Issuer"], _configuration["JwtSettings:Audience"], claims, DateTime.Now, DateTime.Now.AddHours(12), creds);


                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        
        public string UserLogin(UserLoginModel model)
        {
            try
            {
                var Credientials = _fundooContext.User.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
                if (Credientials != null)
                {
                    var token = GenerateJwtToken(Credientials.Email, Credientials.UserId);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            try
            {
                var emailValidity = _fundooContext.User.FirstOrDefault(u => u.Email == forgotPasswordModel.Email);
                if (emailValidity != null)
                {
                    var token = GenerateJwtToken(emailValidity.Email, emailValidity.UserId);
                    MSMQ msmq = new MSMQ();
                    msmq.sendData2Queue(token);
                    return token;
                }


                return null;
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
                if (NewPassword == ConfirmPassword)
                {
                    var EmailValidity = _fundooContext.User.FirstOrDefault(u => u.Email == Email);
                    if (EmailValidity != null)
                    {
                        EmailValidity.Password = ConfirmPassword;
                        _fundooContext.User.Update(EmailValidity);
                        _fundooContext.SaveChanges();
                        return true;
                    }

                }
                return false;

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
