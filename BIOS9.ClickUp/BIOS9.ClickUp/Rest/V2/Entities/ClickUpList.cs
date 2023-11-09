using System.Collections.Immutable;
using System.Text.Json;
using BIOS9.ClickUp.Core.Entities;
using BIOS9.ClickUp.Core.Util;
using BIOS9.ClickUp.Rest.V2.Models;
using BIOS9.ClickUp.Rest.V2.Util;
using RestSharp;

namespace BIOS9.ClickUp.Rest.V2.Entities;

public class ClickUpList : RestEntity, IList
{
    public string Name { get; private set; }

    public ClickUpList(string id, ClickUpClient clickUp) : base(id, clickUp) { }
    
    public ClickUpList(Models.Common.List model, ClickUpClient clickUp) : base(model.Id.Value, clickUp)
    {
        Update(model);
    }

    public async Task DeleteAsync()
    {
        await ClickUp.RequestAsync(
            Method.Delete, 
            $"list/{Id}");
    }

    public async Task ModifyAsync(Action<ListProperties> propertiesFunc)
    {
        var properties = new ListProperties();
        propertiesFunc(properties);
        var body = new Models.Common.List(
            Optional<string>.Unspecified,
            properties.Name);
        var response = await ClickUp.RequestAsync<Models.Common.List>(
            Method.Put, 
            $"list/{Id}",
            payload: body);
        Update(response);
    }
    
    public async Task<IReadOnlyCollection<ITask>> GetTasksAsync(bool includeSubtasks = false, bool includeClosed = false, bool archived = false)
    {
        var response = await ClickUp.RequestAsync<GetTasksResponse>(
            Method.Get, 
            $"list/{Id}/task", new []
            {
                new QueryParameter("archived", archived.ToString()),
                new QueryParameter("subtasks", includeSubtasks.ToString()),
                new QueryParameter("include_closed", includeClosed.ToString())
            });
        return response.Tasks.Select(s => new ClickUpTask(s, ClickUp)).ToImmutableList();
    }

    public async Task<ITask> CreateTaskAsync(Action<TaskProperties> propertiesFunc)
    {
        var properties = new TaskProperties();
        propertiesFunc(properties);
        var body = new Models.Common.Task(
            Optional<string>.Unspecified, 
            properties.Name.OrThrow(new ArgumentException($"{nameof(properties.Name)} must be specified")),
            properties.Description);
        var response = await ClickUp.RequestAsync<Models.Common.Task>(
            Method.Post, 
            $"list/{Id}/task",
            payload: body);
        return new ClickUpTask(response, ClickUp);
    }

    public override async Task UpdateAsync()
    {
        var response = await ClickUp.RequestAsync<Models.Common.List>(
            Method.Get, 
            $"list/{Id}");
        Update(response);
    }
    
    internal ClickUpList Update(Models.Common.List model)
    {
        Name = model.Name.Value;
        return this;
    }
}