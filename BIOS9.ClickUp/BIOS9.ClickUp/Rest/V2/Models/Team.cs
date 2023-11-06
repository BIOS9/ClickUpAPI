using System.Text.Json.Serialization;

namespace BIOS9.ClickUp.Rest.V2.Models;

public record Team(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("color")] string Color,
    [property: JsonPropertyName("avatar")] string Avatar,
    [property: JsonPropertyName("members")] IReadOnlyList<Member> Members
);
