namespace BIOS9.ClickUp.Core.Entities;

public interface ISpace
{
    string Id { get; }
    string Name { get; }

    Task DeleteAsync();
    Task ModifyAsync(Action<SpaceProperties> propertiesFunc);
    Task<IReadOnlyCollection<IFolder>> GetFoldersAsync(bool archived = false);
    Task<IFolder> CreateFolderAsync(string name);
    Task<IReadOnlyCollection<IList>> GetListsAsync(bool archived = false);
    Task<IList> CreateListAsync(string name);
    
}