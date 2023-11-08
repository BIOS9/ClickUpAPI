using System.Text.Json.Serialization;

namespace BIOS9.ClickUp.Rest.V2.Models;

public record CreateSpaceRequest(
    [property: JsonPropertyName("name")] string Name
);