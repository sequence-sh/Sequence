using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommandDotNet;
using ConsoleTables;
using Reductech.EDR.ConnectorManagement;

namespace Reductech.EDR
{

/// <summary>
/// Provides commands to manage Connectors configurations
/// </summary>
[Command(
    Name        = "connector",
    Description = "Provides commands to manage Connectors configurations"
)]
public class ConnectorCommand
{
    private readonly IConnectorManager _connectorManager;

    /// <summary>
    /// Instantiate this command with the specified connector manager.
    /// </summary>
    /// <param name="connectorManager"></param>
    public ConnectorCommand(IConnectorManager connectorManager) =>
        _connectorManager = connectorManager;

    /// <summary>
    /// List the Connector configurations currently installed
    /// </summary>
    [Command(
        Name        = "list",
        Description = "List the Connector configurations currently installed"
    )]
    public async Task List(
        CancellationToken ct,
        [Operand(Description = "Filter configuration names using a regular expression")]
        string? filter = null)
    {
        if (!await _connectorManager.Verify(ct))
            throw new ConnectorConfigurationException("Could not validate installed connectors.");

        var connectors = _connectorManager.List(filter).ToArray();

        if (connectors.Length > 0)
        {
            ConsoleTable.From(
                    connectors.Select(
                        c => new ListRow(
                            c.name,
                            c.data.ConnectorSettings.Id,
                            c.data.ConnectorSettings.Version,
                            c.data.ConnectorSettings.Enable
                        )
                    )
                )
                .Write(Format.Minimal);
        }
    }

    private record ListRow(string Configuration, string ConnectorId, string Version, bool Enabled);

    /// <summary>
    /// List EDR Connectors available in the configured registry
    /// </summary>
    [Command(
        Name        = "find",
        Description = "List EDR Connectors available in the configured registry"
    )]
    public async Task Find(
        CancellationToken ct,
        [Option(
            LongName    = "filter",
            ShortName   = "f",
            Description = "Filter connector Ids using a regular expression"
        )]
        string? search = null,
        [Option(Description = "Include prerelease versions of connectors.")]
        bool prerelease = false)
    {
        var connectors = await _connectorManager.Find(search, prerelease, ct);

        if (connectors.Count > 0)
            ConsoleTable.From(connectors).Write(Format.Minimal);
    }

    /// <summary>
    /// Add a Connector configuration, installing the specified connector
    /// </summary>
    [Command(
        Name        = "add",
        Description = "Add a Connector configuration, installing the specified connector"
    )]
    public async Task Add(
        CancellationToken ct,
        [Operand(
            Name = "connectorId",
            Description =
                "The id of the connector to add. e.g. Reductech.EDR.Connectors.StructuredData"
        )]
        string connectorId,
        [Option(
            ShortName = "c",
            Description =
                "Name of the configuration. By default this is the same as the connector id."
        )]
        string? configuration = null,
        [Option(
            ShortName = "v",
            Description =
                "Add the specified version of the connector. By default the latest version will be added."
        )]
        string? version = null,
        [Option(
            ShortName   = "f",
            Description = "If a configuration already exists, overwrite."
        )]
        bool force = false,
        [Option(Description = "Include prerelease versions of connectors.")]
        bool prerelease = false) => await _connectorManager.Add(
        connectorId,
        configuration,
        version,
        prerelease,
        force,
        ct
    );

    /// <summary>
    /// Remove a Connector configuration
    /// </summary>
    [Command(
        Name        = "remove",
        Description = "Remove a Connector configuration"
    )]
    public async Task RemoveConnector(
        CancellationToken ct,
        [Operand(
            Name = "name",
            Description =
                "The name of the configuration to remove."
        )]
        string name,
        [Option(Description = "Do not remove the installation directory.")]
        bool configurationOnly = false) =>
        await _connectorManager.Remove(name, configurationOnly, ct);
}

}
