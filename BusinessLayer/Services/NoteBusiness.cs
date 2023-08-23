using BusinessLayer.Interface;
using CommonLayer.Models;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NoteBusiness : INoteBusiness
    {
        private readonly INoteRepo _noterepo;
        public NoteBusiness(INoteRepo noterepo)
        {
            this._noterepo = noterepo;
        }
        public NoteEntity CreateNote(NoteMakingModel model, long userId)
        {
            try
            {
                return _noterepo.CreateNote(model, userId);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public NoteEntity UpdateNote(string Title, string TakeNote, long NoteId, long userId)
        {
            try
            {
                return _noterepo.UpdateNote(Title,TakeNote,NoteId, userId);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public bool DeleteNoteById(long noteId, long userId)
        {
            try
            {
                return _noterepo.DeleteNoteById( noteId, userId);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public List<NoteEntity> GetNotesForUser(int userId)
        {
            try
            {
                return _noterepo.GetNotesForUser(userId);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public string UpdateColorNoteById(long NoteId, long UserId, string colour)
        {
            try
            {
                return _noterepo.UpdateColorNoteById(NoteId, UserId, colour);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ArchiveNoteById(long NoteId, long UserId)
        {
            try
            {
                return _noterepo.ArchiveNoteById(NoteId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool PinNoteById(long NoteId, long UserId)
        {
            try
            {
                return _noterepo.PinNoteById(NoteId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool TrashNoteById(long NoteId, long UserId)
        {
            try
            {
                return _noterepo.TrashNoteById(NoteId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
