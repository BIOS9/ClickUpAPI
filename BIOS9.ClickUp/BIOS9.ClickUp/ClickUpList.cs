using System.Text.Json.Serialization;

namespace BIOS9.ClickUp;

public class ClickUpList
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }
    
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    
    [JsonPropertyName("orderindex")]
    public required int OrderIndex { get; init; }

    // [JsonPropertyName("content")]
    // public required string Content { get; init; }
    
    // status
    
    // priority
    
    // assignee
    
    // task_count
    
    // due_date
    
    // start date
    
    // folder
    
    // space
    
    [JsonPropertyName("archived")]
    public required bool Archived { get; init; }
    
    [JsonPropertyName("override_statuses")]
    public required bool OverrideStatuses { get; init; }
    
    // permission_level
    
    internal ClickUpClient? ClickUpClient { get; set; }
}