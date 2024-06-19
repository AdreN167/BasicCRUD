using BasicCrud.Domain.Dto.Note;
using BasicCrud.Domain.Interfaces.Result;

namespace BasicCrud.Domain.Interfaces.Services;

public interface INoteService
{
    Task<CollectionResult<NoteDto>> GetAsync();
    Task<BaseResult<NoteDto>> GetByIdAsync(int id);
    Task<BaseResult<NoteDto>> CreateAsync(CreateNoteDto noteDto);
    Task<BaseResult<NoteDto>> UpdateAsync(UpdateNoteDto noteDto);
    Task<BaseResult<NoteDto>> DeleteAsync(DeleteNoteDto noteDto);
}

