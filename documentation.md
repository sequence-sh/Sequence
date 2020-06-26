<a name="RubyScriptProcess"></a>
# Nuix Processes
<a name="NuixAddConcordance"></a>
## NuixAddConcordance

**Unit**

*Requires Nuix Version 7.6*

*Requires Nuix Feature 'CASE_CREATION'*

*Requires Nuix Feature 'METADATA_IMPORT'*

|Parameter             |Type    |Required|Summary|
|:--------------------:|:------:|:------:|:-----:|
|ConcordanceProfileName|`string`|☑️      |       |
|ConcordanceDateFormat |`string`|☑️      |       |
|FilePath              |`string`|☑️      |       |
|Custodian             |`string`|☑️      |       |
|Description           |`string`|        |       |
|FolderName            |`string`|☑️      |       |
|CasePath              |`string`|☑️      |       |

<a name="NuixAddItem"></a>
## NuixAddItem

**Unit**

*Requires Nuix Version 5.0*

*Requires Nuix Feature 'CASE_CREATION'*

|Parameter            |Type    |Required|Summary|Default Value                               |Requirements|
|:-------------------:|:------:|:------:|:-----:|:------------------------------------------:|:----------:|
|Path                 |`string`|☑️      |       |                                            |            |
|Custodian            |`string`|☑️      |       |                                            |            |
|Description          |`string`|        |       |                                            |            |
|FolderName           |`string`|☑️      |       |                                            |            |
|CasePath             |`string`|☑️      |       |                                            |            |
|PasswordFilePath     |`string`|☑️      |       |                                            |Nuix 7.6    |
|ProcessingProfileName|`string`|        |       |The default processing profile will be used.|Nuix 7.6    |
|ProcessingProfilePath|`string`|        |       |The default processing profile will be used.|Nuix 7.6    |

<a name="NuixAddToItemSet"></a>
## NuixAddToItemSet

**Unit**

*Requires Nuix Version 5.0*

*Requires Nuix Feature 'ANALYSIS'*

|Parameter           |Type                                         |Required|Summary|Default Value|
|:------------------:|:-------------------------------------------:|:------:|:-----:|:-----------:|
|ItemSetName         |`string`                                     |☑️      |       |             |
|SearchTerm          |`string`                                     |☑️      |       |             |
|CasePath            |`string`                                     |☑️      |       |             |
|ItemSetDeduplication|[ItemSetDeduplication](#ItemSetDeduplication)|        |       |Default      |
|ItemSetDescription  |`string`                                     |        |       |             |
|Order               |`string`                                     |        |       |             |
|DeduplicateBy       |[DeduplicateBy](#DeduplicateBy)              |        |       |Individual   |
|Limit               |`int`?                                       |        |       |             |
|CustodianRanking    |List<`string`>                               |        |       |             |

<a name="NuixAddToProductionSet"></a>
## NuixAddToProductionSet

**Unit**

*Requires Nuix Version 7.2*

*Requires Nuix Feature 'PRODUCTION_SET'*

|Parameter            |Type    |Required|Summary|Default Value                               |Requirements|
|:-------------------:|:------:|:------:|:-----:|:------------------------------------------:|:----------:|
|ProductionSetName    |`string`|☑️      |       |                                            |            |
|SearchTerm           |`string`|☑️      |       |                                            |            |
|CasePath             |`string`|☑️      |       |                                            |            |
|Description          |`string`|        |       |                                            |            |
|Order                |`string`|        |       |                                            |            |
|Limit                |`int`?  |        |       |                                            |            |
|ProductionProfileName|`string`|        |       |The default processing profile will be used.|Nuix 7.2    |
|ProductionProfilePath|`string`|        |       |The default processing profile will be used.|Nuix 7.6    |

<a name="NuixAnnotateDocumentIdList"></a>
## NuixAnnotateDocumentIdList

**Unit**

*Requires Nuix Version 7.4*

*Requires Nuix Feature 'PRODUCTION_SET'*

|Parameter        |Type    |Required|Summary|
|:---------------:|:------:|:------:|:-----:|
|ProductionSetName|`string`|☑️      |       |
|CasePath         |`string`|☑️      |       |
|DataPath         |`string`|☑️      |       |

<a name="NuixAssertPrintPreviewState"></a>
## NuixAssertPrintPreviewState

**Unit**

*Requires Nuix Version 5.2*

*Requires Nuix Feature 'ANALYSIS'*

*Requires Nuix Feature 'PRODUCTION_SET'*

|Parameter        |Type                                   |Required|Summary|Default Value|
|:---------------:|:-------------------------------------:|:------:|:-----:|:-----------:|
|ExpectedState    |[PrintPreviewState](#PrintPreviewState)|        |       |All          |
|ProductionSetName|`string`                               |☑️      |       |             |
|CasePath         |`string`                               |☑️      |       |             |

<a name="NuixAssignCustodian"></a>
## NuixAssignCustodian

**Unit**

*Requires Nuix Version 5.0*

*Requires Nuix Feature 'ANALYSIS'*

|Parameter |Type    |Required|Summary|
|:--------:|:------:|:------:|:-----:|
|Custodian |`string`|☑️      |       |
|SearchTerm|`string`|☑️      |       |
|CasePath  |`string`|☑️      |       |

<a name="NuixCountItems"></a>
## NuixCountItems

**Unit**

*Requires Nuix Version 5.0*

|Parameter |Type    |Required|Summary|
|:--------:|:------:|:------:|:-----:|
|CasePath  |`string`|☑️      |       |
|SearchTerm|`string`|☑️      |       |

<a name="NuixCreateCase"></a>
## NuixCreateCase

**Unit**

*Requires Nuix Version 5.0*

*Requires Nuix Feature 'CASE_CREATION'*

|Parameter   |Type    |Required|Summary|
|:----------:|:------:|:------:|:-----:|
|CaseName    |`string`|☑️      |       |
|CasePath    |`string`|☑️      |       |
|Investigator|`string`|☑️      |       |
|Description |`string`|        |       |

<a name="NuixCreateIrregularItemsReport"></a>
## NuixCreateIrregularItemsReport

**Unit**

*Requires Nuix Version 5.0*

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|CasePath |`string`|☑️      |       |

<a name="NuixCreateNRTReport"></a>
## NuixCreateNRTReport

**Unit**

*Requires Nuix Version 7.4*

*Requires Nuix Feature 'ANALYSIS'*

|Parameter        |Type    |Required|Summary|
|:---------------:|:------:|:------:|:-----:|
|CasePath         |`string`|☑️      |       |
|NRTPath          |`string`|☑️      |       |
|OutputFormat     |`string`|☑️      |       |
|LocalResourcesURL|`string`|☑️      |       |
|OutputPath       |`string`|☑️      |       |

<a name="NuixCreateReport"></a>
## NuixCreateReport

**Unit**

*Requires Nuix Version 6.2*

*Requires Nuix Feature 'ANALYSIS'*

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|CasePath |`string`|☑️      |       |

<a name="NuixCreateTermList"></a>
## NuixCreateTermList

**Unit**

*Requires Nuix Version 5.0*

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|CasePath |`string`|☑️      |       |

<a name="NuixDoesCaseExists"></a>
## NuixDoesCaseExists

**Unit**

*Requires Nuix Version 5.0*

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|CasePath |`string`|☑️      |       |

<a name="NuixExportConcordance"></a>
## NuixExportConcordance

**Unit**

*Requires Nuix Version 7.2*

*Requires Nuix Feature 'EXPORT_ITEMS'*

*Requires Nuix Feature 'PRODUCTION_SET'*

|Parameter        |Type    |Required|Summary|
|:---------------:|:------:|:------:|:-----:|
|ProductionSetName|`string`|☑️      |       |
|ExportPath       |`string`|☑️      |       |
|CasePath         |`string`|☑️      |       |

<a name="NuixExtractEntities"></a>
## NuixExtractEntities

**Unit**

*Requires Nuix Version 5.0*

|Parameter   |Type    |Required|Summary|
|:----------:|:------:|:------:|:-----:|
|OutputFolder|`string`|☑️      |       |
|CasePath    |`string`|☑️      |       |

<a name="NuixGeneratePrintPreviews"></a>
## NuixGeneratePrintPreviews

**Unit**

*Requires Nuix Version 5.2*

*Requires Nuix Feature 'PRODUCTION_SET'*

|Parameter        |Type    |Required|Summary|
|:---------------:|:------:|:------:|:-----:|
|ProductionSetName|`string`|☑️      |       |
|CasePath         |`string`|☑️      |       |

<a name="NuixGetItemProperties"></a>
## NuixGetItemProperties

**Unit**

*Requires Nuix Version 6.2*

|Parameter    |Type    |Required|Summary|
|:-----------:|:------:|:------:|:-----:|
|CasePath     |`string`|☑️      |       |
|SearchTerm   |`string`|☑️      |       |
|PropertyRegex|`string`|☑️      |       |
|ValueRegex   |`string`|        |       |

<a name="NuixImportDocumentIds"></a>
## NuixImportDocumentIds

**Unit**

*Requires Nuix Version 7.4*

*Requires Nuix Feature 'PRODUCTION_SET'*

|Parameter                    |Type    |Required|Summary|Default Value|
|:---------------------------:|:------:|:------:|:-----:|:-----------:|
|ProductionSetName            |`string`|☑️      |       |             |
|CasePath                     |`string`|☑️      |       |             |
|AreSourceProductionSetsInData|`bool`  |        |       |False        |
|DataPath                     |`string`|☑️      |       |             |

<a name="NuixMigrateCase"></a>
## NuixMigrateCase

**Unit**

*Requires Nuix Version 5.0*

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|CasePath |`string`|☑️      |       |

<a name="NuixPerformOCR"></a>
## NuixPerformOCR

**Unit**

*Requires Nuix Version 7.6*

*Requires Nuix Feature 'OCR_PROCESSING'*

|Parameter     |Type    |Required|Summary|Default Value                                                                                                                                      |Requirements|
|:------------:|:------:|:------:|:-----:|:-------------------------------------------------------------------------------------------------------------------------------------------------:|:----------:|
|CasePath      |`string`|☑️      |       |                                                                                                                                                   |            |
|SearchTerm    |`string`|        |       |NOT flag:encrypted AND ((mime-type:application/pdf AND NOT content:\*) OR (mime-type:image/\* AND ( flag:text_not_indexed OR content:( NOT \* ) )))|            |
|OCRProfileName|`string`|        |       |The default profile will be used.                                                                                                                  |            |
|OCRProfilePath|`string`|        |       |The default profile will be used.                                                                                                                  |Nuix 7.6    |

<a name="NuixRemoveFromProductionSet"></a>
## NuixRemoveFromProductionSet

**Unit**

*Requires Nuix Version 5.0*

*Requires Nuix Feature 'PRODUCTION_SET'*

|Parameter        |Type    |Required|Summary|Default Value             |
|:---------------:|:------:|:------:|:-----:|:------------------------:|
|ProductionSetName|`string`|☑️      |       |                          |
|SearchTerm       |`string`|        |       |All items will be removed.|
|CasePath         |`string`|☑️      |       |                          |

<a name="NuixReorderProductionSet"></a>
## NuixReorderProductionSet

**Unit**

*Requires Nuix Version 5.2*

*Requires Nuix Feature 'PRODUCTION_SET'*

|Parameter        |Type                                             |Required|Summary|Default Value|
|:---------------:|:-----------------------------------------------:|:------:|:-----:|:-----------:|
|ProductionSetName|`string`                                         |☑️      |       |             |
|CasePath         |`string`                                         |☑️      |       |             |
|SortOrder        |[ProductionSetSortOrder](#ProductionSetSortOrder)|        |       |Position     |

<a name="NuixSearchAndTag"></a>
## NuixSearchAndTag

**Unit**

*Requires Nuix Version 5.0*

*Requires Nuix Feature 'ANALYSIS'*

|Parameter |Type    |Required|Summary|
|:--------:|:------:|:------:|:-----:|
|Tag       |`string`|☑️      |       |
|SearchTerm|`string`|☑️      |       |
|CasePath  |`string`|☑️      |       |

<a name="IdolProcess"></a>
# Introspect Processes
<a name="ConvertConcordanceToIDX"></a>
## ConvertConcordanceToIDX

**List of String**

|Parameter  |Type    |Required|Summary|
|:---------:|:------:|:------:|:-----:|
|Concordance|`string`|☑️      |       |

<a name="ConvertToConcordance"></a>
## ConvertToConcordance

**String**

|Parameter|Type                                  |Required|Summary|
|:-------:|:------------------------------------:|:------:|:-----:|
|Records  |IReadOnlyCollection<[Record](#Record)>|☑️      |       |

<a name="IntrospectAdd"></a>
## IntrospectAdd

**Int32**

*Requires an IDOL Instance*

|Parameter                   |Type                         |Required|Summary|Default Value                                           |Example                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |Recommended Range                                 |See Also                                                                                                                                                                                                                                                     |URL                                                                                                                                                                                                     |Value Delimiter|
|:--------------------------:|:---------------------------:|:------:|:-----:|:------------------------------------------------------:|:-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:------------------------------------------------:|:-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:-------------:|
|IndexFile                   |`string`                     |        |       |                                                        |D:\\Files\\Data\\myfile.idx                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |                                                  |FileName                                                                                                                                                                                                                                                     |[IndexFile](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_IndexFile.htm)                                                        |               |
|ACLFields                   |IReadOnlyCollection<`string`>|        |       |                                                        |\*/AUTONOMYMETADATA<br>                        In this example, IDOL Server reads ACLs from any fields that are called AUTONOMYMETADATA.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |                                                  |ACLType configuration parameter                                                                                                                                                                                                                              |[ACLFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_ACLFields.htm)                                                        |,              |
|CantHaveFields              |IReadOnlyCollection<`string`>|        |       |                                                        |\*/StandardHeader<br>                        In this example, any StandardHeader fields in a document are discarded before the document is indexed.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |                                                  |MustHaveFields                                                                                                                                                                                                                                               |[CantHaveFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_CantHaveFields.htm)                                              |,              |
|CreateDatabase              |`bool`?                      |        |       |False                                                   |True                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |                                                  |DREDbName<br>                        <br>                        DatabaseFields<br>                        <br>                        DatabaseType configuration parameter                                                                                  |[CreateDatabase](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_CreateDatabase.htm)                                              |               |
|DatabaseFields              |IReadOnlyCollection<`string`>|        |       |                                                        |Document/DREDBName,\*/myDB<br>                        In this example, IDOL Server indexes the document into the database whose name is contained in any DREDbName field below the Document level and whose name is contained in any fields called myDB.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |                                                  |DREDbName configuration parameter                                                                                                                                                                                                                            |[DatabaseFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_DatabaseFields.htm)                                              |,              |
|DateFields                  |IReadOnlyCollection<`string`>|        |       |                                                        |Document/DREDate,\*/myDocDate<br>                        In this example, IDOL Server extracts dates from any fields that are called DREDate that are contained below the Document level and from any fields that are called myDocdate.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |                                                  |DateType configuration parameter                                                                                                                                                                                                                             |[DateFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_DateFields.htm)                                                      |,              |
|Delete                      |`bool`?                      |        |       |False                                                   |true                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |                                                  |                                                                                                                                                                                                                                                             |[Delete](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_Delete.htm)                                                              |               |
|DocumentDelimiters          |IReadOnlyCollection<`string`>|        |       |                                                        |\*/DOCUMENT,\*/SPEECH<br>                        In this example, the beginning and end of individual documents in a file is marked by opening and closing DOCUMENT and SPEECH tags.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |                                                  |DocumentDelimiterCSVs configuration parameter                                                                                                                                                                                                                |[DocumentDelimiters](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_DocumentDelimiters.htm)                                      |,              |
|DocumentFormat              |`string`                     |        |       |                                                        |XML                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |                                                  |                                                                                                                                                                                                                                                             |[DocumentFormat](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_DocumentFormat.htm)                                              |               |
|DREDbName                   |`string`                     |        |       |                                                        |News                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |                                                  |DatabaseFields                                                                                                                                                                                                                                               |[DREDbName](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_DREDbName.htm)                                                        |               |
|ExpiryDateFields            |IReadOnlyCollection<`string`>|        |       |                                                        |Document/DREExpiryDate,\*/myExpiryDate<br>                        In this example, IDOL Server reads the expiry date from any DREExpiryDate field below the Document level and from any fields called myExpiryDate.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |                                                  |ExpireDateType configuration parameter                                                                                                                                                                                                                       |[ExpiryDateFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_ExpiryDateFields.htm)                                          |,              |
|FileName                    |`string`                     |        |       |                                                        |C:\\IndexFiles\\MyIDX.idx                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |                                                  |IndexFile                                                                                                                                                                                                                                                    |[FileName](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_FileName.htm)                                                          |               |
|FlattenIndexFields          |IReadOnlyCollection<`string`>|        |       |                                                        |<documents><br>   <article id="_21498602"><br>      <url>http://example.com/21490.html</url><br>      <hltext_display>The history of pharmacogenetics </hltext_display><br>      <source>Science Online</source><br>      <media_type>text</media_type><br>      <subject><br>         <text>The prologue to pharmacogenetics began to play out around 1850 and spanned some 60 years into the 1900s.</text><br>         <text>In 1953, the molecular basis of heredity, the double helix of DNA, was described.</text><br>      </subject><br>   <valid_time>Jul 13 2001 5:00AM</valid_time><br>   </article><br></documents><br>                        If you specify FlattenIndexFields=\*/subject, and index the XML above, any content that a subject field or a field within a subject field includes is indexed as the content of the subject field.<br>                        You must also have a field configuration that configures the \*/subject and \*/subject/\* field paths as Index type, or you must set IndexField=\*/subject,\*/subject/\* in the index action.<br>                        If you now query for a particular term in the subject field that is actually contained in a level below the subject field, for example the term pharmacogenetics, the above text is returned. If you had not flattened the subject field the query would fail, as the subject field itself does not contain this term.|                                                  |FlattenIndexType configuration parameter                                                                                                                                                                                                                     |[FlattenIndexFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_FlattenIndexFields.htm)                                      |,              |
|IDXFieldPrefix              |`string`                     |        |       |The value of the IDXFieldPrefix configuration parameter.|DOCUMENT                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |                                                  |IDXFieldPrefix configuration parameter                                                                                                                                                                                                                       |[IDXFieldPrefix](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_IDXFieldPrefix.htm)                                              |               |
|IndexFields                 |IReadOnlyCollection<`string`>|        |       |                                                        |\*/DRECONTENT,\*/DRETITLE<br>                        In this example, DRECONTENT and DRETITLE fields are stored as Index fields in IDOL Server                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |                                                  |Index configuration parameter                                                                                                                                                                                                                                |[IndexFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_IndexFields.htm)                                                    |,              |
|IgnoreMaxPendingItems       |`bool`?                      |        |       |False                                                   |True                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |                                                  |Index Queue Configuration Parameters                                                                                                                                                                                                                         |[IgnoreMaxPendingItems](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/../../Actions/SharedParameters/_ACI_IgnoreMaxPendingItems.htm)|               |
|IndexUID                    |`string`                     |        |       |                                                        |12345abcd                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |                                                  |Document Tracking Configuration Parameters                                                                                                                                                                                                                   |[IndexUID](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/../../Actions/SharedParameters/_ACI_IndexUID.htm)                          |               |
|KeepExisting                |`bool`?                      |        |       |False                                                   |True                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |                                                  |KillDuplicatesDB                                                                                                                                                                                                                                             |[KeepExisting](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_KeepExisting.htm)                                                  |               |
|KillDuplicates              |`string`                     |        |       |                                                        |REFERENCE                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |                                                  |KeepExisting<br>                        <br>                        KillDuplicatesDB<br>                        <br>                        KillDuplicates configuration parameter                                                                           |[KillDuplicates](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_KillDuplicates.htm)                                              |               |
|KillDuplicatesDB            |`string`                     |        |       |                                                        |dedup                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |                                                  |DREADD index action KillDuplicates parameter<br>                        DREADDDATA index action KillDuplicatesOption parameter<br>                        KillDuplicatesDBField                                                                              |[KillDuplicatesDB](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_KillDuplicatesDB.htm)                                          |               |
|KillDuplicatesDBField       |`string`                     |        |       |                                                        |DATABASE                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |                                                  |DREADD index action KillDuplicates parameter<br>                        DREADDDATA index action KillDuplicatesOption parameter<br>                        KillDuplicatesDB                                                                                   |[KillDuplicatesDBField](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_KillDuplicatesDBField.htm)                                |               |
|KillDuplicatesMatchDBs      |`string`                     |        |       |                                                        |DB1+DB2                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |                                                  |DREADD index action KillDuplicates parameter<br>                        DREADDDATA index action KillDuplicatesOption parameter<br>                        KillDuplicatesDB<br>                        <br>                        KillDuplicatesMatchTargetDB|[KillDuplicatesMatchDBs](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_KillDuplicatesMatchDBs.htm)                              |               |
|KillDuplicatesMatchTargetDB |`bool`?                      |        |       |False                                                   |false                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |                                                  |DREADD index action KillDuplicates parameter<br>                        DREADDDATA index action KillDuplicatesOption parameter<br>                        KillDuplicatesDB<br>                        <br>                        KillDuplicatesMatchDBs     |[KillDuplicatesMatchTargetDB](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_KillDuplicatesMatchTarge.htm)                       |               |
|KillDuplicatesPreserveFields|`string`                     |        |       |                                                        |\*/SAVEDATA                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |                                                  |DREADD index action KillDuplicates parameter<br>                        DREADDDATA index action KillDuplicatesOption parameter<br>                        KillDuplicatesPreserveFields configuration parameter                                               |[KillDuplicatesPreserveFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_KillDuplicatesPreserveFi.htm)                      |               |
|LanguageFields              |IReadOnlyCollection<`string`>|        |       |                                                        |Document/DRELanguageType,\*/myLanguageType<br>                        In this example, IDOL Server reads the document's language type from any DRELanguageType field below the Document level and any myLanguageType fields.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           |                                                  |LanguageType configuration parameter                                                                                                                                                                                                                         |[LanguageFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_LanguageFields.htm)                                              |,              |
|LanguageType                |`string`                     |        |       |                                                        |myEnglish<br>                        In this example, the file is indexed with the language type myEnglish. The way IDOL Server handles this language type is determined by the way it has been defined in the IDOL Server configuration file (that is by the settings that you have associated with this language type in the configuration file).                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |                                                  |Language Types Configuration Parameters                                                                                                                                                                                                                      |[LanguageType](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_LanguageType.htm)                                                  |               |
|MustHaveFields              |IReadOnlyCollection<`string`>|        |       |                                                        |\*/DRECONTENT,\*/DRETITLE<br>                        In this example, IDOL Server only stores a document's DRECONTENT and DRETITLE fields.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             |                                                  |CantHaveFields<br>                        MustHaveFieldCSVs configuration parameter                                                                                                                                                                          |[MustHaveFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_MustHaveFields.htm)                                              |,              |
|Priority                    |`long`?                      |        |       |0                                                       |5                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      |Minimum: 0<br>                        Maximum: 100|                                                                                                                                                                                                                                                             |[Priority](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/../../Actions/SharedParameters/_ACI_Priority.htm)                          |               |
|SectionFields               |IReadOnlyCollection<`string`>|        |       |                                                        |Document/DRESection,\*/mySection<br>                        In this example, any DRESection field below the Document level and any mySection fields indicate the start of a new section.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |                                                  |SectionBreakType configuration parameter                                                                                                                                                                                                                     |[SectionFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_SectionFields.htm)                                                |,              |
|SecurityFields              |IReadOnlyCollection<`string`>|        |       |                                                        |Document/DRESecurity,\*/mySecurity<br>                        In this example, IDOL Server reads the security type of documents from any DRESecurity field below the Document level and any mySecurity fields.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |                                                  |SecurityType configuration parameter                                                                                                                                                                                                                         |[SecurityFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_SecurityFields.htm)                                              |,              |
|SecurityType                |`string`                     |        |       |                                                        |Notes_V4<br>                        This example uses the security options defined in the [Notes_V4] section of your IDOL Server configuration file.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |                                                  |Security Type Configuration                                                                                                                                                                                                                                  |[SecurityType](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_SecurityType.htm)                                                  |               |
|TitleFields                 |IReadOnlyCollection<`string`>|        |       |                                                        |\*/DRETITLE<br>                        In this example, IDOL Server reads the document title from its DRETITLE field.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |                                                  |TitleType configuration parameter                                                                                                                                                                                                                            |[TitleFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_TitleFields.htm)                                                    |,              |

<a name="IntrospectAddData"></a>
## IntrospectAddData

**Int32**

*Requires an IDOL Instance*

|Parameter                   |Type                                  |Required|Summary|Default Value                                           |Example                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |Recommended Range                                 |See Also                                                                                                                                                                                                                                                     |URL                                                                                                                                                                               |Value Delimiter|
|:--------------------------:|:------------------------------------:|:------:|:-----:|:------------------------------------------------------:|:-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:------------------------------------------------:|:-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:-------------:|
|FieldValues                 |IReadOnlyDictionary<`string`,`string`>|        |       |                                                        |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       |                                                  |                                                                                                                                                                                                                                                             |                                                                                                                                                                                  |               |
|IDXData                     |`string`                              |        |       |                                                        |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       |                                                  |                                                                                                                                                                                                                                                             |                                                                                                                                                                                  |               |
|ACLFields                   |IReadOnlyCollection<`string`>         |        |       |                                                        |\*/AUTONOMYMETADATA<br>                        In this example, IDOL Server reads ACLs from any fields that are called AUTONOMYMETADATA.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |                                                  |ACLType configuration parameter                                                                                                                                                                                                                              |[ACLFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_ACLFields.htm)                                  |,              |
|CantHaveFields              |IReadOnlyCollection<`string`>         |        |       |                                                        |\*/StandardHeader<br>                        In this example, any StandardHeader fields in a document are discarded before the document is indexed.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |                                                  |MustHaveFields                                                                                                                                                                                                                                               |[CantHaveFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_CantHaveFields.htm)                        |,              |
|CreateDatabase              |`bool`?                               |        |       |False                                                   |True                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |                                                  |DREDbName<br>                        <br>                        DatabaseFields<br>                        <br>                        DatabaseType configuration parameter                                                                                  |[CreateDatabase](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_CreateDatabase.htm)                        |               |
|DatabaseFields              |IReadOnlyCollection<`string`>         |        |       |                                                        |Document/DREDBName,\*/myDB<br>                        In this example, IDOL Server indexes the document into the database whose name is contained in any DREDbName field below the Document level and whose name is contained in any fields called myDB.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |                                                  |DREDbName configuration parameter                                                                                                                                                                                                                            |[DatabaseFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_DatabaseFields.htm)                        |,              |
|DateFields                  |IReadOnlyCollection<`string`>         |        |       |                                                        |Document/DREDate,\*/myDocDate<br>                        In this example, IDOL Server extracts dates from any fields that are called DREDate that are contained below the Document level and from any fields that are called myDocdate.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |                                                  |DateType configuration parameter                                                                                                                                                                                                                             |[DateFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_DateFields.htm)                                |,              |
|Delete                      |`bool`?                               |        |       |False                                                   |true                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |                                                  |                                                                                                                                                                                                                                                             |[Delete](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_Delete.htm)                                        |               |
|DocumentDelimiters          |IReadOnlyCollection<`string`>         |        |       |                                                        |\*/DOCUMENT,\*/SPEECH<br>                        In this example, the beginning and end of individual documents in a file is marked by opening and closing DOCUMENT and SPEECH tags.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |                                                  |DocumentDelimiterCSVs configuration parameter                                                                                                                                                                                                                |[DocumentDelimiters](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_DocumentDelimiters.htm)                |,              |
|DocumentFormat              |`string`                              |        |       |                                                        |XML                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |                                                  |                                                                                                                                                                                                                                                             |[DocumentFormat](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_DocumentFormat.htm)                        |               |
|DREDbName                   |`string`                              |        |       |                                                        |News                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |                                                  |DatabaseFields                                                                                                                                                                                                                                               |[DREDbName](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_DREDbName.htm)                                  |               |
|ExpiryDateFields            |IReadOnlyCollection<`string`>         |        |       |                                                        |Document/DREExpiryDate,\*/myExpiryDate<br>                        In this example, IDOL Server reads the expiry date from any DREExpiryDate field below the Document level and from any fields called myExpiryDate.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |                                                  |ExpireDateType configuration parameter                                                                                                                                                                                                                       |[ExpiryDateFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_ExpiryDateFields.htm)                    |,              |
|FlattenIndexFields          |IReadOnlyCollection<`string`>         |        |       |                                                        |<documents><br>   <article id="_21498602"><br>      <url>http://example.com/21490.html</url><br>      <hltext_display>The history of pharmacogenetics </hltext_display><br>      <source>Science Online</source><br>      <media_type>text</media_type><br>      <subject><br>         <text>The prologue to pharmacogenetics began to play out around 1850 and spanned some 60 years into the 1900s.</text><br>         <text>In 1953, the molecular basis of heredity, the double helix of DNA, was described.</text><br>      </subject><br>   <valid_time>Jul 13 2001 5:00AM</valid_time><br>   </article><br></documents><br>                        If you specify FlattenIndexFields=\*/subject, and index the XML above, any content that a subject field or a field within a subject field includes is indexed as the content of the subject field.<br>                        You must also have a field configuration that configures the \*/subject and \*/subject/\* field paths as Index type, or you must set IndexField=\*/subject,\*/subject/\* in the index action.<br>                        If you now query for a particular term in the subject field that is actually contained in a level below the subject field, for example the term pharmacogenetics, the above text is returned. If you had not flattened the subject field the query would fail, as the subject field itself does not contain this term.|                                                  |FlattenIndexType configuration parameter                                                                                                                                                                                                                     |[FlattenIndexFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_FlattenIndexFields.htm)                |,              |
|IDXFieldPrefix              |`string`                              |        |       |The value of the IDXFieldPrefix configuration parameter.|DOCUMENT                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |                                                  |IDXFieldPrefix configuration parameter                                                                                                                                                                                                                       |[IDXFieldPrefix](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_IDXFieldPrefix.htm)                        |               |
|IndexFields                 |IReadOnlyCollection<`string`>         |        |       |                                                        |\*/DRECONTENT,\*/DRETITLE<br>                        In this example, DRECONTENT and DRETITLE fields are stored as Index fields in IDOL Server                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |                                                  |Index configuration parameter                                                                                                                                                                                                                                |[IndexFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_IndexFields.htm)                              |,              |
|IndexUID                    |`string`                              |        |       |                                                        |12345abcd                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |                                                  |Document Tracking Configuration Parameters                                                                                                                                                                                                                   |[IndexUID](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/../../Actions/SharedParameters/_ACI_IndexUID.htm)    |               |
|KeepExisting                |`bool`?                               |        |       |False                                                   |True                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |                                                  |KillDuplicatesDB                                                                                                                                                                                                                                             |[KeepExisting](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_KeepExisting.htm)                            |               |
|KillDuplicates              |`string`                              |        |       |                                                        |REFERENCE                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |                                                  |KeepExisting<br>                        <br>                        KillDuplicatesDB<br>                        <br>                        KillDuplicates configuration parameter                                                                           |[KillDuplicates](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_KillDuplicates.htm)                        |               |
|KillDuplicatesOption        |`string`                              |        |       |                                                        |To set the required KillDuplicates option, append it directly to the #DREENDDATA tag:DREADDDATA?[optionalParameters]Data#DREENDDATAREFERENCE\\n\\n<br>                        In this example, KillDuplicates is set to REFERENCE.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     |                                                  |KeepExisting<br>                        <br>                        KillDuplicates<br>                        <br>                        KillDuplicatesDB                                                                                                   |[KillDuplicatesOption](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_KillDuplicatesOption.htm)            |               |
|KillDuplicatesDB            |`string`                              |        |       |                                                        |dedup                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |                                                  |DREADD index action KillDuplicates parameter<br>                        DREADDDATA index action KillDuplicatesOption parameter<br>                        KillDuplicatesDBField                                                                              |[KillDuplicatesDB](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_KillDuplicatesDB.htm)                    |               |
|KillDuplicatesDBField       |`string`                              |        |       |                                                        |DATABASE                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |                                                  |DREADD index action KillDuplicates parameter<br>                        DREADDDATA index action KillDuplicatesOption parameter<br>                        KillDuplicatesDB                                                                                   |[KillDuplicatesDBField](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_KillDuplicatesDBField.htm)          |               |
|KillDuplicatesMatchDBs      |`string`                              |        |       |                                                        |DB1+DB2                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |                                                  |DREADD index action KillDuplicates parameter<br>                        DREADDDATA index action KillDuplicatesOption parameter<br>                        KillDuplicatesDB<br>                        <br>                        KillDuplicatesMatchTargetDB|[KillDuplicatesMatchDBs](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_KillDuplicatesMatchDBs.htm)        |               |
|KillDuplicatesMatchTargetDB |`bool`?                               |        |       |False                                                   |false                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |                                                  |DREADD index action KillDuplicates parameter<br>                        DREADDDATA index action KillDuplicatesOption parameter<br>                        KillDuplicatesDB<br>                        <br>                        KillDuplicatesMatchDBs     |[KillDuplicatesMatchTargetDB](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_KillDuplicatesMatchTarge.htm) |               |
|KillDuplicatesPreserveFields|`string`                              |        |       |                                                        |\*/SAVEDATA                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |                                                  |DREADD index action KillDuplicates parameter<br>                        DREADDDATA index action KillDuplicatesOption parameter<br>                        KillDuplicatesPreserveFields configuration parameter                                               |[KillDuplicatesPreserveFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_KillDuplicatesPreserveFi.htm)|               |
|LanguageFields              |IReadOnlyCollection<`string`>         |        |       |                                                        |Document/DRELanguageType,\*/myLanguageType<br>                        In this example, IDOL Server reads the document's language type from any DRELanguageType field below the Document level and any myLanguageType fields.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           |                                                  |LanguageType configuration parameter                                                                                                                                                                                                                         |[LanguageFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_LanguageFields.htm)                        |,              |
|LanguageType                |`string`                              |        |       |                                                        |myEnglish<br>                        In this example, the file is indexed with the language type myEnglish. The way IDOL Server handles this language type is determined by the way it has been defined in the IDOL Server configuration file (that is by the settings that you have associated with this language type in the configuration file).                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |                                                  |Language Types Configuration Parameters                                                                                                                                                                                                                      |[LanguageType](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_LanguageType.htm)                            |               |
|MustHaveFields              |IReadOnlyCollection<`string`>         |        |       |                                                        |\*/DRECONTENT,\*/DRETITLE<br>                        In this example, IDOL Server only stores a document's DRECONTENT and DRETITLE fields.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             |                                                  |CantHaveFields<br>                        MustHaveFieldCSVs configuration parameter                                                                                                                                                                          |[MustHaveFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_MustHaveFields.htm)                        |,              |
|Priority                    |`long`?                               |        |       |0                                                       |5                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      |Minimum: 0<br>                        Maximum: 100|                                                                                                                                                                                                                                                             |[Priority](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/../../Actions/SharedParameters/_ACI_Priority.htm)    |               |
|SectionFields               |IReadOnlyCollection<`string`>         |        |       |                                                        |Document/DRESection,\*/mySection<br>                        In this example, any DRESection field below the Document level and any mySection fields indicate the start of a new section.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |                                                  |SectionBreakType configuration parameter                                                                                                                                                                                                                     |[SectionFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_SectionFields.htm)                          |,              |
|SecurityFields              |IReadOnlyCollection<`string`>         |        |       |                                                        |Document/DRESecurity,\*/mySecurity<br>                        In this example, IDOL Server reads the security type of documents from any DRESecurity field below the Document level and any mySecurity fields.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |                                                  |SecurityType configuration parameter                                                                                                                                                                                                                         |[SecurityFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_SecurityFields.htm)                        |,              |
|SecurityType                |`string`                              |        |       |                                                        |Notes_V4<br>                        This example uses the security options defined in the [Notes_V4] section of your IDOL Server configuration file.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |                                                  |Security Type Configuration                                                                                                                                                                                                                                  |[SecurityType](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_SecurityType.htm)                            |               |
|TitleFields                 |IReadOnlyCollection<`string`>         |        |       |                                                        |\*/DRETITLE<br>                        In this example, IDOL Server reads the document title from its DRETITLE field.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |                                                  |TitleType configuration parameter                                                                                                                                                                                                                            |[TitleFields](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/IndexData/_IX_TitleFields.htm)                              |,              |

<a name="IntrospectAddFieldValues"></a>
## IntrospectAddFieldValues

**Int32**

*Requires an IDOL Instance*

|Parameter  |Type                                                |Required|Summary|
|:---------:|:--------------------------------------------------:|:------:|:-----:|
|DREDOCREF  |`string`                                            |☑️      |       |
|FieldValues|IReadOnlyCollection<KeyValuePair<`string`,`string`>>|☑️      |       |

<a name="IntrospectConvertQueryResponseData"></a>
## IntrospectConvertQueryResponseData

**String**

|Parameter        |Type                                   |Required|Summary|
|:---------------:|:-------------------------------------:|:------:|:-----:|
|QueryResponseData|[QueryResponseData](#QueryResponseData)|☑️      |       |
|ResultType       |[RecordType](#RecordType)?             |☑️      |       |

<a name="IntrospectDeleteDoc"></a>
## IntrospectDeleteDoc

**Int32**

*Requires an IDOL Instance*

|Parameter            |Type                      |Required|Summary|Default Value|Example                                                                                                                                                                                     |Recommended Range                                 |See Also                                  |URL                                                                                                                                                                                                         |
|:-------------------:|:------------------------:|:------:|:-----:|:-----------:|:------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:------------------------------------------------:|:----------------------------------------:|:----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|
|DocumentIds          |IReadOnlyCollection<`int`>|        |       |             |                                                                                                                                                                                            |                                                  |                                          |                                                                                                                                                                                                            |
|FirstDoc             |`int`?                    |        |       |             |                                                                                                                                                                                            |                                                  |                                          |                                                                                                                                                                                                            |
|LastDoc              |`int`?                    |        |       |             |                                                                                                                                                                                            |                                                  |                                          |                                                                                                                                                                                                            |
|IgnoreMaxPendingItems|`bool`?                   |        |       |False        |True                                                                                                                                                                                        |                                                  |Index Queue Configuration Parameters      |[IgnoreMaxPendingItems](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/RemoveContent/../../Actions/SharedParameters/_ACI_IgnoreMaxPendingItems.htm)|
|IndexUID             |`string`                  |        |       |             |12345abcd                                                                                                                                                                                   |                                                  |Document Tracking Configuration Parameters|[IndexUID](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/RemoveContent/../../Actions/SharedParameters/_ACI_IndexUID.htm)                          |
|Priority             |`long`?                   |        |       |0            |5                                                                                                                                                                                           |Minimum: 0<br>                        Maximum: 100|                                          |[Priority](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/RemoveContent/../../Actions/SharedParameters/_ACI_Priority.htm)                          |
|StateID              |`string`                  |        |       |             |B8UGIK95FKJG-23<br>                        Delete the first twelve documents, plus the14th and 16th documents, listed in the state token B8UGIK95FKJG-23:StateID=B8UGIK95FKJG-23[0-11+13+15]|                                                  |Query/StoreState                          |[StateID](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/RemoveContent/_IX_StateID.htm)                                                            |

<a name="IntrospectDeleteFields"></a>
## IntrospectDeleteFields

**Int32**

*Requires an IDOL Instance*

|Parameter     |Type                         |Required|Summary|
|:------------:|:---------------------------:|:------:|:-----:|
|DREDOCREF     |`string`                     |☑️      |       |
|FieldsToDelete|IReadOnlyCollection<`string`>|☑️      |       |

<a name="IntrospectDeleteFieldsOnlyIfTheyContainValue"></a>
## IntrospectDeleteFieldsOnlyIfTheyContainValue

**Int32**

*Requires an IDOL Instance*

|Parameter  |Type                                                |Required|Summary|
|:---------:|:--------------------------------------------------:|:------:|:-----:|
|DREDOCREF  |`string`                                            |☑️      |       |
|FieldValues|IReadOnlyCollection<KeyValuePair<`string`,`string`>>|☑️      |       |

<a name="IntrospectDeleteRef"></a>
## IntrospectDeleteRef

**Int32**

*Requires an IDOL Instance*

|Parameter            |Type                         |Required|Summary|Default Value|Example            |Recommended Range                                 |See Also                                  |URL                                                                                                                                                                                                         |Value Delimiter|
|:-------------------:|:---------------------------:|:------:|:-----:|:-----------:|:-----------------:|:------------------------------------------------:|:----------------------------------------:|:----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:-------------:|
|DocumentReferences   |IReadOnlyCollection<`string`>|        |       |             |                   |                                                  |                                          |                                                                                                                                                                                                            |               |
|DREDbName            |`string`                     |        |       |             |News               |                                                  |                                          |[DREDbName](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/RemoveContent/_IX_DREDbName_DREDELETEREF.htm)                                           |               |
|Field                |IReadOnlyCollection<`string`>|        |       |             |REFFIELD1,REFFIELD2|                                                  |                                          |[Field](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/RemoveContent/_IX_Field.htm)                                                                |,              |
|IgnoreMaxPendingItems|`bool`?                      |        |       |False        |True               |                                                  |Index Queue Configuration Parameters      |[IgnoreMaxPendingItems](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/RemoveContent/../../Actions/SharedParameters/_ACI_IgnoreMaxPendingItems.htm)|               |
|IndexUID             |`string`                     |        |       |             |12345abcd          |                                                  |Document Tracking Configuration Parameters|[IndexUID](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/RemoveContent/../../Actions/SharedParameters/_ACI_IndexUID.htm)                          |               |
|Priority             |`long`?                      |        |       |0            |5                  |Minimum: 0<br>                        Maximum: 100|                                          |[Priority](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/RemoveContent/../../Actions/SharedParameters/_ACI_Priority.htm)                          |               |

<a name="IntrospectExportIDX"></a>
## IntrospectExportIDX

**Int32**

*Requires an IDOL Instance*

|Parameter            |Type    |Required|Summary|Default Value|Example                                                                                                                                                                                                                      |Recommended Range                                 |See Also                                  |URL                                                                                                                                                                                                         |
|:-------------------:|:------:|:------:|:-----:|:-----------:|:---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:------------------------------------------------:|:----------------------------------------:|:----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|
|BatchSize            |`int`?  |        |       |100000       |1000                                                                                                                                                                                                                         |                                                  |                                          |[BatchSize](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/_IX_BatchSize.htm)                                                        |
|Compress             |`bool`? |        |       |True         |false                                                                                                                                                                                                                        |                                                  |                                          |[Compress](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/_IX_Compress.htm)                                                          |
|DatabaseMatch        |`string`|        |       |             |News,Marketing                                                                                                                                                                                                               |                                                  |                                          |[DatabaseMatch](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/_IX_DatabaseMatch.htm)                                                |
|Delete               |`bool`? |        |       |False        |true                                                                                                                                                                                                                         |                                                  |                                          |[Delete](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/_IX_Delete.htm)                                                              |
|Field                |`string`|        |       |             |DREREFERENCE                                                                                                                                                                                                                 |                                                  |MatchReference                            |[Field](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/_IX_Field.htm)                                                                |
|FileName             |`string`|        |       |             |/export/data/backup/output<br>                        In this example, the files that are created in the /export/data/backup directory are called output-0.idx.gz, output-1.idx.gz and so on.                                |                                                  |                                          |[FileName](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/_IX_FileName.htm)                                                          |
|HostDetails          |`bool`? |        |       |False        |true                                                                                                                                                                                                                         |                                                  |                                          |[HostDetails](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/_IX_HostDetails.htm)                                                    |
|IgnoreMaxPendingItems|`bool`? |        |       |False        |True                                                                                                                                                                                                                         |                                                  |Index Queue Configuration Parameters      |[IgnoreMaxPendingItems](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/../../Actions/SharedParameters/_ACI_IgnoreMaxPendingItems.htm)|
|IndexUID             |`string`|        |       |             |12345abcd                                                                                                                                                                                                                    |                                                  |Document Tracking Configuration Parameters|[IndexUID](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/../../Actions/SharedParameters/_ACI_IndexUID.htm)                          |
|MatchID              |`string`|        |       |             |5701 5702 5703 5704                                                                                                                                                                                                          |                                                  |                                          |[MatchID](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/_IX_MatchID.htm)                                                            |
|MatchReference       |`string`|        |       |             |http%253A%252F%252Fwww%252Ecompany%252Ecom%252Fwiki%252Ftopic_1%20http%253A%252F%252Fwww%252Ecompany%252Ecom%252Fwiki%252Ftopic_2%20http%253A%252F%252Fwww%252Ecompany%252Ecom%252Fwiki%252Ftopic_3                          |                                                  |                                          |[MatchReference](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/_IX_MatchReference.htm)                                              |
|MaxDate              |`string`|        |       |             |01/01/2004<br>                        In this example, only documents with a date before the 1st of January 2004 are exported.                                                                                               |                                                  |                                          |[MaxDate](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/_IX_MaxDate.htm)                                                            |
|MaxID                |`long`? |        |       |             |1000000                                                                                                                                                                                                                      |                                                  |                                          |[MaxID](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/_IX_MaxID.htm)                                                                |
|MinDate              |`string`|        |       |             |01/01/2003<br>                        In this example, only documents with a date after the 1st of January 2003 are exported.                                                                                                |                                                  |                                          |[MinDate](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/_IX_MinDate.htm)                                                            |
|MinID                |`string`|        |       |             |500001                                                                                                                                                                                                                       |                                                  |                                          |[MinID](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/_IX_MinID.htm)                                                                |
|Priority             |`long`? |        |       |0            |5                                                                                                                                                                                                                            |Minimum: 0<br>                        Maximum: 100|                                          |[Priority](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/../../Actions/SharedParameters/_ACI_Priority.htm)                          |
|StateMatchID         |`string`|        |       |             |B8UGIK95FKJG-23<br>                        Restrict the exported documents to the first twelve documents, plus the14th and 16th documents, listed in the state token B8UGIK95FKJG-23:StateMatchID=B8UGIK95FKJG-23[0-11+13+15]|                                                  |Query action StoreState parameter         |[StateMatchID](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ExportContent/_IX_StateMatchID.htm)                                                  |

<a name="IntrospectGetContent"></a>
## IntrospectGetContent

**QueryResponse**

*Requires an IDOL Instance*

|Parameter           |Type                         |Required|Summary|Value Delimiter|
|:------------------:|:---------------------------:|:------:|:-----:|:-------------:|
|StateId             |`string`                     |☑️      |       |               |
|DatabaseMatch       |List<`string`>               |        |       |               |
|FileName            |`string`                     |        |       |               |
|Output              |[Output](#Output)?           |        |       |               |
|Print               |[Print](#Print)?             |        |       |               |
|PrintFields         |IReadOnlyCollection<`string`>|        |       |,              |
|Boolean             |`bool`?                      |        |       |               |
|Characters          |`int`?                       |        |       |               |
|EncryptResponse     |`bool`?                      |        |       |               |
|EndTag              |`string`                     |        |       |               |
|FieldRecurse        |`bool`?                      |        |       |               |
|ForceTemplateRefresh|`bool`?                      |        |       |               |
|Highlight           |[Highlight](#Highlight)?     |        |       |               |
|HighlightTagTerm    |`bool`?                      |        |       |               |
|ID                  |`string`                     |        |       |               |
|LanguageType        |`string`                     |        |       |               |
|Links               |`string`                     |        |       |               |
|MaxPrintChars       |`int`?                       |        |       |               |
|OutputEncoding      |`string`                     |        |       |               |
|SecurityInfo        |`string`                     |        |       |               |
|Sentences           |`int`?                       |        |       |               |
|StartTag            |`string`                     |        |       |               |
|Summary             |[Summary](#Summary)?         |        |       |               |
|Template            |`string`                     |        |       |               |
|XMLMeta             |`bool`?                      |        |       |               |
|XMLResponse         |`bool`?                      |        |       |               |

<a name="IntrospectGetIndexStatus"></a>
## IntrospectGetIndexStatus

**IndexerGetStatusResult**

*Requires an IDOL Instance*

|Parameter           |Type                                |Required|Summary|Default Value|Example                                                                                                                                                                           |Recommended Range                                 |See Also                                                                                |URL                                                                                                                                                                             |
|:------------------:|:----------------------------------:|:------:|:-----:|:-----------:|:--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:------------------------------------------------:|:--------------------------------------------------------------------------------------:|:------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|
|EpochTime           |`bool`?                             |        |       |False        |True                                                                                                                                                                              |                                                  |                                                                                        |[EpochTime](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/_ACI_EpochTime.htm)                                          |
|Index               |`string`                            |        |       |             |1+3+5-10+13                                                                                                                                                                       |                                                  |IndexAction                                                                             |[Index](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/_ACI_Index.htm)                                                  |
|IndexAction         |[IndexActionEnum](#IndexActionEnum)?|        |       |             |IndexerGetStatus&Index=125&IndexAction=Setpriority&Priority=50<br>                        <br>                            This action changes the priority of index job 125 to 50.|                                                  |Index<br>                        <br>                        Priority                   |[IndexAction](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/_ACI_IndexAction.htm)                                      |
|IndexCmd            |`string`                            |        |       |             |IndexerGetStatus&IndexCmd=DREADD,DRESYNC<br>                        This action returns only information from DREADD or DRESYNC index actions.                                    |                                                  |                                                                                        |[IndexCmd](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/_ACI_IndexCmd.htm)                                            |
|IndexStatus         |`string`                            |        |       |             |-1+-3<br>                        In this example, only jobs with the status IDs -1 (finished) and -3 (file not found) are returned.                                               |                                                  |                                                                                        |[IndexStatus](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/_ACI_IndexStatus.htm)                                      |
|MaxResults          |`long`?                             |        |       |             |12                                                                                                                                                                                |                                                  |                                                                                        |[MaxResults](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/_ACI_MaxResults.htm)                                        |
|Priority            |`long`?                             |        |       |0            |5                                                                                                                                                                                 |Minimum: 0<br>                        Maximum: 100|                                                                                        |[Priority](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/_ACI_Priority.htm)                                            |
|ActionID            |`string`                            |        |       |             |GQTV17346                                                                                                                                                                         |                                                  |                                                                                        |[ActionID](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/../SharedParameters/_ACI_ActionID.htm)                        |
|EncryptResponse     |`bool`?                             |        |       |False        |True                                                                                                                                                                              |                                                  |CommsEncryptionType configuration parameter                                             |[EncryptResponse](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/../SharedParameters/_ACI_EncryptResponse.htm)          |
|FileName            |`string`                            |        |       |             |C:\\IDOLOutput\\Output.txt                                                                                                                                                        |                                                  |Output<br>                        <br>                        AllowedOutputDirectoryCSVs|[FileName](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/../SharedParameters/_ACI_FileName.htm)                        |
|ForceTemplateRefresh|`bool`?                             |        |       |False        |True                                                                                                                                                                              |                                                  |Template                                                                                |[ForceTemplateRefresh](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/../SharedParameters/_ACI_ForceTemplateRefresh.htm)|
|Output              |`string`                            |        |       |             |File                                                                                                                                                                              |                                                  |FileName                                                                                |[Output](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/../SharedParameters/_ACI_Output.htm)                            |
|ResponseFormat      |`string`                            |        |       |XML          |XML                                                                                                                                                                               |                                                  |                                                                                        |[ResponseFormat](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/../SharedParameters/_ACI_ResponseFormat.htm)            |
|Template            |`string`                            |        |       |             |MyTemplate                                                                                                                                                                        |                                                  |ForceTemplateRefresh                                                                    |[Template](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/../SharedParameters/_ACI_Template.htm)                        |
|TemplateParamCSVs   |`string`                            |        |       |             |Param1,Value1,Param2,Value2                                                                                                                                                       |                                                  |Template                                                                                |[TemplateParamCSVs](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/../SharedParameters/_ACI_TemplateParamCSVs.htm)      |

<a name="IntrospectGetStatus"></a>
## IntrospectGetStatus

**GetStatusResult**

*Requires an IDOL Instance*

|Parameter           |Type    |Required|Summary|Default Value|Example                    |See Also                                                                                |URL                                                                                                                                                                             |
|:------------------:|:------:|:------:|:-----:|:-----------:|:-------------------------:|:--------------------------------------------------------------------------------------:|:------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|
|ActionID            |`string`|        |       |             |GQTV17346                  |                                                                                        |[ActionID](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/../SharedParameters/_ACI_ActionID.htm)                        |
|EncryptResponse     |`bool`? |        |       |False        |True                       |CommsEncryptionType configuration parameter                                             |[EncryptResponse](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/../SharedParameters/_ACI_EncryptResponse.htm)          |
|FileName            |`string`|        |       |             |C:\\IDOLOutput\\Output.txt |Output<br>                        <br>                        AllowedOutputDirectoryCSVs|[FileName](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/../SharedParameters/_ACI_FileName.htm)                        |
|ForceTemplateRefresh|`bool`? |        |       |False        |True                       |Template                                                                                |[ForceTemplateRefresh](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/../SharedParameters/_ACI_ForceTemplateRefresh.htm)|
|Format              |`string`|        |       |XML          |HTML                       |                                                                                        |[Format](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/_ACI_Format.htm)                                                |
|Output              |`string`|        |       |             |File                       |FileName                                                                                |[Output](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/../SharedParameters/_ACI_Output.htm)                            |
|PortOnly            |`bool`? |        |       |False        |True                       |                                                                                        |[PortOnly](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/_ACI_PortOnly.htm)                                            |
|ResponseFormat      |`string`|        |       |XML          |XML                        |                                                                                        |[ResponseFormat](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/../SharedParameters/_ACI_ResponseFormat.htm)            |
|Template            |`string`|        |       |             |MyTemplate                 |ForceTemplateRefresh                                                                    |[Template](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/../SharedParameters/_ACI_Template.htm)                        |
|TemplateParamCSVs   |`string`|        |       |             |Param1,Value1,Param2,Value2|Template                                                                                |[TemplateParamCSVs](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Actions/Status/../SharedParameters/_ACI_TemplateParamCSVs.htm)      |

<a name="IntrospectReplace"></a>
## IntrospectReplace

**Int32**

*Requires an IDOL Instance*

|Parameter            |Type                                                |Required|Summary|Default Value|Example            |Recommended Range                                 |See Also                                  |URL                                                                                                                                                                                                        |Value Delimiter|
|:-------------------:|:--------------------------------------------------:|:------:|:-----:|:-----------:|:-----------------:|:------------------------------------------------:|:----------------------------------------:|:---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|:-------------:|
|DatabaseMatch        |`string`                                            |        |       |             |News+Archive       |                                                  |                                          |[DatabaseMatch](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ManageFields/_IX_DatabaseMatch_DREREPLACE.htm)                                     |               |
|Field                |IReadOnlyCollection<`string`>                       |        |       |             |REFFIELD1,REFFIELD2|                                                  |                                          |[Field](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ManageFields/_IX_Field_DREREPLACE.htm)                                                     |,              |
|IgnoreMaxPendingItems|`bool`?                                             |        |       |False        |True               |                                                  |Index Queue Configuration Parameters      |[IgnoreMaxPendingItems](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ManageFields/../../Actions/SharedParameters/_ACI_IgnoreMaxPendingItems.htm)|               |
|IndexUID             |`string`                                            |        |       |             |12345abcd          |                                                  |Document Tracking Configuration Parameters|[IndexUID](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ManageFields/../../Actions/SharedParameters/_ACI_IndexUID.htm)                          |               |
|InsertValue          |`bool`?                                             |        |       |False        |true               |                                                  |                                          |[InsertValue](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ManageFields/_IX_InsertValue.htm)                                                    |               |
|MultipleValues       |`bool`?                                             |        |       |True         |false              |                                                  |                                          |[MultipleValues](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ManageFields/_IX_MultipleValues.htm)                                              |               |
|Priority             |`long`?                                             |        |       |0            |5                  |Minimum: 0<br>                        Maximum: 100|                                          |[Priority](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ManageFields/../../Actions/SharedParameters/_ACI_Priority.htm)                          |               |
|ReplaceAllRefs       |`bool`?                                             |        |       |False        |true               |                                                  |                                          |[ReplaceAllRefs](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/ManageFields/_IX_ReplaceAllRefs.htm)                                              |               |
|FieldValues          |IReadOnlyCollection<KeyValuePair<`string`,`string`>>|        |       |             |                   |                                                  |                                          |                                                                                                                                                                                                           |               |

<a name="IntrospectSearch"></a>
## IntrospectSearch

**QueryResponse**

*Requires an IDOL Instance*

|Parameter         |Type                             |Required|Summary|Default Value       |
|:----------------:|:-------------------------------:|:------:|:-----:|:------------------:|
|BatchingMethod    |[BatchingMethod](#BatchingMethod)|        |       |BatchUsingStateToken|
|QueryId           |[Guid](#Guid)?                   |        |       |                    |
|Text              |`string`                         |        |       |                    |
|FieldText         |`string`                         |        |       |                    |
|StateDontMatchID  |`string`                         |        |       |                    |
|StateMatchID      |`string`                         |        |       |                    |
|AnyLanguage       |`bool`?                          |        |       |True                |
|CaseSensitive     |`bool`?                          |        |       |                    |
|DatabaseMatch     |List<`string`>                   |        |       |                    |
|Delete            |`bool`?                          |        |       |                    |
|DontMatchID       |List<`string`>                   |        |       |                    |
|DontMatchReference|List<`string`>                   |        |       |                    |
|FileName          |`string`                         |        |       |                    |
|MatchID           |List<`string`>                   |        |       |                    |
|MatchReference    |List<`string`>                   |        |       |                    |
|ReferenceField    |`string`                         |        |       |                    |
|MaxDate           |`string`                         |        |       |                    |
|Start             |`int`?                           |        |       |                    |
|MaxResults        |`int`?                           |        |       |                    |
|MinDate           |`string`                         |        |       |                    |
|Output            |[Output](#Output)?               |        |       |                    |
|SingleMatch       |`bool`?                          |        |       |                    |
|Sort              |`string`                         |        |       |                    |
|Stemming          |`bool`?                          |        |       |                    |
|StoredStateDetail |`bool`?                          |        |       |                    |
|StoredStateField  |`string`                         |        |       |                    |
|TimeoutMS         |`int`?                           |        |       |                    |
|Predict           |`bool`                           |        |       |False               |
|TotalResults      |`bool`                           |        |       |False               |

<a name="IntrospectSync"></a>
## IntrospectSync

**Int32**

*Requires an IDOL Instance*

|Parameter            |Type    |Required|Summary|Default Value|Example  |Recommended Range                                 |See Also                                  |URL                                                                                                                                                                                                           |
|:-------------------:|:------:|:------:|:-----:|:-----------:|:-------:|:------------------------------------------------:|:----------------------------------------:|:------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------:|
|IgnoreMaxPendingItems|`bool`? |        |       |False        |True     |                                                  |Index Queue Configuration Parameters      |[IgnoreMaxPendingItems](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/AdministerIndex/../../Actions/SharedParameters/_ACI_IgnoreMaxPendingItems.htm)|
|IndexUID             |`string`|        |       |             |12345abcd|                                                  |Document Tracking Configuration Parameters|[IndexUID](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/AdministerIndex/../../Actions/SharedParameters/_ACI_IndexUID.htm)                          |
|Priority             |`long`? |        |       |0            |5        |Minimum: 0<br>                        Maximum: 100|                                          |[Priority](https://www.microfocus.com/documentation/idol/IDOL/Servers/IDOLServer/11.0/Help/Content/Index%20Actions/AdministerIndex/../../Actions/SharedParameters/_ACI_Priority.htm)                          |

<a name="IntrospectUpdateFieldValues"></a>
## IntrospectUpdateFieldValues

**Int32**

*Requires an IDOL Instance*

|Parameter  |Type                                                |Required|Summary|
|:---------:|:--------------------------------------------------:|:------:|:-----:|
|DREDOCREF  |`string`                                            |☑️      |       |
|FieldValues|IReadOnlyCollection<KeyValuePair<`string`,`string`>>|☑️      |       |

<a name="Enumeration"></a>
# Enumerations
<a name="Concordance"></a>
## Concordance

|Parameter          |Type                     |Required|Summary|Default Value|
|:-----------------:|:-----------------------:|:------:|:-----:|:-----------:|
|ConcordanceFilePath|`string`                 |        |       |             |
|ConcordanceProcess |[Process](#Process)      |        |       |             |
|ConcordanceText    |`string`                 |        |       |             |
|ConvertTo          |[RecordType](#RecordType)|        |       |IDXDocument  |
|Injection          |[Injection](#Injection)  |☑️      |       |             |

<a name="CSV"></a>
## CSV

|Parameter                |Type                                     |Required|Summary|Default Value|
|:-----------------------:|:---------------------------------------:|:------:|:-----:|:-----------:|
|ColumnInjections         |List<[ColumnInjection](#ColumnInjection)>|☑️      |       |             |
|CommentToken             |`string`                                 |        |       |             |
|CSVFilePath              |`string`                                 |        |       |             |
|CSVProcess               |[Process](#Process)                      |        |       |             |
|CSVText                  |`string`                                 |        |       |             |
|Delimiter                |`string`                                 |        |       |,            |
|Distinct                 |`bool`                                   |        |       |False        |
|HasFieldsEnclosedInQuotes|`bool`                                   |        |       |False        |

<a name="Directory"></a>
## Directory

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Path     |`string`                     |☑️      |       |
|Injection|List<[Injection](#Injection)>|☑️      |       |

<a name="List"></a>
## List

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Members  |List<`string`>               |☑️      |       |
|Inject   |List<[Injection](#Injection)>|☑️      |       |

<a name="Process"></a>
# General Processes
<a name="AssertTrue"></a>
## AssertTrue

**Unit**

|Parameter|Type               |Required|Summary|
|:-------:|:-----------------:|:------:|:-----:|
|ResultOf |[Process](#Process)|        |       |

<a name="AssertFalse"></a>
## AssertFalse

**Unit**

|Parameter|Type               |Required|Summary|
|:-------:|:-----------------:|:------:|:-----:|
|ResultOf |[Process](#Process)|        |       |

<a name="AssertError"></a>
## AssertError

**Unit**

|Parameter|Type               |Required|Summary|
|:-------:|:-----------------:|:------:|:-----:|
|Process  |[Process](#Process)|☑️      |       |

<a name="CheckNumber"></a>
## CheckNumber

**Boolean**

|Parameter|Type               |Required|Summary|
|:-------:|:-----------------:|:------:|:-----:|
|Minimum  |`int`?             |        |       |
|Maximum  |`int`?             |        |       |
|Check    |[Process](#Process)|        |       |

<a name="Conditional"></a>
## Conditional

**Returns the same type as the 'Then' and 'Else' processes. Returns void if there is no Else process.**

|Parameter|Type               |Required|Summary|
|:-------:|:-----------------:|:------:|:-----:|
|If       |[Process](#Process)|☑️      |       |
|Then     |[Process](#Process)|☑️      |       |
|Else     |[Process](#Process)|        |       |

<a name="CreateDirectory"></a>
## CreateDirectory

**Unit**

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|Path     |`string`|☑️      |       |

<a name="DeleteItem"></a>
## DeleteItem

**Unit**

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|Path     |`string`|☑️      |       |

<a name="DoesFileContain"></a>
## DoesFileContain

**Boolean**

|Parameter       |Type    |Required|Summary|
|:--------------:|:------:|:------:|:-----:|
|ExpectedContents|`string`|☑️      |       |
|FilePath        |`string`|☑️      |       |

<a name="Loop"></a>
## Loop

**Unit**

|Parameter|Type                       |Required|Summary|
|:-------:|:-------------------------:|:------:|:-----:|
|For      |[Enumeration](#Enumeration)|☑️      |       |
|Do       |[Process](#Process)        |☑️      |       |

<a name="ReadFile"></a>
## ReadFile

**String**

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|FilePath |`string`|☑️      |       |

<a name="RunExternalProcess"></a>
## RunExternalProcess

**Unit**

|Parameter  |Type          |Required|Summary|Default Value                                   |
|:---------:|:------------:|:------:|:-----:|:----------------------------------------------:|
|ProcessPath|`string`      |☑️      |       |                                                |
|Arguments  |List<`string`>|        |       |System.Collections.Generic.List`1[System.String]|

<a name="Sequence"></a>
## Sequence

**Unit**

|Parameter|Type                     |Required|Summary|
|:-------:|:-----------------------:|:------:|:-----:|
|Steps    |List<[Process](#Process)>|☑️      |       |

<a name="Unzip"></a>
## Unzip

**Unit**

|Parameter           |Type    |Required|Summary|Default Value|
|:------------------:|:------:|:------:|:-----:|:-----------:|
|ArchiveFilePath     |`string`|☑️      |       |             |
|DestinationDirectory|`string`|☑️      |       |             |
|OverwriteFiles      |`bool`  |        |       |False        |

<a name="WriteFile"></a>
## WriteFile

**Unit**

|Parameter|Type               |Required|Summary|
|:-------:|:-----------------:|:------:|:-----:|
|Text     |[Process](#Process)|        |       |

<a name="Chain"></a>
## Chain

**The same as the type of the final process in the chain.**

|Parameter|Type                   |Required|Summary|
|:-------:|:---------------------:|:------:|:-----:|
|Process  |[Process](#Process)    |☑️      |       |
|Into     |[ChainLink](#ChainLink)|        |       |

<a name="ChainLink"></a>
## ChainLink

**The same as the type of the final process in the chain.**

|Parameter|Type                   |Required|Summary|
|:-------:|:---------------------:|:------:|:-----:|
|Process  |[Process](#Process)    |☑️      |       |
|Into     |[ChainLink](#ChainLink)|        |       |
|Inject   |[Injection](#Injection)|☑️      |       |

<a name="Injection"></a>
# injections
<a name="ColumnInjection"></a>
## ColumnInjection

|Parameter|Type    |Required|Summary|Default Value                         |
|:-------:|:------:|:------:|:-----:|:------------------------------------:|
|Column   |`string`|☑️      |       |                                      |
|Property |`string`|☑️      |       |                                      |
|Regex    |`string`|        |       |The entire value will be injected.    |
|Template |`string`|        |       |The value will be injected on its own.|

<a name="Injection"></a>
## Injection

|Parameter|Type    |Required|Summary|Default Value                         |
|:-------:|:------:|:------:|:-----:|:------------------------------------:|
|Property |`string`|☑️      |       |                                      |
|Regex    |`string`|        |       |The entire value will be injected.    |
|Template |`string`|        |       |The value will be injected on its own.|

# Enums
<a name="BatchingMethod"></a>
## BatchingMethod
|Name                |Summary|
|:------------------:|:-----:|
|BatchUsingIds       |       |
|BatchUsingStateToken|       |
|GetContentDirectly  |       |

<a name="DeduplicateBy"></a>
## DeduplicateBy
|Name      |Summary|
|:--------:|:-----:|
|Individual|       |
|Family    |       |

<a name="Highlight"></a>
## Highlight
|Name            |Summary|
|:--------------:|:-----:|
|Off             |       |
|Terms           |       |
|Sentences       |       |
|SummarySentences|       |
|SummaryTerms    |       |

<a name="IndexActionEnum"></a>
## IndexActionEnum
|Name       |Summary|
|:---------:|:-----:|
|Pause      |       |
|Restart    |       |
|Cancel     |       |
|SetPriority|       |

<a name="ItemSetDeduplication"></a>
## ItemSetDeduplication
|Name              |Summary|
|:----------------:|:-----:|
|Default           |       |
|MD5               |       |
|MD5PerCustodian   |       |
|MD5RankedCustodian|       |
|Scripted          |       |
|None              |       |

<a name="Output"></a>
## Output
|Name|Summary|
|:--:|:-----:|
|File|       |

<a name="Print"></a>
## Print
|Name          |Summary|
|:------------:|:-----:|
|All           |       |
|AllSections   |       |
|Date          |       |
|Fields        |       |
|Index         |       |
|IndexText     |       |
|NegativeFields|       |
|None          |       |
|NoResults     |       |
|Parametric    |       |
|Reference     |       |
|Source        |       |

<a name="PrintPreviewState"></a>
## PrintPreviewState
|Name|Summary|
|:--:|:-----:|
|All |       |
|Some|       |
|None|       |

<a name="ProductionSetSortOrder"></a>
## ProductionSetSortOrder
|Name                      |Summary|
|:------------------------:|:-----:|
|Position                  |       |
|TopLevelItemDate          |       |
|TopLevelItemDateDescending|       |
|DocumentId                |       |

<a name="RecordType"></a>
## RecordType
|Name       |Summary|
|:---------:|:-----:|
|IDXDocument|       |
|IDXData    |       |
|Concordance|       |

<a name="Summary"></a>
## Summary
|Name            |Summary|
|:--------------:|:-----:|
|Concept         |       |
|Off             |       |
|ParagraphConcept|       |
|Quick           |       |
|Context         |       |

