using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface INoteRepo
    {
        public NoteEntity CreateNote(NoteMakingModel model, long userId);
        public NoteEntity UpdateNote(string Title, string TakeNote, long NoteId, long userId);
        public bool DeleteNoteById(long noteId, long userId);
        public List<NoteEntity> GetNotesForUser(int userId);
        public string UpdateColorNoteById(long NoteId, long UserId, string colour);
        public bool ArchiveNoteById(long NoteId, long UserId);
        public bool PinNoteById(long NoteId, long UserId);
        public bool TrashNoteById(long NoteId, long UserId);
        public Task<Tuple<int, string>> UpdateNoteImage(long userId, long noteId, IFormFile imageFile);
    }
}
