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
    }
}
