using System.Text.Json.Serialization;

namespace BIOS9.ClickUp.Rest.V2.Models.Common;

public record List(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("orderindex")] int OrderIndex,
    [property: JsonPropertyName("content")] string Content,
    [property: JsonPropertyName("assignee")] object Assignee,
    [property: JsonPropertyName("task_count")] object TaskCount,
    [property: JsonPropertyName("due_date")] string DueDate,
    [property: JsonPropertyName("start_date")] object StartDate,
    [property: JsonPropertyName("folder")] FolderLink Folder,
    [property: JsonPropertyName("archived")] bool Archived,
    [property: JsonPropertyName("override_statuses")] bool OverrideStatuses,
    [property: JsonPropertyName("permission_level")] string PermissionLevel
);