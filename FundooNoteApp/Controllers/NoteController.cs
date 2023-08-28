using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RepoLayer.Context;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBusiness _noteBusiness;
        private readonly IDistributedCache _distributedCache;
        private readonly FundooContext _fundooContext;
        public NoteController(INoteBusiness noteBusiness, FundooContext fundooContext, IConfiguration configuration,IDistributedCache distributedCache)
        {
            this._noteBusiness = noteBusiness;
            this._fundooContext= fundooContext;
            this._distributedCache= distributedCache;
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
        [Authorize]
        [HttpPatch]
        [Route("UpdateNote")]
        public IActionResult UpdateNote(string Title, string TakeNote, long NoteId)
        {
             var UserIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
             int userId = int.Parse(UserIdClaim);
            var result = _noteBusiness.UpdateNote(Title, TakeNote, NoteId, userId);
            try
            {

                if (result != null)
                {
                    return this.Ok(new { Success = true, Message = "Note updated  successful", Data = result });

                }
                else
                {
                    return this.BadRequest(new { Success = false, Message = "Note updated unsuccessful", Data = result });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("DeleteNote")]
        public IActionResult DeleteNote(long noteId)
        {
            var UserIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            // var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            int userId = int.Parse(UserIdClaim);
            var result = _noteBusiness.DeleteNoteById(noteId, userId);
            try
            {

                if (result != null)
                {
                    return this.Ok(new { Success = true, Message = "Note deleted  successful", Data = result });

                }
                else
                {
                    return this.BadRequest(new { Success = false, Message = "Note id not found", Data = result });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetAllNotes")]
        public IActionResult GetAll()
        {
            var UserIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            // var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            int userId = int.Parse(UserIdClaim);
            var result = _noteBusiness.GetNotesForUser(userId);
            if (result != null)
            {
                return this.Ok(new { Success = true, Message = " Notes retrieve  successfully", Data = result });
            }
            else
            {
                return this.BadRequest(new { Success = false, Message = "id not found", Data = result });

            }
        }
        [Authorize]
        [HttpPatch]
        [Route("Colour")]
        public IActionResult updateColor(long NoteId, string colour)
        {
            var UserIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            int UserId = int.Parse(UserIdClaim);
            var result = _noteBusiness.UpdateColorNoteById(NoteId, UserId, colour);
            if (result != null)
            {
                return this.Ok(new { success = true, message = "Colour updated successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Note id not found", data = result });
            }

        }
        [Authorize]
        [HttpPut]
        [Route("Archive")]
        public IActionResult ArchiveNote(long NoteId)
        {
            var UserIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            int UserId = int.Parse(UserIdClaim);
            var result = _noteBusiness.ArchiveNoteById(NoteId, UserId);
            if (result == true)
            {
                return this.Ok(new { Success = true, Message = " archive  successfully", Data = result });

            }
            else
            {
                return this.BadRequest(new { Success = false, Message = " Note id not found", Data = result });

            }
        }
        [Authorize]
        [HttpPut]
        [Route("Pin")]
        public IActionResult PinNote(long NoteId)
        {
            var UserIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            int UserId = int.Parse(UserIdClaim);
            var result = _noteBusiness.PinNoteById(NoteId, UserId);
            if (result == true)
            {
                return this.Ok(new { Success = true, Message = " Pin  successfully", Data = result });

            }
            else
            {
                return this.BadRequest(new { Success = false, Message = " Note id not found", Data = result });

            }
        }
        [Authorize]
        [HttpPut]
        [Route("Trash")]
        public IActionResult trashNote(long NoteId)
        {
            var UserIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            int UserId = int.Parse(UserIdClaim);
            var result = _noteBusiness.TrashNoteById(NoteId, UserId);
            if (result == true)
            {
                return this.Ok(new { Success = true, Message = " trash  successfully", Data = result });

            }
            else
            {
                return this.BadRequest(new { Success = false, Message = "Note id not found", Data = result });

            }
        }
        [Authorize]
        [HttpPatch]
        [Route("uploadimage")]
        public async Task<IActionResult> UploadNoteImage(long noteId, IFormFile imageFile)
        {
            // Get the authenticated user's userId from the claims
            var UserIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            int userId = int.Parse(UserIdClaim);
            if (userId == null)
                return Unauthorized(); // User not authenticated properly
            Tuple<int, string> result = await _noteBusiness.UpdateNoteImage(userId, noteId, imageFile);
            if (result.Item1 == 1)
            {
                return this.Ok(new { success = true, Message = "image uploaded successfully", data = result });
            }
            else
            {
                return this.BadRequest(new { success = true, Message = "image uploaded unsuccessfully", data = result });
            }

        }
        [Authorize]
        [HttpGet("GetAllNoteByRedis")]
        public async Task<IActionResult> GetAllNoteswd()
        {
            var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
            int userId = int.Parse(userIdClaim);
            // Check if data is cached
            var cachedData = await _distributedCache.GetStringAsync($"Notes_{userId}");
            if (cachedData != null)
            {
                var notes = JsonConvert.DeserializeObject<List<NoteEntity>>(cachedData);
                return this.Ok(new { success = true, Message = "Notes retrieved from cache successfully", Data = notes });
            }
            else
            {
                var notesFromDb = _fundooContext.Notes.Where(note => note.UserId == userId).ToList();
                // Cache the data for 1 hour
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                };
                await _distributedCache.SetStringAsync($"Notes_{userId}", JsonConvert.SerializeObject(notesFromDb), cacheOptions);
                return this.Ok(new { success = true, Message = "Notes retrieved successfully", Data = notesFromDb });
            }
        }
    }
}
