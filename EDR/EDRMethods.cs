using CommandDotNet;

namespace Reductech.EDR;

/// <summary>
/// EDR methods to be run in the console.
/// </summary>
[Command(
    Description =
        "E-Discovery Reduct\n\nAn application for running workflows defined using the Sequence Configuration Language (SCL).\nFor more information please see the documentation: https://docs.reductech.io"
)]
public class EDRMethods
{
    /// <summary>
    /// The connector command
    /// </summary>
    [SubCommand]
    public ConnectorCommand Connector { get; set; } = null!;

    /// <summary>
    /// The run command
    /// </summary>
    [SubCommand]
    public RunCommand Run { get; set; } = null!;

    /// <summary>
    /// The steps command
    /// </summary>
    [SubCommand]
    public StepsCommand Steps { get; set; } = null!;

    /// <summary>
    /// The validate command
    /// </summary>
    [SubCommand]
    public ValidateCommand Validate { get; set; } = null!;
}
