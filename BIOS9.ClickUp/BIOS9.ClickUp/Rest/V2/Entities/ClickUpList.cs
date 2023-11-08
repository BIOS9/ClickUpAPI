using System.Collections.Immutable;
using System.Text.Json;
using BIOS9.ClickUp.Core.Entities;
using BIOS9.ClickUp.Rest.V2.Models;
using RestSharp;

namespace BIOS9.ClickUp.Rest.V2.Entities;

public class ClickUpList : RestEntity, IList
{
    public string Name { get; private set; }

    public ClickUpList(string id, ClickUpClient clickUp) : base(id, clickUp) { }
    
    public ClickUpList(Models.Common.List model, ClickUpClient clickUp) : base(model.Id, clickUp)
    {
        Update(model);
    }

    public async Task DeleteAsync()
    {
        var request = new RestRequest($"list/{Id}");
        var response = await ClickUp.GetRestClient().DeleteAsync(request);
        if (!response.IsSuccessful)
        {
            throw new Exception("Failed to delete list");
        }
    }

    public async Task ModifyAsync(Action<ListProperties> propertiesFunc)
    {
        var properties = new ListProperties();
        propertiesFunc(properties);
        var body = new Models.Common.List(
            Id,
            properties.Name.OrElse(Name));
        var request = new RestRequest($"list/{Id}");
        request.AddJsonBody(body);
        var response = await ClickUp.GetRestClient().PutAsync<Models.Common.List>(request);
        if (response == null)
        {
            throw new NullReferenceException("Invalid response from server");
        }
        Update(response);
    }
    
    public async Task<IReadOnlyCollection<ITask>> GetTasksAsync(bool includeSubtasks = false, bool includeClosed = false, bool archived = false)
    {
        var request = new RestRequest($"list/{Id}/task");
        request.AddParameter("archived", archived);
        request.AddParameter("subtasks", includeSubtasks);
        request.AddParameter("include_closed", includeSubtasks);
        var response = await ClickUp.GetRestClient().GetAsync<GetTasksResponse>(request);
        if (response == null)
        {
            throw new NullReferenceException("Invalid response from server");
        }
        return response.Tasks.Select(s => new ClickUpTask(s, ClickUp)).ToImmutableList();
    }
    
    internal ClickUpList Update(Models.Common.List model)
    {
        Name = model.Name;
        return this;
    }
}