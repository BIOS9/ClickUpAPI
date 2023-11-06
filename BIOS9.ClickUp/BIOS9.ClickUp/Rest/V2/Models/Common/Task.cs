using System.Text.Json.Serialization;

namespace BIOS9.ClickUp.Rest.V2.Models.Common;

public record Task(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("status")] Status Status,
    [property: JsonPropertyName("markdown_description")] string MarkdownDescription,
    [property: JsonPropertyName("orderindex")] string OrderIndex,
    [property: JsonPropertyName("date_created")] string DateCreated,
    [property: JsonPropertyName("date_updated")] string DateUpdated,
    [property: JsonPropertyName("date_closed")] object DateClosed,
    [property: JsonPropertyName("date_done")] object DateDone,
    [property: JsonPropertyName("creator")] Creator Creator,
    [property: JsonPropertyName("assignees")] IReadOnlyList<object> Assignees,
    [property: JsonPropertyName("checklists")] IReadOnlyList<object> Checklists,
    [property: JsonPropertyName("tags")] IReadOnlyList<object> Tags,
    [property: JsonPropertyName("parent")] object Parent,
    [property: JsonPropertyName("priority")] object Priority,
    [property: JsonPropertyName("due_date")] object DueDate,
    [property: JsonPropertyName("start_date")] object StartDate,
    [property: JsonPropertyName("time_estimate")] object TimeEstimate,
    [property: JsonPropertyName("time_spent")] object TimeSpent,
    [property: JsonPropertyName("list")] ListLink List,
    [property: JsonPropertyName("folder")] FolderLink Folder,
    [property: JsonPropertyName("space")] Space Space,
    [property: JsonPropertyName("url")] string Url
);