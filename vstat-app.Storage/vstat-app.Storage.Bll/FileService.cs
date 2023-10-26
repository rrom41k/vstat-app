using Microsoft.EntityFrameworkCore;
using vstat_app.Storage.Bll.DbConfiguration;
using vstat_app.Storage.Contracts.Commands.FileCommands;
using vstat_app.Storage.Contracts.Interfaces;
using vstat_app.Storage.Contracts.DTO;
using File = vstat_app.Storage.Contracts.Models.File;

namespace vstat_app.Storage.Bll;

public class FileService : IFileService
{
    private readonly StorageDbContext _dbContext;

    public FileService(StorageDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<FileDTO> CreateFile(string id, FileCreateCommand file)
    {
        File newFile =  new(id, 
            file.OwnerId, 
            file.Name, 
            file.Extension, 
            file.BucketId, 
            file.StorageId);
        _dbContext.Files.Add(newFile);
        await _dbContext.SaveChangesAsync();

        return MapToDTO(newFile);
    }

    public async Task<FileDTO> GetFileById(string id)
    {
        var existingFile = await _dbContext.Files.AsNoTracking().FirstOrDefaultAsync(existingFile => existingFile.Id == id) 
            ?? throw new ArgumentException("Файл не найден.");
        return MapToDTO(existingFile);
    }

    public async Task<ICollection<FileDTO>> GetAllFiles()
    {
        return await _dbContext.Files.AsNoTracking()
            .Select(file => MapToDTO(file))
            .ToListAsync();
    }

    public async Task<FileDTO> UpdateFile(string id, FileUpdateCommand updateFileData)
    {
        var fileToUpdate = await _dbContext.Files.AsNoTracking().FirstOrDefaultAsync(fileToUpdate => fileToUpdate.Id == id);

        if (fileToUpdate == null)
            throw new ArgumentException("Файл не найден.");

        UpdateFile(fileToUpdate, updateFileData);
        await _dbContext.SaveChangesAsync();

        return MapToDTO(fileToUpdate);
    }

    public async Task DeleteFile(string id)
    {
        var existingFile = await _dbContext.Files.AsNoTracking().FirstOrDefaultAsync(existingFile => existingFile.Id == id);

        if (existingFile == null)
            throw new ArgumentException("Файл не найден.");

        _dbContext.Files.Remove(existingFile);
        await _dbContext.SaveChangesAsync();
    }
    
    private static FileDTO MapToDTO(File file) => new(file.Id, 
        file.OwnerId, 
        file.Name,
        file.Extension,
        file.BucketId,
        file.StorageId);
    
    private void UpdateFile(File file, FileUpdateCommand updateFileData)
    {
        file.OwnerId = updateFileData.OwnerId;
        file.Name = updateFileData.Name;
        file.Extension = updateFileData.Extension;
        file.BucketId = updateFileData.BucketId;
        file.StorageId = updateFileData.StorageId;
    }
}