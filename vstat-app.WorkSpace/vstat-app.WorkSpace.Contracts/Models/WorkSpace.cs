namespace vstat_app.WorkSpace.Contracts.Models;

public class WorkSpace
{
    public WorkSpace(string id, string ownerId, string email,
        string name,
        string title,
        string createdAt)
    {
        Id = id;
        OwnerId = ownerId;
        Email = email;
        Name = name;
        Title = title;
        CreatedAt = createdAt;
    }

    public string Id { get; set; }
    public string OwnerId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string CreatedAt { get; set; }
    public ICollection<WorkSpaceFile> WorkSpaceFiles { get; }
}