using System.Text.Json;
using RestSharp;

namespace BIOS9.ClickUp;

public class ClickUpClient
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

    public async Task<IList<ClickUpTeam>> GetAuthorizedTeamsAsync()
    {
        var client = GetRestClient();
        var request = new RestRequest("team");
        var response = await client.GetAsync(request);
        var jsonDoc = JsonDocument.Parse(response.Content ?? throw new NullReferenceException("Response content null"));
        var teams = jsonDoc.RootElement.GetProperty("teams").Deserialize<List<ClickUpTeam>>() ?? throw new ArgumentException("Invalid response content");
        foreach (var team in teams)
        {
            team.ClickUpClient = this;
        }
        return teams;
    }
    
    public async Task<ClickUpTask> GetTaskAsync(string id)
    {
        return null;
    }
}