using System.Text.Json.Serialization;
using BIOS9.ClickUp.Rest.V2.Models.Common;

namespace BIOS9.ClickUp.Rest.V2.Models;

public record GetListsResponse(
    [property: JsonPropertyName("lists")] IReadOnlyList<List> Lists
);