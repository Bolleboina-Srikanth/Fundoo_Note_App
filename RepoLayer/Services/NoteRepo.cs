using CommonLayer.Models;
using Microsoft.Extensions.Configuration.UserSecrets;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Services
{
    public class NoteRepo:INoteRepo
    {
        private readonly FundooContext _fundooContext;

        public NoteRepo(FundooContext fundooContext)
        {
            this._fundooContext = fundooContext;
        }
        public NoteEntity CreateNote(NoteMakingModel model, long userId)
        {
            
            try
            {
                    NoteEntity newNote = new NoteEntity();
                    newNote.UserId = userId;
                    newNote.Title = model.Title;
                    newNote.TakeNote = model.TakeNote;

                    _fundooContext.Notes.Add(newNote);
                    _fundooContext.SaveChanges();
                    return newNote;

                if(newNote !=null)
                {
                    return newNote;
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
        public NoteEntity UpdateNote(string Title, string TakeNote, long NoteId,long userId)
        {
            try
            {
                var existingNote = _fundooContext.Notes.FirstOrDefault(x=>x.UserId == userId && x.NoteId==NoteId);

                if (existingNote == null)
                    return null;

                existingNote.Title = Title + existingNote.Title;
                existingNote.TakeNote = TakeNote + existingNote.TakeNote;
                // Update other columns as needed
                _fundooContext.Notes.Update(existingNote);
                _fundooContext.SaveChanges();

                return existingNote;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteNoteById(long noteId,long userId)
        {
            var existingNote = _fundooContext.Notes.FirstOrDefault(x=>x.NoteId == noteId && x.UserId == userId);

            if (existingNote == null)
                return false;
            else
            {
                _fundooContext.Notes.Remove(existingNote);
                _fundooContext.SaveChanges();

                return true;
            }
        }
        public List<NoteEntity> GetNotesForUser(int userId)
        {
            return _fundooContext.Notes.Where(note => note.UserId == userId).ToList();
        }
        public string UpdateColorNoteById(long NoteId, long UserId, string colour)
        {
            var result = _fundooContext.Notes.FirstOrDefault(x => x.NoteId == NoteId && x.UserId == UserId);

            if (result == null)
            {
                return null;
            }
            else
            {
                result.Colour = colour;
                _fundooContext.Notes.Update(result);
                _fundooContext.SaveChanges();

                return result.Colour;
            }
        }
        public bool ArchiveNoteById(long NoteId, long UserId)
        {
            var result = _fundooContext.Notes.FirstOrDefault(x => x.NoteId == NoteId && x.UserId == UserId);

            if (result == null)
            {
                return false;
            }
            else
            {
                result.IsArchive = true;
                _fundooContext.Notes.Update(result);
                _fundooContext.SaveChanges();

                return true;
            }
        }
        public bool PinNoteById(long NoteId, long UserId)
        {
            var result = _fundooContext.Notes.FirstOrDefault(x => x.NoteId == NoteId && x.UserId == UserId);

            if (result == null)
            {
                return false;
            }
            else
            {
                result.IsPin = true;
                _fundooContext.Notes.Update(result);
                _fundooContext.SaveChanges();

                return true;
            }
        }

        public bool TrashNoteById(long NoteId, long UserId)
        {
            var result = _fundooContext.Notes.FirstOrDefault(x => x.NoteId == NoteId && x.UserId == UserId);

            if (result == null)
            {
                return false;
            }
            else
            {
                result.IsTrash = true;
                _fundooContext.Notes.Update(result);
                _fundooContext.SaveChanges();

                return true;
            }
        }
    }
}
