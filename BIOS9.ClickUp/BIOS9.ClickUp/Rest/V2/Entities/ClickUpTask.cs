using BIOS9.ClickUp.Core.Entities;
using RestSharp;

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
    
    public async Task DeleteAsync()
    {
        var request = new RestRequest($"task/{Id}");
        var response = await ClickUp.GetRestClient().DeleteAsync(request);
        if (!response.IsSuccessful)
        {
            throw new Exception("Failed to delete task");
        }
    }

    public async Task ModifyAsync(Action<TaskProperties> propertiesFunc)
    {
        var properties = new TaskProperties();
        propertiesFunc(properties);
        var body = new Models.Common.Task(
            Id,
            properties.Description.OrElse(Description),
            properties.Name.OrElse(Name));
        var request = new RestRequest($"task/{Id}");
        request.AddJsonBody(body);
        var response = await ClickUp.GetRestClient().PutAsync<Models.Common.Task>(request);
        if (response == null)
        {
            throw new NullReferenceException("Invalid response from server");
        }
        Update(response);
    }
    
    internal ClickUpTask Update(Models.Common.Task model)
    {
        Name = model.Name;
        Description = model.Description;
        return this;
    }
}