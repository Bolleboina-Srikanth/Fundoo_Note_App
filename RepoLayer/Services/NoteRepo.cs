using CommonLayer.Models;
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
    }
}
