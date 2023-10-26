namespace vstat_app.Profile.Contracts.Models;

public class Profile
{
    public Profile(
        string id,
        string name,
        string surname,
        string middleName,
        string email,
        string phoneNumber)
    {
        Id = id;
        Name = name;
        Surname = surname;
        MiddleName = middleName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Contact> Contacts { get; set; }
}