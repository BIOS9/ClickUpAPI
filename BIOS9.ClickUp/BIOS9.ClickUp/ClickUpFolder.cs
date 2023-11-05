using System.Text.Json;
using System.Text.Json.Serialization;
using RestSharp;

namespace BIOS9.ClickUp;

public class ClickUpFolder
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }
    
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    
    [JsonPropertyName("orderindex")]
    public required int OrderIndex { get; init; }
    
    [JsonPropertyName("override_statuses")]
    public required bool OverrideStatuses { get; init; }
    
    [JsonPropertyName("hidden")]
    public required bool Hidden { get; init; }
    
    // space
    
    // task count
    
    // lists
    
    public ClickUpClient? ClickUpClient { get; set; }
    
    public async Task<IList<ClickUpList>> GetListsAsync(bool archived = false)
    {
        var client = ClickUpClient?.GetRestClient() ?? throw new Exception("Cannot perform API action when ClickUpClient is not set");
        var request = new RestRequest($"folder/{Id}/list");
        request.AddParameter("archived", archived);
        var response = await client.GetAsync(request);
        var jsonDoc = JsonDocument.Parse(response.Content ?? throw new NullReferenceException("Response content null"));
        var lists = jsonDoc.RootElement.GetProperty("lists").Deserialize<List<ClickUpList>>() ?? throw new ArgumentException("Invalid response content");
        foreach (var list in lists)
        {
            list.ClickUpClient = ClickUpClient;
        }
        return lists;
    }
}