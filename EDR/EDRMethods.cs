using CommandDotNet;

namespace Reductech.EDR
{

/// <summary>
/// EDR methods to be run in the console.
/// </summary>
[Command]
public class EDRMethods
{
    /// <summary>
    /// 
    /// </summary>
    [SubCommand]
    public RunCommand Run { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [SubCommand]
    public ConnectorCommand Connector { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [SubCommand]
    public ValidateCommand Validate { get; set; }
}

}
