using BasicCrud.Domain.Dto.Note;
using BasicCrud.Domain.Entity;
using BasicCrud.Domain.Enums;
using BasicCrud.Domain.Interfaces;
using BasicCrud.Domain.Interfaces.Result;
using BasicCrud.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace BasicCrud.Core.Services;

public class NoteService : INoteService
{
    private readonly IBaseRepository<Note> _repository;

    public NoteService(IBaseRepository<Note> repository)
    {
        _repository = repository;
    }

    public async Task<BaseResult<NoteDto>> CreateAsync(CreateNoteDto noteDto)
    {
        try
        {
            if (noteDto == null)
            {
                return new BaseResult<NoteDto>()
                {
                    ErrorCode = (int)ErrorCodes.NullNoteEntity,
                    ErrorMessage = "Note entity is null",
                };
            }

            var newNote = new Note()
            {
                Name = noteDto.Name,
                Description = noteDto.Description,
                CreatedDate = DateTime.UtcNow
            };

            await _repository.CreateAsync(newNote);

            return new BaseResult<NoteDto>()
            {
                Data = new NoteDto(newNote.Id, newNote.Name, newNote.Description, newNote.CreatedDate.ToString())
            };
        }
        catch (Exception ex)
        {
            return new BaseResult<NoteDto>()
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ex.Message,
            };
        }
    }

    public async Task<BaseResult<NoteDto>> DeleteAsync(DeleteNoteDto noteDto)
    {
        if (noteDto == null)
        {
            return new BaseResult<NoteDto>()
            {
                ErrorCode = (int)ErrorCodes.NullNoteEntity,
                ErrorMessage = "Entity is null"
            };
        }

        try
        {
            var note = await _repository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == noteDto.Id);

            if (note == null)
            {
                return new BaseResult<NoteDto>()
                {
                    ErrorCode = (int)ErrorCodes.NoteNotFound,
                    ErrorMessage = "Entity is not found"
                };
            }

            await _repository.DeleteAsync(note);

            return new BaseResult<NoteDto>()
            {
                Data = new NoteDto(note.Id, note.Name, note.Description, note.CreatedDate.ToString())
            };
        }
        catch (Exception ex)
        {
            return new BaseResult<NoteDto>()
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ex.Message,
            };
        }
    }

    public async Task<CollectionResult<NoteDto>> GetAsync()
    {
        NoteDto[] result;

        try
        {
            result = await _repository
                .GetAll()
                .Select(x => new NoteDto(x.Id, x.Name, x.Description, x.CreatedDate.ToString()))
                .ToArrayAsync();
        }
        catch (Exception ex)
        {
            return new CollectionResult<NoteDto>()
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ex.Message,
            };
        }

        if (result.Length == 0)
        {
            return new CollectionResult<NoteDto>()
            {
                ErrorCode = (int)ErrorCodes.NoteNotFound,
                ErrorMessage = "Note is not found",
            };
        }

        return new CollectionResult<NoteDto>()
        {
            Data = result,
            Count = result.Length
        };
    }

    public Task<BaseResult<NoteDto>> GetByIdAsync(int id)
    {
        NoteDto? result; 
        try
        {
            result = _repository
                .GetAll()
                .AsEnumerable()
                .Select(x => new NoteDto(x.Id, x.Name, x.Description, x.CreatedDate.ToString()))
                .FirstOrDefault(x => x.Id == id);
        }
        catch (Exception ex)
        {
            return Task.FromResult(new BaseResult<NoteDto>()
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ex.Message,
            });
        }

        if (result == null)
        {
            return Task.FromResult(new BaseResult<NoteDto>()
            {
                ErrorCode = (int)ErrorCodes.NoteNotFound,
                ErrorMessage = "Note is not found"
            });
        }

        return Task.FromResult(new BaseResult<NoteDto>()
        {
            Data = result
        });
    }

    public async Task<BaseResult<NoteDto>> UpdateAsync(UpdateNoteDto noteDto)
    {
        if (noteDto == null)
        {
            return new BaseResult<NoteDto>()
            {
                ErrorCode = (int)ErrorCodes.NullNoteEntity,
                ErrorMessage = "Entity is null"
            };
        }
        
        try
        {
            var note = await _repository
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == noteDto.Id);

            if (note == null)
            {
                return new BaseResult<NoteDto>()
                {
                    ErrorCode = (int)ErrorCodes.NoteNotFound,
                    ErrorMessage = "Entity is not found"
                };
            }

            note.Name = noteDto.Name;
            note.Description = noteDto.Description;

            await _repository.UpdateAsync(note);

            return new BaseResult<NoteDto>()
            {
                Data = new NoteDto(note.Id, note.Name, note.Description, note.CreatedDate.ToString())
            };
        }
        catch (Exception ex)
        {
            return new BaseResult<NoteDto>()
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ex.Message,
            };
        }
    }
}

