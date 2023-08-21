using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBusiness _noteBusiness;
        public NoteController(INoteBusiness noteBusiness)
        {
            this._noteBusiness = noteBusiness;
        }
        [Authorize]
        [HttpPost]
        [Route("Notemaking")]
        public IActionResult CreateNote(NoteMakingModel model)
        {
            // Get the authenticated user's userId from the claims
            var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            if (userIdClaim == null)
            {

                return Unauthorized(); // User not authenticated properly
            }

            int userId = int.Parse(userIdClaim);

            // Associate the userId with the note
            //  NoteEntity.UserId = userId;

            var result = _noteBusiness.CreateNote(model, userId);
            if (result != null)
            {
                return this.Ok(new { Success = true, Message = "Note created successful", Data = result });

            }
            else
            {
                return this.BadRequest(new { Success = false, Message = "Note created unsuccessful", Data = result });

            }
        }
    }
}
