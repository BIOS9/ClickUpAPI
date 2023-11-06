using System.Text.Json.Serialization;
using BIOS9.ClickUp.Rest.V2.Models.Common;

namespace BIOS9.ClickUp.Rest.V2.Models;

public record GetListsResponse(
    [property: JsonPropertyName("lists")] IReadOnlyList<List> Lists
);

public record Priority(
    [property: JsonPropertyName("priority")] string Value,
    [property: JsonPropertyName("color")] string Color
);

public record ListStatus(
    [property: JsonPropertyName("status")] string Value,
    [property: JsonPropertyName("color")] string Color,
    [property: JsonPropertyName("hide_label")] bool HideLabel
);