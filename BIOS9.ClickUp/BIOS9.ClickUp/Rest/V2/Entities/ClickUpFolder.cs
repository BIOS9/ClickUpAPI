using System.Collections.Immutable;
using BIOS9.ClickUp.Core.Entities;
using BIOS9.ClickUp.Core.Util;
using BIOS9.ClickUp.Rest.V2.Models;
using RestSharp;

namespace BIOS9.ClickUp.Rest.V2.Entities;

public class ClickUpFolder : RestEntity, IFolder
{
    public string Name { get; private set; }

    public ClickUpFolder(string id, ClickUpClient clickUp) : base(id, clickUp) { }
    
    public ClickUpFolder(Models.Common.Folder model, ClickUpClient clickUp) : base(model.Id.Value, clickUp)
    {
        Update(model);
    }

    public async Task DeleteAsync()
    {
        await ClickUp.RequestAsync(
            Method.Delete, 
            $"folder/{Id}");
    }

    public async Task ModifyAsync(Action<FolderProperties> propertiesFunc)
    {
        var properties = new FolderProperties();
        propertiesFunc(properties);
        var body = new Models.Common.Folder(
            Optional<string>.Unspecified, 
            properties.Name);
        var response = await ClickUp.RequestAsync<Models.Common.Folder>(
            Method.Put, 
            $"folder/{Id}",
            payload: body);
        Update(response);
    }
    
    public async Task<IReadOnlyCollection<IList>> GetListsAsync(bool archived = false)
    {
        var response = await ClickUp.RequestAsync<GetListsResponse>(
            Method.Get, 
            $"folder/{Id}/list", new []
            {
                new QueryParameter("archived", archived.ToString())
            });
        return response.Lists.Select(s => new ClickUpList(s, ClickUp)).ToImmutableList();
    }

    public async Task<IList> CreateListAsync(string name)
    {
        throw new NotImplementedException();
    }

    public override async Task UpdateAsync()
    {
        var response = await ClickUp.RequestAsync<Models.Common.Folder>(
            Method.Get, 
            $"folder/{Id}");
        Update(response);
    }
    
    internal ClickUpFolder Update(Models.Common.Folder model)
    {
        Name = model.Name.Value;
        return this;
    }
}