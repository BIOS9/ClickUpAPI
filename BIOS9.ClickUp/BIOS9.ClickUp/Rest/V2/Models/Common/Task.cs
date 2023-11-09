using BIOS9.ClickUp.Core.Util;
using Newtonsoft.Json;

namespace BIOS9.ClickUp.Rest.V2.Models.Common;

public record Task(
    [property: JsonProperty("id")] Optional<string> Id,
    [property: JsonProperty("name")] Optional<string> Name,
    [property: JsonProperty("description")] Optional<string> Description
);