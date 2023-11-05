using System.Text.Json;
using System.Text.Json.Serialization;
using RestSharp;

namespace BIOS9.ClickUp;

public class ClickUpTeam
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }
    
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    
    [JsonPropertyName("color")]
    public required string Color { get; init; }
    
    [JsonPropertyName("avatar")]
    public required string Avatar { get; init; }
    
    //public required string? Members { get; init; }
    
    internal ClickUpClient? ClickUpClient { get; set; }

    public async Task<IList<ClickUpSpace>> GetSpacesAsync(bool archived = false)
    {
        var client = ClickUpClient?.GetRestClient() ?? throw new Exception("Cannot perform API action when ClickUpClient is not set");
        var request = new RestRequest($"team/{Id}/space");
        request.AddParameter("archived", archived);
        var response = await client.GetAsync(request);
        var jsonDoc = JsonDocument.Parse(response.Content ?? throw new NullReferenceException("Response content null"));
        var spaces = jsonDoc.RootElement.GetProperty("spaces").Deserialize<List<ClickUpSpace>>() ?? throw new ArgumentException("Invalid response content");
        foreach (var space in spaces)
        {
            space.ClickUpClient = ClickUpClient;
        }
        return spaces;
    }
}