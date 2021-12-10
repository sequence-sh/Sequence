namespace Reductech.EDR;

/// <summary>
/// Represents errors that occur when configuring or validating connectors.
/// </summary>
public class ConnectorConfigurationException : Exception
{
    /// <inheritdoc />
    public ConnectorConfigurationException(string message) : base(message) { }
}

/// <summary>
/// Thrown when an argument provided to EDR is not valid.
/// </summary>
public class CommandLineArgumentException : Exception
{
    /// <inheritdoc />
    public CommandLineArgumentException(string message) : base(message) { }
}
