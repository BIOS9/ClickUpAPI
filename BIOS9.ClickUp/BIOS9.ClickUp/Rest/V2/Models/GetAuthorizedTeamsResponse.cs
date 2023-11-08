using System.Text.Json.Serialization;
using BIOS9.ClickUp.Rest.V2.Models.Common;

namespace BIOS9.ClickUp.Rest.V2.Models;

public record GetAuthorizedTeamsResponse(
    [property: JsonPropertyName("teams")] IReadOnlyList<Team> Teams
);