# E-Discovery Reduct

Reductech EDR is a collection of libraries that automates
cross-application e-discovery and forensic workflows.

The EDR project is a command-line application to run
[Sequences of Steps](#steps-and-sequences) using the
[Sequence Configuration Language (SCL)](#sequence-configuration-language).

EDR includes:

- [Core](https://gitlab.com/reductech/edr/core) which is:
  - An interpreter for the Sequence Configuration Language
  - A collection of application-independent Steps that:
    - Control flow, e.g. If, ForEach, While
    - Manipulate strings, e.g. Append, Concatenate, ChangeCase
    - Enforce data standards and convert between various formats through the use of [Schemas](https://docs.reductech.io/edr/how-to/scl/schemas.html)
    - Create and manipulate [entities](https://docs.reductech.io/edr/how-to/scl/entities.html) (structured data)
- Connectors that interact with various applications, e.g SQL, PowerShell, Nuix, StructuredData
  - Included with EDR is a [connector manager](#connectors)
  - For a full list of available connectors, please see the [Connector Registry](https://gitlab.com/reductech/edr/connector-registry/-/packages)

## Quick Start

1. Download the latest release [here](https://gitlab.com/reductech/edr/edr/-/releases)
2. Unzip the file and open a shell (cmd, pwsh, powershell) of your choice in that directory
3. Run `edr.exe run scl "Print 'Hello world'"`
4. That's it, now for something a bit more useful:
   - [Quick Start](https://docs.reductech.io/edr/how-to/quick-start.html)
   - [Connector Examples](https://docs.reductech.io/edr/examples/core/csv-files.html)

### Running SCL

To run a Sequence from a file:

```powershell
PS > .\edr.exe run .\Examples\core-sequence.scl
```

From the command line:

```powershell
PS > .\edr.exe run scl "- <version> = GetApplicationVersion `n- Print <version>"
```

### Help

To display the available commands and parameters when running edr, use the
`--help` or `-h` argument:

```powershell
PS > .\edr.exe --help
```

To see a list of all the `Steps` available, use the `steps` command:

```powershell
PS > .\edr.exe steps

# To filter by name or connector, pass a regular expression as the first argument
PS > .\edr.exe steps file
```

## Sequence Configuration Language

Workflows in EDR are defined using a custom configuration language.
SCL is designed to be powerful yet easy to pick-up and use.
A quick introduction to the language and its features can be found in the
[documentation](https://docs.reductech.io/edr/how-to/scl/sequence-configuration-language.html).

Here is SCL to remove duplicate rows from a CSV file:

```perl
- FileRead 'C:\temp\data.csv'
| FromCSV
| ArrayDistinct <entity>
| ToCSV
| FileWrite 'C:\temp\data-NoDuplicates.csv'
```

### Steps and Sequences

A `Step` is a unit of work in an application such as
creating a case, ingesting data, searching or exporting data.

A `Sequence` is a series of `Steps` that are executed in order.
EDR allows for data and configuration to be passed between Steps.

### Connectors

EDR uses a connector system to extend functionality to various applications.

By default, EDR comes with the `FileSystem` and `StructuredData` connectors.
All the available connectors can be seen in the [Connector Registry](https://gitlab.com/reductech/edr/connector-registry/-/packages).

To manage connectors, use the `connector` command:

```powershell
PS > .\edr.exe connector

# To list the connectors currently installed, use list
PS > .\edr.exe connector list

# To list all the connectors available in the registry, use find
PS > .\edr.exe connector find

# To install a connector, use add
PS > .\edr.exe connector add Reductech.EDR.Connectors.Sql
```

### VSCode Plugin

To make SCL easier to use, a [Visual Studio Code](https://code.visualstudio.com/)
[SCL plugin](https://marketplace.visualstudio.com/items?itemName=reductech.reductech-scl)
is available that supports syntax highlighting, code completion for
steps and parameters, and error diagnostics.

Search for `SCL` or `reductech` in the VS Code extensions window to install.

## Documentation

Documentation is available at [docs.reductech.io](https://docs.reductech.io)

For details on how to setup various logging targets, including
JSON and Elastic, see the [logging](https://docs.reductech.io/edr/how-to/logging.html)
section of the documentation.

## OS Compatibility

EDR is compatible with any [OS supported by .NET 5](https://github.com/dotnet/core/blob/master/release-notes/5.0/5.0-supported-os.md).

However, we're currently only targeting the `win10-x64` runtime for
our [releases](https://gitlab.com/reductech/edr/edr/-/releases).
