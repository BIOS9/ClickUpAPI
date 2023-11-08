namespace BIOS9.ClickUp.Core.Entities;

public interface ITeam
{
    string Id { get; }
    string Name { get; }

    Task<IReadOnlyCollection<ISpace>> GetSpacesAsync(bool archived = false);
    Task<ISpace> CreateSpaceAsync(string name);
}