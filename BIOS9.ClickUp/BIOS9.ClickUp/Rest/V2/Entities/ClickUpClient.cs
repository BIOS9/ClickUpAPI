using System.Collections.Immutable;
using System.Text.Json;
using BIOS9.ClickUp.Core.Entities;
using BIOS9.ClickUp.Rest.V2.Models;
using RestSharp;

namespace BIOS9.ClickUp.Rest.V2.Entities;

public class ClickUpClient : IClickUpClient
{
    private readonly string _personalApiToken;
    
    public ClickUpClient(string personalApiToken)
    {
        _personalApiToken = personalApiToken;
    }

    internal RestClient GetRestClient()
    {
        return new RestClient(new RestClientOptions()
        {
            BaseUrl = new Uri("https://api.clickup.com/api/v2/"),
            Authenticator = new PersonalApiAuthenticator(_personalApiToken),
            ThrowOnAnyError = true,
            ThrowOnDeserializationError = true
        });
    }

    public async Task<IReadOnlyCollection<ITeam>> GetAuthorizedTeamsAsync()
    {
        var request = new RestRequest("team");
        var response = await GetRestClient().GetAsync<GetAuthorizedTeamsResponse>(request);
        if (response == null)
        {
            throw new NullReferenceException("Invalid response from server");
        }
        return response.Teams.Select(t => new ClickUpTeam(t, this)).ToImmutableList();
    }
}