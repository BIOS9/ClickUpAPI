using System.Text.Json;
using System.Text.Json.Serialization;
using RestSharp;

namespace BIOS9.ClickUp;

public class ClickUpSpace
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }
    
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    
    [JsonPropertyName("multiple_assignees")]
    public required bool MultipleAssignees { get; init; }
    
    // statuses
    
    // features
    
    public ClickUpClient? ClickUpClient { get; set; }
    
    public async Task<IList<ClickUpFolder>> GetFoldersAsync(bool archived = false)
    {
        var client = ClickUpClient?.GetRestClient() ?? throw new Exception("Cannot perform API action when ClickUpClient is not set");
        var request = new RestRequest($"space/{Id}/folder");
        request.AddParameter("archived", archived);
        var response = await client.GetAsync(request);
        var jsonDoc = JsonDocument.Parse(response.Content ?? throw new NullReferenceException("Response content null"));
        var folders = jsonDoc.RootElement.GetProperty("folders").Deserialize<List<ClickUpFolder>>() ?? throw new ArgumentException("Invalid response content");
        foreach (var folder in folders)
        {
            folder.ClickUpClient = ClickUpClient;
        }
        return folders;
    }
    
    public async Task<IList<ClickUpList>> GetListsAsync(bool archived = false)
    {
        var client = ClickUpClient?.GetRestClient() ?? throw new Exception("Cannot perform API action when ClickUpClient is not set");
        var request = new RestRequest($"space/{Id}/list");
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