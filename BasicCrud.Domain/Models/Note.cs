using BasicCrud.Domain.Models;

namespace BasicCrud.Domain.Entity;

public class Note
{
    public int Id { get; }
    //public int UserId { get; set; }
    //public User User { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
}

