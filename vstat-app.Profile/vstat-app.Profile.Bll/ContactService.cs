using Microsoft.EntityFrameworkCore;

using vstat_app.Profile.Bll.DbConfiguration;
using vstat_app.Profile.Contracts.Commands.ContactCommands;
using vstat_app.Profile.Contracts.DTO;
using vstat_app.Profile.Contracts.Interfaces;

namespace vstat_app.Profile.Bll;

public class ContactService : IContactService
{
    private readonly ProfileDbContext _dbContext;

    public ContactService(ProfileDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContactDto> CreateContact(
        string id,
        ContactCreateCommand contact,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _dbContext.Contacts.AddAsync(new(id, contact.ProfileId, contact.Email), cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(id, contact.ProfileId, contact.Email);
    }

    public async Task<ContactDto> GetContactById(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var contact = await _dbContext.Contacts.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (contact == null)
        {
            throw new ArgumentException("Контакт не найден.");
        }

        return new(id, contact.ProfileId, contact.Email);
    }

    public async Task<ICollection<ContactDto>> GetAllContacts(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var contacts = await _dbContext.Contacts.AsNoTracking().ToListAsync(cancellationToken);

        var contactsDto = contacts.Select(c => new ContactDto(c.Id, c.ProfileId, c.Email)).ToList();

        return contactsDto;
    }

    public async Task<ContactDto> UpdateContact(
        string id,
        ContactUpdateCommand contact,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var existingContact = _dbContext.Contacts.FirstOrDefault(c => c.Id == id);

        if (existingContact == null)
        {
            throw new ArgumentException("Контакт не найден.");
        }

        existingContact.ProfileId = contact.ProfileId;
        existingContact.Email = contact.Email;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(id, contact.ProfileId, contact.Email);
    }

    public async Task DeleteContact(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var contactToDelete = _dbContext.Contacts.FirstOrDefault(c => c.Id == id);

        if (contactToDelete == null)
        {
            throw new ArgumentException("Контакт не найден.");
        }

        _dbContext.Contacts.Remove(contactToDelete);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}