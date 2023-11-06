﻿using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using BIOS9.ClickUp.Core.Entities;
using BIOS9.ClickUp.Rest.V2.Models;
using RestSharp;

namespace BIOS9.ClickUp.Rest.V2.Entities;

public class ClickUpSpace : RestEntity, ISpace
{
    public string Name { get; private set; }
    
    public ClickUpSpace(string id, ClickUpClient clickUp) : base(id, clickUp) { }
    
    public ClickUpSpace(Models.Space model, ClickUpClient clickUp) : base(model.Id, clickUp)
    {
        Update(model);
    }

    public async Task<IReadOnlyCollection<IFolder>> GetFoldersAsync(bool archived = false)
    {
        var request = new RestRequest($"space/{Id}/folder");
        request.AddParameter("archived", archived);
        var response = await ClickUp.GetRestClient().GetAsync<GetFolderResponse>(request);
        if (response == null)
        {
            throw new NullReferenceException("Invalid response from server");
        }
        return response.Folders.Select(f => new ClickUpFolder(f, ClickUp)).ToImmutableList();
    }
    
    public async Task<IReadOnlyCollection<IList>> GetListsAsync(bool archived = false)
    {
        var request = new RestRequest($"space/{Id}/list");
        request.AddParameter("archived", archived);
        var response = await ClickUp.GetRestClient().GetAsync<GetListsResponse>(request);
        if (response == null)
        {
            throw new NullReferenceException("Invalid response from server");
        }
        return response.Lists.Select(s => new ClickUpList(s, ClickUp)).ToImmutableList();
    }
    
    internal ClickUpSpace Update(Models.Space model)
    {
        Name = model.Name;
        return this;
    }
}