using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            this._userBusiness = userBusiness;
        }
        [HttpPost]
        [Route("Register")]
        public ActionResult Registeration(UserRegisterationModel model)
        {
            var result = _userBusiness.UserRegisteration(model);
            if(result != null)
            {
                return this.Ok(new{ success=true, Message="Registeration successfull", date=result });
            }
            else
            {
                return this.BadRequest(new { Success = false, Message = "Registeration unsuccessfull", Data = result });
            }

        }

        [HttpPost]
        [Route("Login")]
        public ActionResult login(UserLoginModel model)
        {
            try
            {
          
                var result = _userBusiness.UserLogin(model);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "login successfull", date = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, Message = "login failed", Data = result });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
