using BIOS9.ClickUp.Core.Entities;

namespace BIOS9.ClickUp.Rest.V2.Entities;

public class ClickUpTask : RestEntity, ITask
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    
    public ClickUpTask(string id, ClickUpClient clickUp) : base(id, clickUp)
    {
    }
    
    public ClickUpTask(Models.Common.Task model, ClickUpClient clickUp) : base(model.Id, clickUp)
    {
        Update(model);
    }
    
    internal ClickUpTask Update(Models.Common.Task model)
    {
        Name = model.Name;
        Description = model.Description;
        return this;
    }
}