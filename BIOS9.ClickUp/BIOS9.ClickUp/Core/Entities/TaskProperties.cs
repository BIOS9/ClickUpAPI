using BIOS9.ClickUp.Core.Util;

namespace BIOS9.ClickUp.Core.Entities;

public class TaskProperties
{
    public Optional<string> Name { get; set; }
    public Optional<string> Description { get; set; }
}