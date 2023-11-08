using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using BIOS9.ClickUp.Core.Entities;
using BIOS9.ClickUp.Rest.V2.Models;
using RestSharp;

namespace BIOS9.ClickUp.Rest.V2.Entities;

public class ClickUpFolder : RestEntity, IFolder
{
    public string Name { get; private set; }

    public ClickUpFolder(string id, ClickUpClient clickUp) : base(id, clickUp) { }
    
    public ClickUpFolder(Models.Common.Folder model, ClickUpClient clickUp) : base(model.Id, clickUp)
    {
        Update(model);
    }

    public async Task DeleteAsync()
    {
        var request = new RestRequest($"folder/{Id}");
        var response = await ClickUp.GetRestClient().DeleteAsync(request);
        if (!response.IsSuccessful)
        {
            throw new Exception("Failed to delete folder");
        }
    }

    public async Task ModifyAsync(Action<FolderProperties> propertiesFunc)
    {
        var properties = new FolderProperties();
        propertiesFunc(properties);
        var body = new Models.Common.Space(
            Id,
            properties.Name.OrElse(Name));
        var request = new RestRequest($"folder/{Id}");
        request.AddJsonBody(body);
        var response = await ClickUp.GetRestClient().PutAsync<Models.Common.Folder>(request);
        if (response == null)
        {
            throw new NullReferenceException("Invalid response from server");
        }
        Update(response);
    }
    
    public async Task<IReadOnlyCollection<IList>> GetListsAsync(bool archived = false)
    {
        var request = new RestRequest($"folder/{Id}/list");
        request.AddParameter("archived", archived);
        var response = await ClickUp.GetRestClient().GetAsync<GetListsResponse>(request);
        if (response == null)
        {
            throw new NullReferenceException("Invalid response from server");
        }
        return response.Lists.Select(s => new ClickUpList(s, ClickUp)).ToImmutableList();
    }

    internal ClickUpFolder Update(Models.Common.Folder model)
    {
        Name = model.Name;
        return this;
    }
}