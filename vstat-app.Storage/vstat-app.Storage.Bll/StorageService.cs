using Microsoft.EntityFrameworkCore;
using vstat_app.Storage.Bll.DbConfiguration;
using vstat_app.Storage.Contracts.Commands.StorageCommands;
using vstat_app.Storage.Contracts.Interfaces;
using vstat_app.Storage.Contracts.DTO;
using ModelStorage = vstat_app.Storage.Contracts.Models.Storage;

namespace vstat_app.Storage.Bll;

public class StorageService : IStorageService
{
    private readonly StorageDbContext _dbContext;

    public StorageService(StorageDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StorageDTO> CreateStorage(string id, StorageCreateCommand storage)
    {
        ModelStorage newStorage = new(id, storage.UserId);
        _dbContext.Storages.Add(newStorage);
        await _dbContext.SaveChangesAsync();

        return MapToDTO(newStorage);
    }

    public async Task<StorageDTO> GetStorageById(string id)
    {
        var existingStorage = await _dbContext.Storages.AsNoTracking().FirstOrDefaultAsync(existingStorage => existingStorage.Id == id)
            ?? throw new ArgumentException("Хранилище не найдено.");
        return MapToDTO(existingStorage);
    }

    public async Task<ICollection<StorageDTO>> GetAllStorages()
    {
        return await _dbContext.Storages.AsNoTracking()
            .Select(storage => MapToDTO(storage))
            .ToListAsync();
    }

    public async Task<StorageDTO> UpdateStorage(string id, StorageUpdateCommand updateStorageData)
    {
        var storageToUpdate = await _dbContext.Storages.AsNoTracking().FirstOrDefaultAsync(storageToUpdate => storageToUpdate.Id == id);

        if (storageToUpdate == null)
            throw new ArgumentException("Хранилище не найдено.");

        UpdateCommand(storageToUpdate, updateStorageData);
        await _dbContext.SaveChangesAsync();

        return MapToDTO(storageToUpdate);
    }

    public async Task DeleteStorage(string id)
    {
        var storageToDelete = await _dbContext.Storages.AsNoTracking().FirstOrDefaultAsync(storageToDelete => storageToDelete.Id == id);

        if (storageToDelete == null)
            throw new ArgumentException("Хранилище не найдено");
        
        _dbContext.Storages.Remove(storageToDelete);
        await _dbContext.SaveChangesAsync();
    }
    
    private static StorageDTO MapToDTO(ModelStorage storage) => new(storage.Id, storage.UserId);

    private void UpdateCommand(ModelStorage originalStorage, StorageUpdateCommand updateStorageData)
    {
        originalStorage.UserId = updateStorageData.UserId;
    }
}