namespace BIOS9.ClickUp.Core.Entities;

public interface IStatus
{
    enum StatusType
    {
        Open,
        Done,
        Closed
    }
    
    string Value { get; }
    string Color { get; }
    int OrderIndex { get; }
    StatusType Type { get; }
}