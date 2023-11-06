using System.Text.Json.Serialization;

namespace BIOS9.ClickUp.Rest.V2.Models;

public record Member(
    [property: JsonPropertyName("user")] User User
);