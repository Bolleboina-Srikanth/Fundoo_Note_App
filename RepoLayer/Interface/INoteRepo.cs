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
        public NoteEntity UpdateNote(string Title, string TakeNote, long NoteId, long userId);
        public bool DeleteNoteById(long noteId, long userId);
        public List<NoteEntity> GetNotesForUser(int userId);
    }
}
