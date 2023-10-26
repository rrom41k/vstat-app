namespace vstat_app.Storage.Contracts.Models;

public class Storage
{
    public Storage(string Id, string UserId)
    {
        this.Id = Id;
        this.UserId = UserId;
        Files = new HashSet<File>();
    }

    public string Id { get; set; }
    public string UserId { get; set; }
    public ICollection<File> Files { get; }
}