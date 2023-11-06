using System.Text.Json.Serialization;

namespace BIOS9.ClickUp.Rest.V2.Models.Common;

public record ListLink(
    [property: JsonPropertyName("id")] string Id
);