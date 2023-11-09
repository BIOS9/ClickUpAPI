namespace BIOS9.ClickUp.Core.Entities;

public interface IFolder : IEntity
{
    string Name { get; }
    
    Task DeleteAsync();
    Task ModifyAsync(Action<FolderProperties> propertiesFunc);
    Task<IReadOnlyCollection<IList>> GetListsAsync(bool archived = false);
    Task<IList> CreateListAsync(Action<ListProperties> propertiesFunc);
}