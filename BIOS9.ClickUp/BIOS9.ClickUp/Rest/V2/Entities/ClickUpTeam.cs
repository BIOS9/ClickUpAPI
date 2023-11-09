using System.Collections.Immutable;
using BIOS9.ClickUp.Core.Entities;
using BIOS9.ClickUp.Rest.V2.Models;
using RestSharp;

namespace BIOS9.ClickUp.Rest.V2.Entities;

public class ClickUpTeam : RestEntity, ITeam
{
    public string Name { get; private set; }

    public ClickUpTeam(string id, ClickUpClient clickUp) : base(id, clickUp) { }
    
    public ClickUpTeam(Models.Common.Team model, ClickUpClient clickUp) : base(model.Id.Value, clickUp)
    {
        Update(model);
    }
    
    public async Task<IReadOnlyCollection<ISpace>> GetSpacesAsync(bool archived = false)
    {
        var response = await ClickUp.RequestAsync<GetSpacesResponse>(
            Method.Get, 
            $"team/{Id}/space", new []
            {
                new QueryParameter("archived", archived.ToString())
            });
        return response.Spaces.Select(s => new ClickUpSpace(s, ClickUp)).ToImmutableList();
    }

    public async Task<ISpace> CreateSpaceAsync(string name)
    {
        var response = await ClickUp.RequestAsync<Models.Common.Space>(
            Method.Post, 
            $"team/{Id}/space",
            payload: new CreateSpaceRequest(name));
        return new ClickUpSpace(response, ClickUp);
    }

    public override async Task UpdateAsync()
    {
        var response = await ClickUp.RequestAsync<Models.Common.Team>(
            Method.Get, 
            $"team/{Id}");
        Update(response);
    }
    
    internal ClickUpTeam Update(Models.Common.Team model)
    {
        Name = model.Name.Value;
        return this;
    }
}