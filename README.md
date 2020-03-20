[![pipeline status](https://gitlab.com/reductech/e-discovery/edr/badges/master/pipeline.svg)](https://gitlab.com/reductech/e-discovery/edr/-/commits/master)
[![coverage report](https://gitlab.com/reductech/e-discovery/edr/badges/master/coverage.svg)](https://gitlab.com/reductech/e-discovery/edr/-/commits/master)

# Introduction

This is a project that lets you automate various E-Discovery and Forensics tasks. It is currently only connected with NUIX but we hope to add more connectors soon.

There is a console app which runs all the processes individually and you can also provide yaml containing a sequence of processes to perform.


# Quickstart

* [Download Latest Build](https://gitlab.com/reductech/e-discovery/edr/-/jobs/artifacts/master/download?job=release)
* Modify EDR.dll.config - set NuixUseDongle and NuixExeConsolePath 
* Create your.yaml file - see below for an example
* Run cmd.exe and cd into the publish folder 
* `>edr.exe  RunProcessFromYaml -yamlpath MyFile.yaml`


# Yaml example

The following yaml will create a case, add evidence from both a file and a concordance, tag some of the evidence and move it to a production set and then export the production set


```yaml
!Sequence
#Sets the CasePath property of every process that needs it
Defaults: 
    CasePath: C:/Cases/MyCase

#This list will be ignored but you can use it to set yaml anchors
Ignore:
    - &CaseName Case 1
    - &Investigator Joe Bloggs
    - &EvidencePath C:/Evidence/My Evidence
    - &Custodian John Smith
    - &FolderName Evidence Folder 1
    - &ReportsFolder C:/Reports/Case1
    - &SearchTagCSVPath C:/Documents/Searches.csv
    - &ExportPath C:/Exports/Case1

Steps:
- !NuixCreateCase 
#Create a new Case
  CaseName: *CaseName
  Investigator: *Investigator
- !NuixAddItem
  Path: *EvidencePath
  Custodian: *Custodian
  FolderName: *FolderName
- !NuixCreateReport
  OutputFolder: *ReportsFolder
- !NuixPerformOCR
  OCRProfileName: OCR Profile
- !Loop
#For each row in this CSV, do a search and tag the results
  For: !CSV
    CSVFilePath: *SearchTagCSVPath
    InjectColumns:
      SearchTerm:
        Property: SearchTerm
      Tag:
        Property: Tag
    Delimiter: ','
    HasFieldsEnclosedInQuotes: false
  Do: !NuixSearchAndTag
  #These properties will be injected from the CSV
    SearchTerm: _
    Tag: _
- !NuixAddToItemSet
  ItemSetName: TaggedItems
  SearchTerm: Tag:*
- !NuixAddToProductionSet
  ProductionSetName: &ProductionSetName TaggedItemsProductionSet
  SearchTerm: ItemSet:TaggedItems
- !NuixExportConcordance
  MetadataProfileName: Default
  ProductionSetName: *ProductionSetName
  ExportPath: *ExportPath

```