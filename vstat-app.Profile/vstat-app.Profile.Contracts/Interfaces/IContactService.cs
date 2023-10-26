using vstat_app.Profile.Contracts.Commands.ContactCommands;
using vstat_app.Profile.Contracts.DTO;

namespace vstat_app.Profile.Contracts.Interfaces;

public interface IContactService
{
    Task<ContactDto> CreateContact(
        string id,
        ContactCreateCommand contact,
        CancellationToken cancellationToken = default);

    Task<ContactDto> GetContactById(string id, CancellationToken cancellationToken = default);
    Task<ICollection<ContactDto>> GetAllContacts(CancellationToken cancellationToken = default);

    Task<ContactDto> UpdateContact(
        string id,
        ContactUpdateCommand contact,
        CancellationToken cancellationToken = default);

    Task DeleteContact(string id, CancellationToken cancellationToken = default);
}