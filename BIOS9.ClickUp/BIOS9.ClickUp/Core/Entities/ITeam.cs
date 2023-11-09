namespace BIOS9.ClickUp.Core.Entities;

public interface ITeam : IEntity
{
    string Name { get; }
    string Color { get; }
    string Avatar { get; }

    Task<IReadOnlyCollection<ISpace>> GetSpacesAsync(bool archived = false);
    Task<ISpace> CreateSpaceAsync(Action<SpaceProperties> propertiesFunc);
}