using BIOS9.ClickUp.Core.Entities;
using BIOS9.ClickUp.Core.Util;
using RestSharp;

namespace BIOS9.ClickUp.Rest.V2.Entities;

public class ClickUpTask : RestEntity, ITask
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public ClickUpTask(string id, ClickUpClient clickUp) : base(id, clickUp)
    {
    }
    
    public ClickUpTask(Models.Common.Task model, ClickUpClient clickUp) : base(model.Id.Value, clickUp)
    {
        Update(model);
    }
    
    public async Task DeleteAsync()
    {
        await ClickUp.RequestAsync(
            Method.Delete, 
            $"task/{Id}");
    }

    public async Task ModifyAsync(Action<TaskProperties> propertiesFunc)
    {
        var properties = new TaskProperties();
        propertiesFunc(properties);
        var body = new Models.Common.Task(
            Optional<string>.Unspecified, 
            properties.Name,
            properties.Description);
        var response = await ClickUp.RequestAsync<Models.Common.Task>(
            Method.Put, 
            $"task/{Id}",
            payload: body);
        Update(response);
    }
    
    public override async Task UpdateAsync()
    {
        var response = await ClickUp.RequestAsync<Models.Common.Task>(
            Method.Get, 
            $"task/{Id}");
        Update(response);
    }
    
    internal ClickUpTask Update(Models.Common.Task model)
    {
        Name = model.Name.Value;
        Description = model.Description.Value;
        return this;
    }
}