using System.Text.Json.Serialization;

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
}