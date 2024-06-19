using BasicCrud.Domain.Entity;

namespace BasicCrud.Domain.Models;

public class User
{
    public int Id { get; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Note> Notes { get; set; }
}

