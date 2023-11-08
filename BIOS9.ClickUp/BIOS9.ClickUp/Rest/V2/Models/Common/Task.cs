using System.Text.Json.Serialization;

namespace BIOS9.ClickUp.Rest.V2.Models.Common;

public record Task(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string Description
);