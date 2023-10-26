namespace vstat_app.WorkSpace.Contracts.DTO;
public record WorkSpaceDTO(
    string Id,
    string OwnerId,
    string Email,
    string Name,
    string Title,
    string CreatedAt);