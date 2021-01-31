## v0.4.0 (2021-01-31)

Major rework of the configuration language and data streaming features
so lots of **breaking changes**:

- Moved from YAML-based configuration to a custom configuration
  language called SCL (Sequence Configuration Language)
  - Using a custom [ANTLR](https://www.antlr.org/) grammar to parse the configuration language
  - The new language is similar to YAML
- Consolidated entity streams, lists and array into a single datatype - `Array<T>`
  - There are no longer separate Steps for arrays and entity streams (e.g. `ForEach`, `IsEmpty`, `Length`)
- Consolidated strings and data streams into a single datatype - `StringStream`
- Reworked logging and exceptions
  - More frequent and more consistent logging
  - Added step scopes
  - Added localization features
  - Added support for appending custom metadata

Also:

- Added Powershell connector to allow for inline scripts to be executed in Sequences
- Added Step to execute inline Ruby scripts when running the Nuix Connector
- Added steps for working with JSON and IDX
- Documentation has now been moved to https://docs.reductech.io
- Update to .NET 5.0

For more details see the release notes for core and connectors:

- [Core v0.4.0](https://gitlab.com/reductech/edr/core/-/releases/v0.4.0)
- [Nuix Connector v0.4.0](https://gitlab.com/reductech/edr/connectors/nuix/-/releases/v0.4.0)
- [Pwsh Connector v0.4.0](https://gitlab.com/reductech/edr/connectors/pwsh/-/releases/v0.4.0)

### New Features

- Add Pwsh connector to EDR #42
- Change settings to use Dynamic Settings #38
- Print results of steps that do not return type Unit, to make the config more concise #27
- Use dependency injection for connector and logging configuration #22
- Update to latest version of Core and Connectors, to include new language features #32
- Upgrade to dot net 5 #28

### Bug Fixes

- Package stage no longer works after .net 5 update #33
- App and nlog config files do not work with PublishSingleFile #21

### Maintenance

- Add nuixconnectorscript to the package artifacts #39
- Set default log level to Info #37
- Add .editorconfig file and standardize formatting #35
- Temporarily disable dotnet-stryker CI job #31
- Embed debug symbols in the single file application #30

## v0.3.0 (2020-11-27)

### Core

- Added Entities and EntityStreams along with LINQ-style methods to manipulate them.
- Added Entity Schemas to allow conversion between different formats/types
- Added Steps to convert `EntityStream` to/from concordance and CSV

### Connectors

The way the Nuix connector interacts with Nuix has been rewritten - functions
and data are now streamed to Nuix so there is no longer a requirement
for script composition and there is a performance increase when dealing with
conditional/flow operators.

### Breaking Changes

- This version only supports Nuix 8 (to be resolved in `0.4.0`)
- Step and argument names have changed to make them more
  consistent. Step names now follow the convention of _NamespaceAction_, e.g.
  `ArrayLength`, `ArraySort`, `EntityMap`, `EntityStreamSort`

### Documentation

- Add Examples to make it easier to demo #25

### Maintenance

- Add Release issue template #19
- Use template ci config, so that it's easier to maintain #20
- Add ci stage to sign binaries #24

## v0.2.1 (2020-11-03)

### New Features

- Create Unit Tests so we can be confident the Console app is working #13
- Make error messages more verbose, so technicians can debug their yaml more easily #14
- Move in ConsoleMethods from Core #12
- Use NLog for logging, so that technicians can customise log output #11

### Bug Fixes

- Output to console does not work #6

### Maintenance

- Add issue templates #8

### Other

- Update to the latest version of Core #10
- 'Execute' should be the default method to save technicians from extra typing #7

## v0.2.0 (2020-10-02)

- The paradigm by which Processes are defined has changed from functional to procedural
- A 'Process' is now a 'Step'
- Many new General Steps have been added to support the new paradigm (e.g. SetVariable, GetVariable, MathOperator)
- The yaml specification has changed entirely so yaml from the previous versions will not work with this version (please see [readme](README.md))
- Idol processes have been removed. They will return again in a later version.

For further details, please see the following epics:

- [Version 0.2.0](https://gitlab.com/groups/reductech/-/epics/5)
- [Change to Procedural Paradigm](https://gitlab.com/groups/reductech/edr/-/epics/6)

### Maintenance

- Update to version 0.2.0 of Core and Nuix #4

### Other

- Adjust namespaces to match new hierarchy #2

## v0.1.0 (2020-03-13)

Initial release. Console application to integrate EDR connectors.
