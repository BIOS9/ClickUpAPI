using System.Collections.Immutable;
using BIOS9.ClickUp.Core.Entities;
using BIOS9.ClickUp.Rest.V2.Models;
using BIOS9.ClickUp.Rest.V2.Models.Common;
using RestSharp;

namespace BIOS9.ClickUp.Rest.V2.Entities;

public class ClickUpTeam : RestEntity, ITeam
{
    public string Name { get; private set; }

    public ClickUpTeam(string id, ClickUpClient clickUp) : base(id, clickUp) { }
    
    public ClickUpTeam(Team model, ClickUpClient clickUp) : base(model.Id, clickUp)
    {
        Update(model);
    }
    
    public async Task<IReadOnlyCollection<ISpace>> GetSpacesAsync(bool archived = false)
    {
        var request = new RestRequest($"team/{Id}/space");
        request.AddParameter("archived", archived);
        var response = await ClickUp.GetRestClient().GetAsync<GetSpacesResponse>(request);
        if (response == null)
        {
            throw new NullReferenceException("Invalid response from server");
        }
        return response.Spaces.Select(s => new ClickUpSpace(s, ClickUp)).ToImmutableList();
    }

    internal ClickUpTeam Update(Team model)
    {
        Name = model.Name;
        return this;
    }
}