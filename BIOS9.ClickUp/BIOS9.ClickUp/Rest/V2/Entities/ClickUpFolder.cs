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

    public async Task<IList<ClickUpList>> GetListsAsync(bool archived = false)
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