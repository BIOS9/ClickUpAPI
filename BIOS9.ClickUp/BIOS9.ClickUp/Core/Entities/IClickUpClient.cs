namespace BIOS9.ClickUp.Core.Entities;

public interface IClickUpClient
{
    Task<IReadOnlyCollection<ITeam>> GetAuthorizedTeamsAsync();
}