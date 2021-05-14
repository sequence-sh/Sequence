using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CommandDotNet;
using ConsoleTables;
using Reductech.EDR.ConnectorManagement;
using Reductech.EDR.Core.Internal.Documentation;

namespace Reductech.EDR
{

/// <summary>
/// Provides commands to list and search available steps
/// </summary>
[Command(
    Name        = "steps",
    Description = "List and search available steps"
)]
public class StepsCommand
{
    private readonly IConnectorManager _connectorManager;

    /// <summary>
    /// Instantiate this command with the specified connector manager.
    /// </summary>
    /// <param name="connectorManager"></param>
    public StepsCommand(IConnectorManager connectorManager) => _connectorManager = connectorManager;

    /// <summary>
    /// List the available steps
    /// </summary>
    [DefaultMethod]
    public async Task List(
        CancellationToken cancellationToken,
        [Option(
            ShortName   = "n",
            Description = "Filter step names using a regular expression"
        )]
        string? name = null,
        [Option(
            ShortName   = "c",
            Description = "Filter connector names using a regular expression"
        )]
        string? connector = null)
    {
        var stepFactoryStore = await _connectorManager.GetStepFactoryStoreAsync(cancellationToken);

        var allSteps = stepFactoryStore
            .Dictionary
            .GroupBy(x => x.Value, x => x.Key)
            .Select(x => new StepWrapper(x));

        if (name != null)
            allSteps = allSteps.Where(s => Regex.IsMatch(s.Name, name, RegexOptions.IgnoreCase));

        if (connector != null)
            allSteps = allSteps.Where(
                s => Regex.IsMatch(s.DocumentationCategory, connector, RegexOptions.IgnoreCase)
            );

        var steps = allSteps.Select(
                s => new ConnectorRow(
                    s.Name,
                    s.DocumentationCategory,
                    s.Summary.Replace("\r", "").Replace("\n", " ")
                )
            )
            .ToList();

        if (steps.Count > 0)
        {
            var maxLen = Console.WindowWidth - steps.Max(s => s.Name.Length + s.Connector.Length)
                                             - 15;

            var formattedSteps = steps.Select(
                s => s with
                {
                    Description = s.Description.Length < maxLen
                        ? s.Description
                        : s.Description.Substring(0, maxLen) + "..."
                }
            );

            ConsoleTable.From(formattedSteps).Write(Format.Minimal);
        }
    }

    private record ConnectorRow(string Name, string Connector, string Description);
}

}
