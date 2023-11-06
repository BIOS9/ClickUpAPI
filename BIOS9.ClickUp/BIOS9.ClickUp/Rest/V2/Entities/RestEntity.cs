using BIOS9.ClickUp.Core.Entities;

namespace BIOS9.ClickUp.Rest.V2.Entities;

public abstract class RestEntity : IEntity
{
    public string Id { get; }
    internal ClickUpClient ClickUp { get; }

    protected RestEntity(string id, ClickUpClient clickUp)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        ClickUp = clickUp ?? throw new ArgumentNullException(nameof(clickUp));
    }
}