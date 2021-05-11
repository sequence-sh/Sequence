using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommandDotNet;
using ConsoleTables;
using Microsoft.Extensions.Primitives;
using Reductech.EDR.ConnectorManagement;
using Reductech.EDR.Core.Internal;

namespace Reductech.EDR
{

/// <summary>
/// 
/// </summary>
[Command(
    Name        = "connector",
    Usage       = "connector <command> <arguments>",
    Description = "Connector management"
)]
public class ConnectorCommand
{
    private readonly IConnectorManager _connectorManager;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="connectorManager"></param>
    public ConnectorCommand(IConnectorManager connectorManager)
    {
        _connectorManager = connectorManager;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ct"></param>
    /// <param name="filter"></param>
    [Command(
        Name        = "list",
        Description = "List the Connectors currently installed"
    )]
    public async Task List(
        CancellationToken ct,
        [Option(
            LongName    = "filter",
            ShortName   = "f",
            Description = "Filter available connectors using this regex."
        )]
        string? filter = null)
    {
        if (!await _connectorManager.Verify(ct))
            throw new InvalidConfigurationException("Could not validate installed connectors.");

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
    /// 
    /// </summary>
    /// <param name="ct"></param>
    /// <param name="search"></param>
    /// <param name="prerelease"></param>
    /// <returns></returns>
    [Command(
        Name        = "find",
        Description = "Find EDR connectors"
    )]
    public async Task Find(
        CancellationToken ct,
        [Option(
            LongName    = "filter",
            ShortName   = "f",
            Description = "Filter available connectors using this regex."
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
    /// 
    /// </summary>
    /// <param name="ct"></param>
    /// <param name="name"></param>
    /// <param name="configuration"></param>
    /// <param name="version"></param>
    /// <param name="force"></param>
    /// <param name="prerelease"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    [Command(
        Name        = "add",
        Description = "Add Connector to EDR"
    )]
    public async Task Add(
        CancellationToken ct,
        [Operand(
            Name = "name",
            Description =
                "The name of the connector to add. e.g. Reductech.EDR.Connectors.StructuredData"
        )]
        string name,
        [Option(
            //LongName  = "configuration",
            ShortName = "c",
            Description =
                "Name of the configuration. By default this is the same as the connector name."
        )]
        string? configuration = null,
        [Option(
            //LongName  = "version",
            ShortName = "v",
            Description =
                "Add the specified version of the connector. By default the latest version will be added."
        )]
        string? version = null,
        [Option(
            //LongName    = "force",
            ShortName   = "f",
            Description = "If a connector already exists, overwrite"
        )]
        bool force = false,
        [Option(Description = "Include prerelease versions of connectors.")]
        bool prerelease = false) => await _connectorManager.Add(
        name,
        configuration,
        version,
        prerelease,
        force,
        ct
    );

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ct"></param>
    /// <param name="name"></param>
    /// <param name="configurationOnly"></param>
    /// <returns></returns>
    [Command(
        Name        = "remove",
        Description = "Remove a connector from EDR"
    )]
    public async Task RemoveConnector(
        CancellationToken ct,
        [Operand(
            Name = "name",
            Description =
                "The name of the connector to add. e.g. Reductech.EDR.Connectors.StructuredData"
        )]
        string name,
        [Option(Description = "Do not remove the installation directory.")]
        bool configurationOnly = false) =>
        await _connectorManager.Remove(name, configurationOnly, ct);
}

}
