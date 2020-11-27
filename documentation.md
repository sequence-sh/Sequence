# Contents
|Step                                                             |Summary|
|:---------------------------------------------------------------:|:-----:|
|[AppendString](#AppendString)                                    |       |
|[ApplyBooleanOperator](#ApplyBooleanOperator)                    |       |
|[ApplyMathOperator](#ApplyMathOperator)                          |       |
|[Array<T>](#Array<T>)                                            |       |
|[ArrayIsEmpty<T>](#ArrayIsEmpty<T>)                              |       |
|[ArrayLength<T>](#ArrayLength<T>)                                |       |
|[ArraySort<T>](#ArraySort<T>)                                    |       |
|[AssertError](#AssertError)                                      |       |
|[AssertTrue](#AssertTrue)                                        |       |
|[CharAtIndex](#CharAtIndex)                                      |       |
|[Compare<T>](#Compare<T>)                                        |       |
|[CreateDirectory](#CreateDirectory)                              |       |
|[DeleteItem](#DeleteItem)                                        |       |
|[DirectoryExists](#DirectoryExists)                              |       |
|[DoNothing](#DoNothing)                                          |       |
|[DoXTimes](#DoXTimes)                                            |       |
|[ElementAtIndex<T>](#ElementAtIndex<T>)                          |       |
|[EnforceSchema](#EnforceSchema)                                  |       |
|[EntityForEach](#EntityForEach)                                  |       |
|[EntityGetValue](#EntityGetValue)                                |       |
|[EntityHasProperty](#EntityHasProperty)                          |       |
|[EntityMap](#EntityMap)                                          |       |
|[EntityMapProperties](#EntityMapProperties)                      |       |
|[EntitySetValue<T>](#EntitySetValue<T>)                          |       |
|[EntityStreamConcat](#EntityStreamConcat)                        |       |
|[EntityStreamDistinct](#EntityStreamDistinct)                    |       |
|[EntityStreamFilter](#EntityStreamFilter)                        |       |
|[EntityStreamSort](#EntityStreamSort)                            |       |
|[FileExists](#FileExists)                                        |       |
|[FileExtract](#FileExtract)                                      |       |
|[FileWrite](#FileWrite)                                          |       |
|[FindElement<T>](#FindElement<T>)                                |       |
|[FindLastSubstring](#FindLastSubstring)                          |       |
|[FindSubstring](#FindSubstring)                                  |       |
|[For](#For)                                                      |       |
|[ForEach<T>](#ForEach<T>)                                        |       |
|[FromConcordance](#FromConcordance)                              |       |
|[FromCSV](#FromCSV)                                              |       |
|[GenerateDocumentation](#GenerateDocumentation)                  |       |
|[GetSubstring](#GetSubstring)                                    |       |
|[GetVariable<T>](#GetVariable<T>)                                |       |
|[If](#If)                                                        |       |
|[IncrementVariable](#IncrementVariable)                          |       |
|[Not](#Not)                                                      |       |
|[PathCombine](#PathCombine)                                      |       |
|[Print<T>](#Print<T>)                                            |       |
|[ReadFile](#ReadFile)                                            |       |
|[Repeat<T>](#Repeat<T>)                                          |       |
|[RunExternalProcess](#RunExternalProcess)                        |       |
|[Sequence](#Sequence)                                            |       |
|[SetVariable<T>](#SetVariable<T>)                                |       |
|[StringContains](#StringContains)                                |       |
|[StringFromStream](#StringFromStream)                            |       |
|[StringIsEmpty](#StringIsEmpty)                                  |       |
|[StringJoin](#StringJoin)                                        |       |
|[StringLength](#StringLength)                                    |       |
|[StringSplit](#StringSplit)                                      |       |
|[StringToCase](#StringToCase)                                    |       |
|[StringToStream](#StringToStream)                                |       |
|[StringTrim](#StringTrim)                                        |       |
|[ToConcordance](#ToConcordance)                                  |       |
|[ToCSV](#ToCSV)                                                  |       |
|[ValueIf<T>](#ValueIf<T>)                                        |       |
|[While](#While)                                                  |       |
|[NuixAddConcordance](#NuixAddConcordance)                        |       |
|[NuixAddItem](#NuixAddItem)                                      |       |
|[NuixAddToItemSet](#NuixAddToItemSet)                            |       |
|[NuixAddToProductionSet](#NuixAddToProductionSet)                |       |
|[NuixAnnotateDocumentIdList](#NuixAnnotateDocumentIdList)        |       |
|[NuixAssertPrintPreviewState](#NuixAssertPrintPreviewState)      |       |
|[NuixAssignCustodian](#NuixAssignCustodian)                      |       |
|[NuixCloseConnection](#NuixCloseConnection)                      |       |
|[NuixCountItems](#NuixCountItems)                                |       |
|[NuixCreateCase](#NuixCreateCase)                                |       |
|[NuixCreateIrregularItemsReport](#NuixCreateIrregularItemsReport)|       |
|[NuixCreateNRTReport](#NuixCreateNRTReport)                      |       |
|[NuixCreateReport](#NuixCreateReport)                            |       |
|[NuixCreateTermList](#NuixCreateTermList)                        |       |
|[NuixDoesCaseExist](#NuixDoesCaseExist)                          |       |
|[NuixExportConcordance](#NuixExportConcordance)                  |       |
|[NuixExtractEntities](#NuixExtractEntities)                      |       |
|[NuixGeneratePrintPreviews](#NuixGeneratePrintPreviews)          |       |
|[NuixGetItemProperties](#NuixGetItemProperties)                  |       |
|[NuixImportDocumentIds](#NuixImportDocumentIds)                  |       |
|[NuixMigrateCase](#NuixMigrateCase)                              |       |
|[NuixPerformOCR](#NuixPerformOCR)                                |       |
|[NuixRemoveFromProductionSet](#NuixRemoveFromProductionSet)      |       |
|[NuixReorderProductionSet](#NuixReorderProductionSet)            |       |
|[NuixSearchAndTag](#NuixSearchAndTag)                            |       |
# Core
<a name="AppendString"></a>
## AppendString

**Unit**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Variable |[VariableName](#VariableName)|☑️      |       |
|String   |`string`                     |☑️      |       |

<a name="ApplyBooleanOperator"></a>
## ApplyBooleanOperator

**Boolean**

|Parameter|Type                               |Required|Summary|
|:-------:|:---------------------------------:|:------:|:-----:|
|Left     |`bool`                             |☑️      |       |
|Operator |[BooleanOperator](#BooleanOperator)|☑️      |       |
|Right    |`bool`                             |☑️      |       |

<a name="ApplyMathOperator"></a>
## ApplyMathOperator

**Int32**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Left     |`int`                        |☑️      |       |
|Operator |[MathOperator](#MathOperator)|☑️      |       |
|Right    |`int`                        |☑️      |       |

<a name="Array<T>"></a>
## Array<T>

**List<T>**

|Parameter|Type          |Required|Summary|
|:-------:|:------------:|:------:|:-----:|
|Elements |IStep<[T](#T)>|☑️      |       |

<a name="ArrayIsEmpty<T>"></a>
## ArrayIsEmpty<T>

**Boolean**

|Parameter|Type         |Required|Summary|
|:-------:|:-----------:|:------:|:-----:|
|Array    |List<[T](#T)>|☑️      |       |

<a name="ArrayLength<T>"></a>
## ArrayLength<T>

**Int32**

|Parameter|Type         |Required|Summary|
|:-------:|:-----------:|:------:|:-----:|
|Array    |List<[T](#T)>|☑️      |       |

<a name="ArraySort<T>"></a>
## ArraySort<T>

**List<T>**

|Parameter |Type         |Required|Summary|Default Value|
|:--------:|:-----------:|:------:|:-----:|:-----------:|
|Array     |List<[T](#T)>|☑️      |       |             |
|Descending|`bool`       |        |       |False        |

<a name="AssertError"></a>
## AssertError

**Unit**

|Parameter|Type         |Required|Summary|
|:-------:|:-----------:|:------:|:-----:|
|Step     |[Unit](#Unit)|☑️      |       |

<a name="AssertTrue"></a>
## AssertTrue

**Unit**

|Parameter|Type  |Required|Summary|
|:-------:|:----:|:------:|:-----:|
|Boolean  |`bool`|☑️      |       |

<a name="CharAtIndex"></a>
## CharAtIndex

**String**

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|Index    |`int`   |☑️      |       |
|String   |`string`|☑️      |       |

<a name="Compare<T>"></a>
## Compare<T>

**Boolean**

|Parameter|Type                               |Required|Summary|
|:-------:|:---------------------------------:|:------:|:-----:|
|Left     |[T](#T)                            |☑️      |       |
|Operator |[CompareOperator](#CompareOperator)|☑️      |       |
|Right    |[T](#T)                            |☑️      |       |

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

<a name="DirectoryExists"></a>
## DirectoryExists

**Boolean**

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|Path     |`string`|☑️      |       |

<a name="DoNothing"></a>
## DoNothing

**Unit**

<a name="DoXTimes"></a>
## DoXTimes

**Unit**

|Parameter|Type         |Required|Summary|
|:-------:|:-----------:|:------:|:-----:|
|Action   |[Unit](#Unit)|☑️      |       |
|X        |`int`        |☑️      |       |

<a name="ElementAtIndex<T>"></a>
## ElementAtIndex<T>

**T**

|Parameter|Type         |Required|Summary|
|:-------:|:-----------:|:------:|:-----:|
|Array    |List<[T](#T)>|☑️      |       |
|Index    |`int`        |☑️      |       |

<a name="EnforceSchema"></a>
## EnforceSchema

**EntityStream**

|Parameter     |Type                             |Required|Summary|Default Value|
|:------------:|:-------------------------------:|:------:|:-----:|:-----------:|
|EntityStream  |[EntityStream](#EntityStream)    |☑️      |       |             |
|ErrorBehaviour|[ErrorBehaviour](#ErrorBehaviour)|        |       |Fail         |
|Schema        |[Schema](#Schema)                |☑️      |       |             |

<a name="EntityForEach"></a>
## EntityForEach

**Unit**

|Parameter   |Type                         |Required|Summary|
|:----------:|:---------------------------:|:------:|:-----:|
|Action      |[Unit](#Unit)                |☑️      |       |
|EntityStream|[EntityStream](#EntityStream)|☑️      |       |

<a name="EntityGetValue"></a>
## EntityGetValue

**String**

|Parameter|Type             |Required|Summary|
|:-------:|:---------------:|:------:|:-----:|
|Entity   |[Entity](#Entity)|☑️      |       |
|Property |`string`         |☑️      |       |

<a name="EntityHasProperty"></a>
## EntityHasProperty

**Boolean**

|Parameter|Type             |Required|Summary|
|:-------:|:---------------:|:------:|:-----:|
|Entity   |[Entity](#Entity)|☑️      |       |
|Property |`string`         |☑️      |       |

<a name="EntityMap"></a>
## EntityMap

**EntityStream**

|Parameter   |Type                         |Required|Summary|
|:----------:|:---------------------------:|:------:|:-----:|
|EntityStream|[EntityStream](#EntityStream)|☑️      |       |
|Function    |[Entity](#Entity)            |☑️      |       |

<a name="EntityMapProperties"></a>
## EntityMapProperties

**EntityStream**

|Parameter   |Type                         |Required|Summary|
|:----------:|:---------------------------:|:------:|:-----:|
|EntityStream|[EntityStream](#EntityStream)|☑️      |       |
|Mappings    |[Entity](#Entity)            |☑️      |       |

<a name="EntitySetValue<T>"></a>
## EntitySetValue<T>

**Entity**

|Parameter|Type             |Required|Summary|
|:-------:|:---------------:|:------:|:-----:|
|Entity   |[Entity](#Entity)|☑️      |       |
|Property |`string`         |☑️      |       |
|Value    |[T](#T)          |☑️      |       |

<a name="EntityStreamConcat"></a>
## EntityStreamConcat

**EntityStream**

|Parameter    |Type                               |Required|Summary|
|:-----------:|:---------------------------------:|:------:|:-----:|
|EntityStreams|List<[EntityStream](#EntityStream)>|☑️      |       |

<a name="EntityStreamDistinct"></a>
## EntityStreamDistinct

**EntityStream**

|Parameter   |Type                         |Required|Summary|Default Value|
|:----------:|:---------------------------:|:------:|:-----:|:-----------:|
|EntityStream|[EntityStream](#EntityStream)|☑️      |       |             |
|KeySelector |`string`                     |☑️      |       |             |
|IgnoreCase  |`bool`                       |        |       |False        |

<a name="EntityStreamFilter"></a>
## EntityStreamFilter

**EntityStream**

|Parameter   |Type                         |Required|Summary|
|:----------:|:---------------------------:|:------:|:-----:|
|EntityStream|[EntityStream](#EntityStream)|☑️      |       |
|Predicate   |`bool`                       |☑️      |       |

<a name="EntityStreamSort"></a>
## EntityStreamSort

**EntityStream**

|Parameter   |Type                         |Required|Summary|Default Value|
|:----------:|:---------------------------:|:------:|:-----:|:-----------:|
|EntityStream|[EntityStream](#EntityStream)|☑️      |       |             |
|KeySelector |`string`                     |☑️      |       |             |
|Descending  |`bool`                       |        |       |False        |

<a name="FileExists"></a>
## FileExists

**Boolean**

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|Path     |`string`|☑️      |       |

<a name="FileExtract"></a>
## FileExtract

**Unit**

|Parameter      |Type    |Required|Summary|Default Value|
|:-------------:|:------:|:------:|:-----:|:-----------:|
|ArchiveFilePath|`string`|☑️      |       |             |
|Destination    |`string`|☑️      |       |             |
|Overwrite      |`bool`  |        |       |false        |

<a name="FileWrite"></a>
## FileWrite

**Unit**

|Parameter|Type             |Required|Summary|
|:-------:|:---------------:|:------:|:-----:|
|Path     |`string`         |☑️      |       |
|Stream   |[Stream](#Stream)|☑️      |       |

<a name="FindElement<T>"></a>
## FindElement<T>

**Int32**

|Parameter|Type         |Required|Summary|
|:-------:|:-----------:|:------:|:-----:|
|Array    |List<[T](#T)>|☑️      |       |
|Element  |[T](#T)      |☑️      |       |

<a name="FindLastSubstring"></a>
## FindLastSubstring

**Int32**

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|String   |`string`|☑️      |       |
|SubString|`string`|☑️      |       |

<a name="FindSubstring"></a>
## FindSubstring

**Int32**

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|String   |`string`|☑️      |       |
|SubString|`string`|☑️      |       |

<a name="For"></a>
## For

**Unit**

|Parameter|Type         |Required|Summary|
|:-------:|:-----------:|:------:|:-----:|
|Action   |[Unit](#Unit)|☑️      |       |
|From     |`int`        |☑️      |       |
|Increment|`int`        |☑️      |       |
|To       |`int`        |☑️      |       |

<a name="ForEach<T>"></a>
## ForEach<T>

**Unit**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Action   |[Unit](#Unit)                |☑️      |       |
|Array    |List<[T](#T)>                |☑️      |       |
|Variable |[VariableName](#VariableName)|☑️      |       |

<a name="FromConcordance"></a>
## FromConcordance

**EntityStream**

|Parameter          |Type                         |Required|Summary|Default Value|
|:-----------------:|:---------------------------:|:------:|:-----:|:-----------:|
|Stream             |[Stream](#Stream)            |☑️      |       |             |
|Encoding           |[EncodingEnum](#EncodingEnum)|        |       |UTF8 no BOM  |
|Delimiter          |`string`                     |        |       |\\u0014 - DC4|
|QuoteCharacter     |`string`                     |        |       |þ            |
|MultiValueDelimiter|`string`                     |        |       |\|           |

<a name="FromCSV"></a>
## FromCSV

**EntityStream**

|Parameter          |Type                         |Required|Summary|Default Value|
|:-----------------:|:---------------------------:|:------:|:-----:|:-----------:|
|Stream             |[Stream](#Stream)            |☑️      |       |             |
|Encoding           |[EncodingEnum](#EncodingEnum)|        |       |UTF8 no BOM  |
|Delimiter          |`string`                     |        |       |,            |
|CommentCharacter   |`string`                     |        |       |#            |
|QuoteCharacter     |`string`                     |        |       |"            |
|MultiValueDelimiter|`string`                     |        |       |             |

<a name="GenerateDocumentation"></a>
## GenerateDocumentation

**List`1**

<a name="GetSubstring"></a>
## GetSubstring

**String**

|Parameter|Type    |Required|Summary|Default Value|
|:-------:|:------:|:------:|:-----:|:-----------:|
|Index    |`int`   |        |       |0            |
|Length   |`int`   |☑️      |       |             |
|String   |`string`|☑️      |       |             |

<a name="GetVariable<T>"></a>
## GetVariable<T>

**T**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Variable |[VariableName](#VariableName)|☑️      |       |

<a name="If"></a>
## If

**Unit**

|Parameter|Type         |Required|Summary|Default Value|
|:-------:|:-----------:|:------:|:-----:|:-----------:|
|Condition|`bool`       |☑️      |       |             |
|Then     |[Unit](#Unit)|☑️      |       |             |
|Else     |[Unit](#Unit)|        |       |Do Nothing   |

<a name="IncrementVariable"></a>
## IncrementVariable

**Int32**

|Parameter|Type                         |Required|Summary|Default Value|
|:-------:|:---------------------------:|:------:|:-----:|:-----------:|
|Amount   |`int`                        |        |       |1            |
|Variable |[VariableName](#VariableName)|☑️      |       |             |

<a name="Not"></a>
## Not

**Boolean**

|Parameter|Type  |Required|Summary|
|:-------:|:----:|:------:|:-----:|
|Boolean  |`bool`|☑️      |       |

<a name="PathCombine"></a>
## PathCombine

**String**

|Parameter|Type          |Required|Summary|
|:-------:|:------------:|:------:|:-----:|
|Paths    |List<`string`>|☑️      |       |

<a name="Print<T>"></a>
## Print<T>

**Unit**

|Parameter|Type   |Required|Summary|
|:-------:|:-----:|:------:|:-----:|
|Value    |[T](#T)|☑️      |       |

<a name="ReadFile"></a>
## ReadFile

**String**

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|Path     |`string`|☑️      |       |

<a name="Repeat<T>"></a>
## Repeat<T>

**List<T>**

|Parameter|Type   |Required|Summary|
|:-------:|:-----:|:------:|:-----:|
|Element  |[T](#T)|☑️      |       |
|Number   |`int`  |☑️      |       |

<a name="RunExternalProcess"></a>
## RunExternalProcess

**Unit**

|Parameter|Type                         |Required|Summary|Default Value   |
|:-------:|:---------------------------:|:------:|:-----:|:--------------:|
|Path     |`string`                     |☑️      |       |                |
|Arguments|List<`string`>               |        |       |No arguments    |
|Encoding |[EncodingEnum](#EncodingEnum)|        |       |Default encoding|

<a name="Sequence"></a>
## Sequence

**Unit**

|Parameter|Type                |Required|Summary|
|:-------:|:------------------:|:------:|:-----:|
|Steps    |IStep<[Unit](#Unit)>|☑️      |       |

<a name="SetVariable<T>"></a>
## SetVariable<T>

**Unit**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Value    |[T](#T)                      |☑️      |       |
|Variable |[VariableName](#VariableName)|☑️      |       |

<a name="StringContains"></a>
## StringContains

**Boolean**

|Parameter |Type    |Required|Summary|Default Value|
|:--------:|:------:|:------:|:-----:|:-----------:|
|IgnoreCase|`bool`  |        |       |False        |
|String    |`string`|☑️      |       |             |
|Substring |`string`|☑️      |       |             |

<a name="StringFromStream"></a>
## StringFromStream

**String**

|Parameter|Type                         |Required|Summary|Default Value|
|:-------:|:---------------------------:|:------:|:-----:|:-----------:|
|Encoding |[EncodingEnum](#EncodingEnum)|        |       |UTF8 no BOM  |
|Stream   |[Stream](#Stream)            |☑️      |       |             |

<a name="StringIsEmpty"></a>
## StringIsEmpty

**Boolean**

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|String   |`string`|☑️      |       |

<a name="StringJoin"></a>
## StringJoin

**String**

|Parameter|Type          |Required|Summary|
|:-------:|:------------:|:------:|:-----:|
|Delimiter|`string`      |☑️      |       |
|Strings  |List<`string`>|☑️      |       |

<a name="StringLength"></a>
## StringLength

**Int32**

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|String   |`string`|☑️      |       |

<a name="StringSplit"></a>
## StringSplit

**List`1**

|Parameter|Type    |Required|Summary|
|:-------:|:------:|:------:|:-----:|
|Delimiter|`string`|☑️      |       |
|String   |`string`|☑️      |       |

<a name="StringToCase"></a>
## StringToCase

**String**

|Parameter|Type                 |Required|Summary|
|:-------:|:-------------------:|:------:|:-----:|
|Case     |[TextCase](#TextCase)|☑️      |       |
|String   |`string`             |☑️      |       |

<a name="StringToStream"></a>
## StringToStream

**Stream**

|Parameter|Type                         |Required|Summary|Default Value|
|:-------:|:---------------------------:|:------:|:-----:|:-----------:|
|String   |`string`                     |☑️      |       |             |
|Encoding |[EncodingEnum](#EncodingEnum)|        |       |UTF8 no BOM  |

<a name="StringTrim"></a>
## StringTrim

**String**

|Parameter|Type                 |Required|Summary|Default Value|
|:-------:|:-------------------:|:------:|:-----:|:-----------:|
|Side     |[TrimSide](#TrimSide)|        |       |Both         |
|String   |`string`             |☑️      |       |             |

<a name="ToConcordance"></a>
## ToConcordance

**Stream**

|Parameter          |Type                         |Required|Summary|Default Value                                                  |Example            |
|:-----------------:|:---------------------------:|:------:|:-----:|:-------------------------------------------------------------:|:-----------------:|
|Entities           |[EntityStream](#EntityStream)|☑️      |       |                                                               |                   |
|Encoding           |[EncodingEnum](#EncodingEnum)|        |       |UTF8 no BOM                                                    |                   |
|Delimiter          |`string`                     |        |       |                                                              |                   |
|AlwaysQuote        |`bool`                       |        |       |false                                                          |                   |
|QuoteCharacter     |`string`                     |        |       |þ                                                              |                   |
|MultiValueDelimiter|`string`                     |        |       |                                                               |                   |
|DateTimeFormat     |`string`                     |        |       |O - ISO 8601 compliant - e.g. 2009-06-15T13:45:30.0000000-07:00|yyyy/MM/dd HH:mm:ss|

<a name="ToCSV"></a>
## ToCSV

**Stream**

|Parameter          |Type                         |Required|Summary|Default Value                                                  |Example            |
|:-----------------:|:---------------------------:|:------:|:-----:|:-------------------------------------------------------------:|:-----------------:|
|Entities           |[EntityStream](#EntityStream)|☑️      |       |                                                               |                   |
|Encoding           |[EncodingEnum](#EncodingEnum)|        |       |UTF8 no BOM                                                    |                   |
|Delimiter          |`string`                     |        |       |,                                                              |                   |
|AlwaysQuote        |`bool`                       |        |       |false                                                          |                   |
|QuoteCharacter     |`string`                     |        |       |"                                                              |                   |
|MultiValueDelimiter|`string`                     |        |       |                                                               |                   |
|DateTimeFormat     |`string`                     |        |       |O - ISO 8601 compliant - e.g. 2009-06-15T13:45:30.0000000-07:00|yyyy/MM/dd HH:mm:ss|

<a name="ValueIf<T>"></a>
## ValueIf<T>

**T**

|Parameter|Type   |Required|Summary|
|:-------:|:-----:|:------:|:-----:|
|Condition|`bool` |☑️      |       |
|Else     |[T](#T)|☑️      |       |
|Then     |[T](#T)|☑️      |       |

<a name="While"></a>
## While

**Unit**

|Parameter|Type         |Required|Summary|
|:-------:|:-----------:|:------:|:-----:|
|Action   |[Unit](#Unit)|☑️      |       |
|Condition|`bool`       |☑️      |       |

# Nuix
<a name="NuixAddConcordance"></a>
## NuixAddConcordance

**Unit**

*Requires Nuix Version 7.6*

*Requires NuixCASE_CREATION*

*Requires NuixMETADATA_IMPORT*

|Parameter             |Type    |Required|Summary|Default Value |Example                   |
|:--------------------:|:------:|:------:|:-----:|:------------:|:------------------------:|
|CasePath              |`string`|☑️      |       |              |C:/Cases/MyCase           |
|ConcordanceDateFormat |`string`|☑️      |       |              |yyyy-MM-dd'T'HH:mm:ss.SSSZ|
|ConcordanceProfileName|`string`|☑️      |       |              |MyProfile                 |
|Custodian             |`string`|☑️      |       |              |                          |
|Description           |`string`|        |       |No description|                          |
|FilePath              |`string`|☑️      |       |              |C:/MyConcordance.dat      |
|FolderName            |`string`|☑️      |       |              |                          |

<a name="NuixAddItem"></a>
## NuixAddItem

**Unit**

*Requires Nuix Version 5.0*

*Requires NuixCASE_CREATION*

|Parameter                 |Type                         |Required|Summary|Default Value                                   |Example                            |Requirements|
|:------------------------:|:---------------------------:|:------:|:-----:|:----------------------------------------------:|:---------------------------------:|:----------:|
|CasePath                  |`string`                     |☑️      |       |                                                |C:/Cases/MyCase                    |            |
|Custodian                 |`string`                     |☑️      |       |                                                |                                   |            |
|Description               |`string`                     |        |       |No Description                                  |                                   |            |
|FolderName                |`string`                     |☑️      |       |                                                |                                   |            |
|MimeTypeSettings          |[EntityStream](#EntityStream)|        |       |Use default settings for all MIME types         |                                   |Nuix 8.2    |
|ParallelProcessingSettings|[Entity](#Entity)            |        |       |Parallel processing settings will not be changed|                                   |            |
|PasswordFilePath          |`string`                     |        |       |Do not attempt decryption                       |C:/Data/Passwords.txt              |Nuix 7.6    |
|Paths                     |List<`string`>               |☑️      |       |                                                |C:/Data/File.txt                   |            |
|ProcessingProfileName     |`string`                     |        |       |The default processing profile will be used.    |MyProcessingProfile                |Nuix 7.6    |
|ProcessingProfilePath     |`string`                     |        |       |The default processing profile will be used.    |C:/Profiles/MyProcessingProfile.xml|Nuix 7.6    |
|ProcessingSettings        |[Entity](#Entity)            |        |       |Processing settings will not be changed         |                                   |            |

<a name="NuixAddToItemSet"></a>
## NuixAddToItemSet

**Unit**

*Requires Nuix Version 5.0*

*Requires NuixANALYSIS*

|Parameter           |Type                                         |Required|Summary|Default Value         |Example                 |
|:------------------:|:-------------------------------------------:|:------:|:-----:|:--------------------:|:----------------------:|
|CasePath            |`string`                                     |☑️      |       |                      |C:/Cases/MyCase         |
|CustodianRanking    |List<`string`>                               |        |       |Do not rank custodians|                        |
|DeduplicateBy       |[DeduplicateBy](#DeduplicateBy)              |        |       |Neither               |                        |
|ItemSetDeduplication|[ItemSetDeduplication](#ItemSetDeduplication)|        |       |No deduplication      |                        |
|ItemSetDescription  |`string`                                     |        |       |No description        |                        |
|ItemSetName         |`string`                                     |☑️      |       |                      |                        |
|Limit               |`int`                                        |        |       |No limit              |                        |
|Order               |`string`                                     |        |       |Do not reorder        |name ASC, item-date DESC|
|SearchTerm          |`string`                                     |☑️      |       |                      |                        |

<a name="NuixAddToProductionSet"></a>
## NuixAddToProductionSet

**Unit**

*Requires Nuix Version 7.2*

*Requires NuixPRODUCTION_SET*

|Parameter            |Type    |Required|Summary|Default Value                               |Example                            |Requirements|
|:-------------------:|:------:|:------:|:-----:|:------------------------------------------:|:---------------------------------:|:----------:|
|CasePath             |`string`|☑️      |       |                                            |C:/Cases/MyCase                    |            |
|Description          |`string`|        |       |No description                              |                                   |            |
|Limit                |`int`   |        |       |No limit                                    |                                   |            |
|Order                |`string`|        |       |Default order                               |name ASC, item-date DESC           |            |
|ProductionProfileName|`string`|        |       |The default processing profile will be used.|MyProcessingProfile                |Nuix 7.2    |
|ProductionProfilePath|`string`|        |       |The default processing profile will be used.|C:/Profiles/MyProcessingProfile.xml|Nuix 7.6    |
|ProductionSetName    |`string`|☑️      |       |                                            |                                   |            |
|SearchTerm           |`string`|☑️      |       |                                            |                                   |            |

<a name="NuixAnnotateDocumentIdList"></a>
## NuixAnnotateDocumentIdList

**Unit**

*Requires Nuix Version 7.4*

*Requires NuixPRODUCTION_SET*

|Parameter        |Type    |Required|Summary|Example        |
|:---------------:|:------:|:------:|:-----:|:-------------:|
|CasePath         |`string`|☑️      |       |C:/Cases/MyCase|
|DataPath         |`string`|☑️      |       |               |
|ProductionSetName|`string`|☑️      |       |               |

<a name="NuixAssertPrintPreviewState"></a>
## NuixAssertPrintPreviewState

**Unit**

*Requires Nuix Version 5.2*

*Requires NuixPRODUCTION_SET*

*Requires NuixANALYSIS*

|Parameter        |Type                                   |Required|Summary|Default Value|Example        |
|:---------------:|:-------------------------------------:|:------:|:-----:|:-----------:|:-------------:|
|CasePath         |`string`                               |☑️      |       |             |C:/Cases/MyCase|
|ExpectedState    |[PrintPreviewState](#PrintPreviewState)|        |       |All          |               |
|ProductionSetName|`string`                               |☑️      |       |             |               |

<a name="NuixAssignCustodian"></a>
## NuixAssignCustodian

**Unit**

*Requires Nuix Version 5.0*

*Requires NuixANALYSIS*

|Parameter |Type    |Required|Summary|Example        |
|:--------:|:------:|:------:|:-----:|:-------------:|
|CasePath  |`string`|☑️      |       |C:/Cases/MyCase|
|Custodian |`string`|☑️      |       |               |
|SearchTerm|`string`|☑️      |       |\*.txt         |

<a name="NuixCloseConnection"></a>
## NuixCloseConnection

**Unit**

<a name="NuixCountItems"></a>
## NuixCountItems

**Int32**

*Requires Nuix Version 5.0*

|Parameter |Type    |Required|Summary|Example        |
|:--------:|:------:|:------:|:-----:|:-------------:|
|CasePath  |`string`|☑️      |       |C:/Cases/MyCase|
|SearchTerm|`string`|☑️      |       |\*.txt         |

<a name="NuixCreateCase"></a>
## NuixCreateCase

**Unit**

*Requires Nuix Version 5.0*

*Requires NuixCASE_CREATION*

|Parameter   |Type    |Required|Summary|Default Value |Example        |
|:----------:|:------:|:------:|:-----:|:------------:|:-------------:|
|CaseName    |`string`|☑️      |       |              |               |
|CasePath    |`string`|☑️      |       |              |C:/Cases/MyCase|
|Description |`string`|        |       |No Description|               |
|Investigator|`string`|☑️      |       |              |               |

<a name="NuixCreateIrregularItemsReport"></a>
## NuixCreateIrregularItemsReport

**String**

*Requires Nuix Version 5.0*

|Parameter|Type    |Required|Summary|Example        |
|:-------:|:------:|:------:|:-----:|:-------------:|
|CasePath |`string`|☑️      |       |C:/Cases/MyCase|

<a name="NuixCreateNRTReport"></a>
## NuixCreateNRTReport

**Unit**

*Requires Nuix Version 7.4*

*Requires NuixANALYSIS*

|Parameter        |Type    |Required|Summary|Example                                                                         |
|:---------------:|:------:|:------:|:-----:|:------------------------------------------------------------------------------:|
|CasePath         |`string`|☑️      |       |C:/Cases/MyCase                                                                 |
|LocalResourcesURL|`string`|☑️      |       |C:\\Program Files\\Nuix\\Nuix 8.4\\user-data\\Reports\\Case Summary\\Resources\\|
|NRTPath          |`string`|☑️      |       |                                                                                |
|OutputFormat     |`string`|☑️      |       |PDF                                                                             |
|OutputPath       |`string`|☑️      |       |C:/Temp/report.pdf                                                              |

<a name="NuixCreateReport"></a>
## NuixCreateReport

**String**

*Requires Nuix Version 6.2*

*Requires NuixANALYSIS*

|Parameter|Type    |Required|Summary|Example        |
|:-------:|:------:|:------:|:-----:|:-------------:|
|CasePath |`string`|☑️      |       |C:/Cases/MyCase|

<a name="NuixCreateTermList"></a>
## NuixCreateTermList

**String**

*Requires Nuix Version 5.0*

|Parameter|Type    |Required|Summary|Example        |
|:-------:|:------:|:------:|:-----:|:-------------:|
|CasePath |`string`|☑️      |       |C:/Cases/MyCase|

<a name="NuixDoesCaseExist"></a>
## NuixDoesCaseExist

**Boolean**

*Requires Nuix Version 5.0*

|Parameter|Type    |Required|Summary|Example        |
|:-------:|:------:|:------:|:-----:|:-------------:|
|CasePath |`string`|☑️      |       |C:/Cases/MyCase|

<a name="NuixExportConcordance"></a>
## NuixExportConcordance

**Unit**

*Requires Nuix Version 7.2*

*Requires NuixPRODUCTION_SET*

*Requires NuixEXPORT_ITEMS*

|Parameter        |Type    |Required|Summary|Example        |
|:---------------:|:------:|:------:|:-----:|:-------------:|
|CasePath         |`string`|☑️      |       |C:/Cases/MyCase|
|ExportPath       |`string`|☑️      |       |               |
|ProductionSetName|`string`|☑️      |       |               |

<a name="NuixExtractEntities"></a>
## NuixExtractEntities

**Unit**

*Requires Nuix Version 5.0*

|Parameter   |Type    |Required|Summary|Example        |
|:----------:|:------:|:------:|:-----:|:-------------:|
|CasePath    |`string`|☑️      |       |C:/Cases/MyCase|
|OutputFolder|`string`|☑️      |       |C:/Output      |

<a name="NuixGeneratePrintPreviews"></a>
## NuixGeneratePrintPreviews

**Unit**

*Requires Nuix Version 5.2*

*Requires NuixPRODUCTION_SET*

|Parameter        |Type    |Required|Summary|Example        |
|:---------------:|:------:|:------:|:-----:|:-------------:|
|CasePath         |`string`|☑️      |       |C:/Cases/MyCase|
|ProductionSetName|`string`|☑️      |       |               |

<a name="NuixGetItemProperties"></a>
## NuixGetItemProperties

**String**

*Requires Nuix Version 6.2*

|Parameter    |Type    |Required|Summary|Default Value              |Example        |
|:-----------:|:------:|:------:|:-----:|:-------------------------:|:-------------:|
|CasePath     |`string`|☑️      |       |                           |C:/Cases/MyCase|
|PropertyRegex|`string`|☑️      |       |                           |Date           |
|SearchTerm   |`string`|☑️      |       |                           |\*.txt         |
|ValueRegex   |`string`|        |       |All values will be returned|(199\\d)       |

<a name="NuixImportDocumentIds"></a>
## NuixImportDocumentIds

**Unit**

*Requires Nuix Version 7.4*

*Requires NuixPRODUCTION_SET*

|Parameter                    |Type    |Required|Summary|Default Value|Example        |
|:---------------------------:|:------:|:------:|:-----:|:-----------:|:-------------:|
|AreSourceProductionSetsInData|`bool`  |        |       |false        |               |
|CasePath                     |`string`|☑️      |       |             |C:/Cases/MyCase|
|DataPath                     |`string`|☑️      |       |             |               |
|ProductionSetName            |`string`|☑️      |       |             |               |

<a name="NuixMigrateCase"></a>
## NuixMigrateCase

**Unit**

*Requires Nuix Version 7.2*

|Parameter|Type    |Required|Summary|Example        |
|:-------:|:------:|:------:|:-----:|:-------------:|
|CasePath |`string`|☑️      |       |C:/Cases/MyCase|

<a name="NuixPerformOCR"></a>
## NuixPerformOCR

**Unit**

*Requires Nuix Version 7.6*

*Requires NuixOCR_PROCESSING*

|Parameter     |Type    |Required|Summary|Default Value                                                                                                                                      |Example                    |Requirements|
|:------------:|:------:|:------:|:-----:|:-------------------------------------------------------------------------------------------------------------------------------------------------:|:-------------------------:|:----------:|
|CasePath      |`string`|☑️      |       |                                                                                                                                                   |C:/Cases/MyCase            |            |
|OCRProfileName|`string`|        |       |The default profile will be used.                                                                                                                  |MyOcrProfile               |            |
|OCRProfilePath|`string`|        |       |The default profile will be used.                                                                                                                  |C:\\Profiles\\MyProfile.xml|Nuix 7.6    |
|SearchTerm    |`string`|        |       |NOT flag:encrypted AND ((mime-type:application/pdf AND NOT content:\*) OR (mime-type:image/\* AND ( flag:text_not_indexed OR content:( NOT \* ) )))|                           |            |

<a name="NuixRemoveFromProductionSet"></a>
## NuixRemoveFromProductionSet

**Unit**

*Requires Nuix Version 5.0*

*Requires NuixPRODUCTION_SET*

|Parameter        |Type    |Required|Summary|Default Value             |Example        |
|:---------------:|:------:|:------:|:-----:|:------------------------:|:-------------:|
|CasePath         |`string`|☑️      |       |                          |C:/Cases/MyCase|
|ProductionSetName|`string`|☑️      |       |                          |               |
|SearchTerm       |`string`|        |       |All items will be removed.|Tag:sushi      |

<a name="NuixReorderProductionSet"></a>
## NuixReorderProductionSet

**Unit**

*Requires Nuix Version 5.2*

*Requires NuixPRODUCTION_SET*

|Parameter        |Type                                             |Required|Summary|Default Value|Example        |
|:---------------:|:-----------------------------------------------:|:------:|:-----:|:-----------:|:-------------:|
|CasePath         |`string`                                         |☑️      |       |             |C:/Cases/MyCase|
|ProductionSetName|`string`                                         |☑️      |       |             |               |
|SortOrder        |[ProductionSetSortOrder](#ProductionSetSortOrder)|        |       |Position     |               |

<a name="NuixSearchAndTag"></a>
## NuixSearchAndTag

**Unit**

*Requires Nuix Version 5.0*

*Requires NuixANALYSIS*

|Parameter |Type    |Required|Summary|Example        |
|:--------:|:------:|:------:|:-----:|:-------------:|
|CasePath  |`string`|☑️      |       |C:/Cases/MyCase|
|SearchTerm|`string`|☑️      |       |\*.txt         |
|Tag       |`string`|☑️      |       |               |

# Enums
<a name="BooleanOperator"></a>
## BooleanOperator
|Name|Summary|
|:--:|:-----:|
|None|       |
|And |       |
|Or  |       |

<a name="CompareOperator"></a>
## CompareOperator
|Name              |Summary|
|:----------------:|:-----:|
|None              |       |
|Equals            |       |
|NotEquals         |       |
|LessThan          |       |
|LessThanOrEqual   |       |
|GreaterThan       |       |
|GreaterThanOrEqual|       |

<a name="DeduplicateBy"></a>
## DeduplicateBy
|Name      |Summary|
|:--------:|:-----:|
|Individual|       |
|Family    |       |

<a name="EncodingEnum"></a>
## EncodingEnum
|Name            |Summary|
|:--------------:|:-----:|
|Default         |       |
|Ascii           |       |
|BigEndianUnicode|       |
|UTF7            |       |
|UTF8            |       |
|UTF8BOM         |       |
|UTF32           |       |
|Unicode         |       |

<a name="ErrorBehaviour"></a>
## ErrorBehaviour
|Name   |Summary|
|:-----:|:-----:|
|Fail   |       |
|Warning|       |
|Ignore |       |

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

<a name="MathOperator"></a>
## MathOperator
|Name    |Summary|
|:------:|:-----:|
|None    |       |
|Add     |       |
|Subtract|       |
|Multiply|       |
|Divide  |       |
|Modulo  |       |
|Power   |       |

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

<a name="TextCase"></a>
## TextCase
|Name |Summary|
|:---:|:-----:|
|Upper|       |
|Lower|       |
|Title|       |

<a name="TrimSide"></a>
## TrimSide
|Name |Summary|
|:---:|:-----:|
|Start|       |
|End  |       |
|Both |       |
