using System.Text.Json.Serialization;

namespace BIOS9.ClickUp.Rest.V2.Models;

public record GetSpacesResponse(
    [property: JsonPropertyName("spaces")] IReadOnlyList<Space> Spaces
);

public record Checklists(
    [property: JsonPropertyName("enabled")] bool Enabled
);

public record CustomFields(
    [property: JsonPropertyName("enabled")] bool Enabled
);

public record DependencyWarning(
    [property: JsonPropertyName("enabled")] bool Enabled
);

public record DueDates(
    [property: JsonPropertyName("enabled")] bool Enabled,
    [property: JsonPropertyName("start_date")] bool StartDate,
    [property: JsonPropertyName("remap_due_dates")] bool RemapDueDates,
    [property: JsonPropertyName("remap_closed_due_date")] bool RemapClosedDueDate
);

public record Features(
    [property: JsonPropertyName("due_dates")] DueDates DueDates,
    [property: JsonPropertyName("time_tracking")] TimeTracking TimeTracking,
    [property: JsonPropertyName("tags")] Tags Tags,
    [property: JsonPropertyName("time_estimates")] TimeEstimates TimeEstimates,
    [property: JsonPropertyName("checklists")] Checklists Checklists,
    [property: JsonPropertyName("custom_fields")] CustomFields CustomFields,
    [property: JsonPropertyName("remap_dependencies")] RemapDependencies RemapDependencies,
    [property: JsonPropertyName("dependency_warning")] DependencyWarning DependencyWarning,
    [property: JsonPropertyName("portfolios")] Portfolios Portfolios
);

public record Portfolios(
    [property: JsonPropertyName("enabled")] bool Enabled
);

public record RemapDependencies(
    [property: JsonPropertyName("enabled")] bool Enabled
);

public record Space(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("private")] bool Private,
    [property: JsonPropertyName("statuses")] IReadOnlyList<SpaceStatus> Statuses,
    [property: JsonPropertyName("multiple_assignees")] bool MultipleAssignees,
    [property: JsonPropertyName("features")] Features Features
);

public record SpaceStatus(
    [property: JsonPropertyName("status")] string Value,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("orderindex")] int Orderindex,
    [property: JsonPropertyName("color")] string Color
);

public record Tags(
    [property: JsonPropertyName("enabled")] bool Enabled
);

public record TimeEstimates(
    [property: JsonPropertyName("enabled")] bool Enabled
);

public record TimeTracking(
    [property: JsonPropertyName("enabled")] bool Enabled
);

