namespace BIOS9.ClickUp.Core.Entities;

public interface IFolder
{
    string Id { get; }
    string Name { get; }
    
    Task DeleteAsync();
    Task ModifyAsync(Action<FolderProperties> propertiesFunc);
    Task<IReadOnlyCollection<IList>> GetListsAsync(bool archived = false);
}