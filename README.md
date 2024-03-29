# Sequence Console

[Sequence®](https://sequence.sh)
is a collection of applications and connectors that automate
e-discovery and forensic workflows using the
[Sequence Configuration Language (SCL)](#sequence-configuration-language).

Sequence Console provides functionality to run SCL and explore the
available connectors from the command-line.

Sequence includes:

- [Core](https://gitlab.com/sequence/core) which is:
  - An interpreter for the Sequence Configuration Language
  - A collection of application-independent steps that:
    - Control flow, e.g. If, ForEach, While
    - Manipulate strings, e.g. Append, Concatenate, ChangeCase
    - Enforce data standards and convert between various formats through the use of [Schemas](https://sequence.sh/docs/schemas)
    - Create and manipulate [entities](https://sequence.sh/docs/entities) (structured data)
- Connectors that interact with various applications, e.g SQL, PowerShell, Nuix, StructuredData
  - Included with Sequence Console is a [connector manager](#connectors)
  - For a full list of available connectors
    - Please see the [Connector Registry](https://gitlab.com/sequence/connector-registry/-/packages)
    - Or check out the [steps documentation](https://sequence.sh/steps/all)

# Download

https://sequence.sh/download

## Quick Start

1. Download the latest release [here](https://sequence.sh/download)
2. Unzip the file and open a shell (cmd, pwsh, powershell) of your choice in that directory
3. Run `sequence run scl "Print 'Hello world'"`
4. That's it, now for something a bit more useful:
   - [Quick Start](https://sequence.sh/docs/quick-start)
   - [Connector Examples](https://sequence.sh/docs/examples/connectors/structureddata/csv-files)

# Try SCL and Core in your browser

https://sequence.sh/playground

### Running SCL

To run a Sequence from a file:

```powershell
PS > ./sequence run ./Examples/core-sequence.scl
```

From the command line:

```powershell
PS > ./sequence run scl "- <version> = GetApplicationVersion `n- Print <version>"
```

### Help

To display the available commands and parameters when running Sequence, use the
`--help` or `-h` argument:

```powershell
PS > ./sequence --help
```

To see a list of all the `Steps` available, use the `steps` command:

```powershell
PS > ./sequence steps

# To filter by name or connector, pass a regular expression as the first argument
PS > ./sequence steps file
```

## Sequence Configuration Language

Workflows in Sequence are defined using a custom configuration language.
SCL is designed to be powerful yet easy to pick-up and use.
A quick introduction to the language and its features can be found in the
[documentation](https://sequence.sh/docs/sequence-configuration-language).

Here is SCL to remove duplicate rows from a CSV file:

```perl
- FileRead 'C:\temp\data.csv'
| FromCSV
| ArrayDistinct <entity>
| ToCSV
| FileWrite 'C:\temp\data-NoDuplicates.csv'
```

### Steps and Sequences

A `step` is a unit of work in an application such as
creating a case, ingesting data, searching or exporting data.

A `sequence` is a series of `steps` that are executed in order.
Sequence allows for data and configuration to be passed between steps.

### Connectors

Sequence uses a connector system to extend functionality to various applications.

By default, Sequence comes with the `FileSystem` and `StructuredData` connectors.
All the available connectors can be seen in the [Connector Registry](https://gitlab.com/sequence/connector-registry/-/packages).

To manage connectors, use the `connector` command:

```powershell
PS > ./sequence connector

# To list the connectors currently installed, use list
PS > ./sequence connector list

# To list all the connectors available in the registry, use find
PS > ./sequence connector find

# To install a connector, use add
PS > ./sequence connector add Sequence.Connectors.Sql

# It's also possible to use a search string to add or update connectors
PS > ./sequence connector add Sql

# To update a connector, use update
PS > ./sequence connector update Sequence.Connectors.Sql
```

## Injecting Variables

You can inject variables from the CLI into SCL.

```powershell
PS > ./sequence run scl "log <a> + <b>" --variable "<a> = 1" -v "<b> = 2"
```

## VSCode Plugin

To make SCL easier to use, a [Visual Studio Code](https://code.visualstudio.com/)
[SCL plugin](https://marketplace.visualstudio.com/items?itemName=reductech.reductech-scl)
is available that supports syntax highlighting, code completion for
steps and parameters, and error diagnostics.

Search for `SCL` or `reductech` in the VS Code extensions window to install.

## Documentation

Documentation is available at https://sequence.sh

For details on how to setup various logging targets, including
JSON and Elastic, see the [logging](https://sequence.sh/docs/logging)
section of the documentation.

## Settings

Settings can be controlled in the `appsettings.json` file.

| Setting                 | Type     | Description                                                                                             |
| ----------------------- | -------- | ------------------------------------------------------------------------------------------------------- |
| `LogAnalytics`          | `bool`   | If true, information about steps used and step durations will be logged.                                |
| `PerformanceMonitoring` | `object` | Controls performance monitoring. Only available on Windows. See below for more information              |
| `nlog`                  | `object` | Controls logging settings. See [documentation](https://sequence.sh/docs/logging/) for more information. |

### Performance Monitoring

Performance Monitoring is controlled by the `PerformanceMonitoring` configuration section. It is only available when running on Windows.

It has the following properties:

| Setting                 | Type     | Description                                                                                                                                                                                                      |
| ----------------------- | -------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `Enable`                | `bool`   | Whether performance monitoring is enabled.                                                                                                                                                                       |
| `MeasurementIntervalMs` | `int`    | How frequently the performance is measured.                                                                                                                                                                      |
| `LoggingIntervalMs`     | `int`    | How frequently the performance is logged.                                                                                                                                                                        |
| `Categories`            | `object` | Performance categories to log. Information about performance categories can be found [here](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.performancecountercategory?view=dotnet-plat-ext-6.0). |
| `MeasureAllCategories`  | `bool`   | Whether all performance categories should be logged.                                                                                                                                                             |

The following is an example of the `PerformanceMonitoring` section:

```json
  "PerformanceMonitoring": {
    "Enable": true,
    "MeasurementIntervalMs": 100,
    "LoggingIntervalMs": 10000,
    "MeasureAllCategories": false,

    "Categories": {
      "Process": [
        "% Processor Time",
        "Working Set"
      ]
    }
  },
```

## OS Compatibility

Sequence is compatible with any [OS supported by .NET 6](https://github.com/dotnet/core/blob/main/release-notes/6.0/supported-os.md).

However, we're currently only targeting the `win10-x64` runtime for
our [releases](https://gitlab.com/sequence/console/-/releases).

For other OSes, please [build from source](https://sequence.sh/docs/build-from-source).
