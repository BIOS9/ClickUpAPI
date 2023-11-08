namespace BIOS9.ClickUp.Core.Entities;

public interface ITask
{
    string Id { get; }
    string Name { get; }
    string Description { get; }
    Task DeleteAsync();
    Task ModifyAsync(Action<TaskProperties> propertiesFunc);
}