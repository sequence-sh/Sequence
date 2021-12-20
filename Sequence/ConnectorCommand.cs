using System.Linq;
using CommandDotNet;
using ConsoleTables;
using Microsoft.Extensions.Logging;
using Reductech.Sequence.ConnectorManagement.Base;

namespace Reductech.Sequence;

/// <summary>
/// Provides commands to manage Connectors configurations
/// </summary>
[Command(
    "connector",
    Description = "Provides commands to manage Connectors configurations"
)]
public class ConnectorCommand
{
    private readonly IConnectorManager _connectorManager;

    private readonly ILogger<ConnectorCommand> _logger;

    /// <summary>
    /// Instantiate this command with the specified connector manager.
    /// </summary>
    /// <param name="connectorManager"></param>
    public ConnectorCommand(IConnectorManager connectorManager, ILogger<ConnectorCommand> logger)
    {
        _connectorManager = connectorManager;
        _logger           = logger;
    }

    /// <summary>
    /// List the Connector configurations currently installed
    /// </summary>
    [Command(
        "list",
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
    /// List Sequence® Connectors available in the configured registry
    /// </summary>
    [Command(
        "find",
        Description = "List Sequence® Connectors available in the configured registry"
    )]
    public async Task Find(
        CancellationToken ct,
        [Option(
            'f',
            "filter",
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
        "add",
        Description = "Add a Connector configuration, installing the specified connector"
    )]
    public async Task Add(
        CancellationToken ct,
        [Operand(
            "connectorId",
            Description =
                "The id of the connector to add. e.g. Reductech.Sequence.Connectors.StructuredData"
        )]
        string connectorId,
        [Option(
            'c',
            Description =
                "Name of the configuration. By default this is the same as the connector id."
        )]
        string? configuration = null,
        [Option(
            'v',
            Description =
                "Add the specified version of the connector. By default the latest version will be added."
        )]
        string? version = null,
        [Option(
            'f',
            Description = "If a configuration already exists, overwrite."
        )]
        bool force = false,
        [Option(Description = "Include prerelease versions of connectors.")]
        bool prerelease = false)
    {
        var options = await _connectorManager.Find(connectorId, prerelease, ct);

        if (options is null || options.Count == 0) //Try add anyway
        {
            await _connectorManager.Add(
                connectorId,
                configuration,
                version,
                prerelease,
                force,
                ct
            );
        }
        else if (options.Count == 1)
        {
            await _connectorManager.Add(
                options.Single().Id,
                configuration,
                version,
                prerelease,
                force,
                ct
            );
        }
        else
        {
            _logger.LogError($"Several Connector names match '{connectorId}':");

            foreach (var connectorMetadata in options)
            {
                _logger.LogInformation(connectorMetadata.Id);
            }
        }
    }

    /// <summary>
    /// Update a Connector configuration to the specified or latest version
    /// </summary>
    [Command(
        "update",
        Description = "Update a Connector configuration to the specified or latest version"
    )]
    public async Task Update(
        CancellationToken ct,
        [Operand(
            "configuration",
            Description = "Name of the configuration to update."
        )]
        string configuration,
        [Option(
            'v',
            Description =
                "Update to the specified version. By default the latest version will be used."
        )]
        string? version = null,
        [Option(Description = "Include prerelease versions of connectors.")]
        bool prerelease = false) =>
        await _connectorManager.Update(configuration, version, prerelease, ct);

    /// <summary>
    /// Remove a Connector configuration
    /// </summary>
    [Command(
        "remove",
        Description = "Remove a Connector configuration"
    )]
    public async Task RemoveConnector(
        CancellationToken ct,
        [Operand(
            "name",
            Description =
                "The name of the configuration to remove."
        )]
        string name,
        [Option(Description = "Do not remove the installation directory.")]
        bool configurationOnly = false) =>
        await _connectorManager.Remove(name, configurationOnly, ct);
}
