using System.Text.Json.Serialization;
using Task = BIOS9.ClickUp.Rest.V2.Models.Common.Task;

namespace BIOS9.ClickUp.Rest.V2.Models;

public record GetTasksResponse(
    [property: JsonPropertyName("tasks")] IReadOnlyList<Task> Tasks,
    [property: JsonPropertyName("last_page")] bool LastPage
);