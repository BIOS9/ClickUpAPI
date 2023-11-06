using System.Text.Json.Serialization;

namespace BIOS9.ClickUp.Rest.V2.Models.Common;

public record Folder(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("orderindex")] int OrderIndex,
    [property: JsonPropertyName("override_statuses")] bool OverrideStatuses,
    [property: JsonPropertyName("hidden")] bool Hidden,
    [property: JsonPropertyName("space")] SpaceLink Space,
    [property: JsonPropertyName("task_count")] string TaskCount,
    [property: JsonPropertyName("lists")] IReadOnlyList<string> Lists
);