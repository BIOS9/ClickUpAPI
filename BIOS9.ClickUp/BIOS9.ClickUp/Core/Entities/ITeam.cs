namespace BIOS9.ClickUp.Core.Entities;

public interface ITeam : IEntity
{
    string Name { get; }

    Task<IReadOnlyCollection<ISpace>> GetSpacesAsync(bool archived = false);
    Task<ISpace> CreateSpaceAsync(Action<SpaceProperties> propertiesFunc);
}