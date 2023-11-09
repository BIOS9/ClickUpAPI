namespace BIOS9.ClickUp.Core.Entities;

public interface ISpace : IEntity
{
    string Name { get; }

    Task DeleteAsync();
    Task ModifyAsync(Action<SpaceProperties> propertiesFunc);
    Task<IReadOnlyCollection<IFolder>> GetFoldersAsync(bool archived = false);
    Task<IFolder> CreateFolderAsync(Action<FolderProperties> propertiesFunc);
    Task<IReadOnlyCollection<IList>> GetListsAsync(bool archived = false);
    Task<IList> CreateListAsync(Action<ListProperties> propertiesFunc);
    
}