namespace BIOS9.ClickUp.Core.Entities;

public interface IEntity
{
    string Id { get; }
    Task UpdateAsync();
}