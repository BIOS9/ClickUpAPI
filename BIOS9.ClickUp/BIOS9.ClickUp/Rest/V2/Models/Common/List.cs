﻿using System.Text.Json.Serialization;

namespace BIOS9.ClickUp.Rest.V2.Models.Common;

public record List(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name
);