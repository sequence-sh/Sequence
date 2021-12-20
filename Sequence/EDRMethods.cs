using CommandDotNet;

namespace Reductech.Sequence;

/// <summary>
/// Sequence methods to be run in the console.
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
    [Subcommand]
    public ConnectorCommand Connector { get; set; } = null!;

    /// <summary>
    /// The run command
    /// </summary>
    [Subcommand]
    public RunCommand Run { get; set; } = null!;

    /// <summary>
    /// The steps command
    /// </summary>
    [Subcommand]
    public StepsCommand Steps { get; set; } = null!;

    /// <summary>
    /// The validate command
    /// </summary>
    [Subcommand]
    public ValidateCommand Validate { get; set; } = null!;
}
