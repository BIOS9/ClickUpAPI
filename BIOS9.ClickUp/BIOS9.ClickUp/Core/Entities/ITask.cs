namespace BIOS9.ClickUp.Core.Entities;

public interface ITask : IEntity
{
    string Name { get; }
    string Description { get; }
    IStatus Status { get; }
    Task DeleteAsync();
    Task ModifyAsync(Action<TaskProperties> propertiesFunc);
}