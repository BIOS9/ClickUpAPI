namespace BIOS9.ClickUp.Core.Entities;

public interface IList
{
    string Id { get; }
    string Name { get; }
    Task DeleteAsync();
    Task ModifyAsync(Action<ListProperties> propertiesFunc);
    Task<IReadOnlyCollection<ITask>> GetTasksAsync(bool includeSubtasks = false, bool includeClosed = false, bool archived = false);
}