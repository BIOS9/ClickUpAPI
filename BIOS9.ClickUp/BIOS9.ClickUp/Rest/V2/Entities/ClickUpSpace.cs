using System.Collections.Immutable;
using BIOS9.ClickUp.Core.Entities;
using BIOS9.ClickUp.Core.Util;
using BIOS9.ClickUp.Rest.V2.Models;
using RestSharp;

namespace BIOS9.ClickUp.Rest.V2.Entities;

public class ClickUpSpace : RestEntity, ISpace
{
    public string Name { get; private set; }
    
    public ClickUpSpace(string id, ClickUpClient clickUp) : base(id, clickUp) { }
    
    public ClickUpSpace(Models.Common.Space model, ClickUpClient clickUp) : base(model.Id.Value, clickUp)
    {
        Update(model);
    }

    public async Task DeleteAsync()
    {
        await ClickUp.RequestAsync(
            Method.Delete, 
            $"space/{Id}");
    }

    public async Task ModifyAsync(Action<SpaceProperties> propertiesFunc)
    {
        var properties = new SpaceProperties();
        propertiesFunc(properties);
        var body = new Models.Common.Space(
            Optional<string>.Unspecified, 
            properties.Name);
        var response = await ClickUp.RequestAsync<Models.Common.Space>(
            Method.Put, 
            $"space/{Id}",
            payload: body);
        Update(response);
    }

    public async Task<IReadOnlyCollection<IFolder>> GetFoldersAsync(bool archived = false)
    {
        var response = await ClickUp.RequestAsync<GetFolderResponse>(
            Method.Get, 
            $"space/{Id}/folder", new []
            {
                new QueryParameter("archived", archived.ToString())
            });
        return response.Folders.Select(f => new ClickUpFolder(f, ClickUp)).ToImmutableList();
    }

    public async Task<IFolder> CreateFolderAsync(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<IList>> GetListsAsync(bool archived = false)
    {
        var response = await ClickUp.RequestAsync<GetListsResponse>(
            Method.Get, 
            $"space/{Id}/list", new []
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
        var response = await ClickUp.RequestAsync<Models.Common.Space>(
            Method.Get, 
            $"space/{Id}");
        Update(response);
    }
    
    internal ClickUpSpace Update(Models.Common.Space model)
    {
        Name = model.Name.Value;
        return this;
    }
}