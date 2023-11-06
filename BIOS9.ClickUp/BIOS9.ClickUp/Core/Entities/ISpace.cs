namespace BIOS9.ClickUp.Core.Entities;

public interface ISpace
{
    string Id { get; }
    string Name { get; }

    Task<IReadOnlyCollection<IFolder>> GetFoldersAsync(bool archived = false);
    Task<IReadOnlyCollection<IList>> GetListsAsync(bool archived = false);
}