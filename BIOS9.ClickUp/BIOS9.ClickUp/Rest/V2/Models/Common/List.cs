using BIOS9.ClickUp.Core.Util;
using Newtonsoft.Json;

namespace BIOS9.ClickUp.Rest.V2.Models.Common;

public record List(
    [property: JsonProperty("id")] Optional<string> Id,
    [property: JsonProperty("name")] Optional<string> Name
);