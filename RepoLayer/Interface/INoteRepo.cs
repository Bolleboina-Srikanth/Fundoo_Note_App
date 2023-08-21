using CommonLayer.Models;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface INoteRepo
    {
        public NoteEntity CreateNote(NoteMakingModel model, long userId);
    }
}
