# E-Discovery Reduct

Reductech EDR is a collection of libraries that automates
cross-application e-discovery and forensic workflows.

The EDR project is a command-line application to run Sequences of Steps using
the sequence configuration language (SCL).

EDR includes:

- [Core](https://gitlab.com/reductech/edr/core) which is:
  - An interpreter for the Sequence Configuration Language
  - A collection of application-independent Steps that:
    - Can be used to import/export data and structure workflows
    - Work with various file formats: CSV, Json, Concordance
    - Manipulate strings, e.g. Append, Concatenate, ChangeCase
    - Enforce data standards and convert between various formats through the use of Schemas
    - Create and manipulate entities (structured objects that represent data)
    - Control flow, e.g. If, ForEach, While
- Connectors that interact with various applications
  - [Nuix](https://gitlab.com/reductech/edr/connectors/nuix)
  - [PowerShell](https://gitlab.com/reductech/edr/connectors/pwsh)
  - [SQL](https://gitlab.com/reductech/edr/connectors/sql)

A `Step` is a unit of work in an application such as
creating a case, ingesting data, searching or exporting data
A `Sequence` is a series of `Steps` that are executed in order.
EDR allows for data and configuration to be passed between Steps.

## Quick Start

1. Download the latest release from the [Releases](https://gitlab.com/reductech/edr/edr/-/releases) page
2. Unzip the file and open a shell (cmd, pwsh, powershell) of your choice in that directory
3. Run `EDR.exe -s "- Print 'Hello world'"`
4. That's it, now try something a bit more useful in the [Quick Start](https://docs.reductech.io/howto/quick-start.html)

## Documentation

Documentation is available at [docs.reductech.io](https://docs.reductech.io)

## OS Compatibility

EDR is compatible with any [OS supported by .NET 5](https://github.com/dotnet/core/blob/master/release-notes/5.0/5.0-supported-os.md).

However, we're currently only targeting the `win-x64` runtime identifier for
our [releases](https://gitlab.com/reductech/edr/edr/-/releases).

## Builds that include the PowerShell connector

PowerShell is not currently compatible with single-file executables
created by .NET 5 (see reductech/edr/connectors/pwsh#5). As a temporary
workaround, there is an additional Windows 10 x64 package of `EDR`
that is compatible with the powershell connector. It has the `Win10x64`
suffix.
