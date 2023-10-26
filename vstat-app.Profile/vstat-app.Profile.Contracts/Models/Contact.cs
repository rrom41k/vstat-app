namespace vstat_app.Profile.Contracts.Models;

public class Contact
{
    public Contact(string id, string profileId, string email)
    {
        Id = id;
        ProfileId = profileId;
        Email = email;
    }

    public string Id { get; set; }
    public string ProfileId { get; set; }
    public string Email { get; set; }
    public Profile Profile { get; set; }
}