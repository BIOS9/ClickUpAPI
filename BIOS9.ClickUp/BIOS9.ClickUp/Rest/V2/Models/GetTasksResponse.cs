using System.Text.Json.Serialization;
using Task = BIOS9.ClickUp.Rest.V2.Models.Common.Task;

namespace BIOS9.ClickUp.Rest.V2.Models;

public record GetTasksResponse(
    [property: JsonPropertyName("tasks")] IReadOnlyList<Task> Tasks,
    [property: JsonPropertyName("last_page")] bool LastPage
);

public record Creator(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("color")] string Color,
    [property: JsonPropertyName("profilePicture")] string ProfilePicture
);

public record Status(
    [property: JsonPropertyName("status")] string Value,
    [property: JsonPropertyName("color")] string Color,
    [property: JsonPropertyName("orderindex")] int OrderIndex,
    [property: JsonPropertyName("type")] string Type
);

