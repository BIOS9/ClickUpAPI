using System.Text.Json.Serialization;

namespace BIOS9.ClickUp.Rest.V2.Models;

public record User(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("color")] string Color,
    [property: JsonPropertyName("profilePicture")] string ProfilePicture
);