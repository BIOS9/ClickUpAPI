namespace BIOS9.ClickUp.Core.Entities;

public interface IList : IEntity
{
    string Name { get; }
    Task DeleteAsync();
    Task ModifyAsync(Action<ListProperties> propertiesFunc);
    Task<IReadOnlyCollection<ITask>> GetTasksAsync(bool includeSubtasks = false, bool includeClosed = false, bool archived = false);
    Task<ITask> CreateTaskAsync(string name);
}