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
|[EntityStreamCreate](#EntityStreamCreate)                        |       |
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
|[Print<T>](#Print<T>)                                            |       |
|[ReadFile](#ReadFile)                                            |       |
|[Repeat<T>](#Repeat<T>)                                          |       |
|[RunExternalProcess](#RunExternalProcess)                        |       |
|[Sequence<T>](#Sequence<T>)                                      |       |
|[SetVariable<T>](#SetVariable<T>)                                |       |
|[StringContains](#StringContains)                                |       |
|[StringIsEmpty](#StringIsEmpty)                                  |       |
|[StringJoin](#StringJoin)                                        |       |
|[StringLength](#StringLength)                                    |       |
|[StringSplit](#StringSplit)                                      |       |
|[StringToCase](#StringToCase)                                    |       |
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
|[NuixRunScript](#NuixRunScript)                                  |       |
# Core
<a name="AppendString"></a>
## AppendString

**Unit**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Variable |[VariableName](#VariableName)|☑️      |       |
|String   |[StringStream](#StringStream)|☑️      |       |

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

**StringStream**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|String   |[StringStream](#StringStream)|☑️      |       |
|Index    |`int`                        |☑️      |       |

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

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Path     |[StringStream](#StringStream)|☑️      |       |

<a name="DeleteItem"></a>
## DeleteItem

**Unit**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Path     |[StringStream](#StringStream)|☑️      |       |

<a name="DirectoryExists"></a>
## DirectoryExists

**Boolean**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Path     |[StringStream](#StringStream)|☑️      |       |

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
|Schema        |[Entity](#Entity)                |☑️      |       |             |
|ErrorBehaviour|[ErrorBehaviour](#ErrorBehaviour)|        |       |Fail         |

<a name="EntityForEach"></a>
## EntityForEach

**Unit**

|Parameter   |Type                         |Required|Summary|
|:----------:|:---------------------------:|:------:|:-----:|
|EntityStream|[EntityStream](#EntityStream)|☑️      |       |
|Action      |[Unit](#Unit)                |☑️      |       |

<a name="EntityGetValue"></a>
## EntityGetValue

**StringStream**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Entity   |[Entity](#Entity)            |☑️      |       |
|Property |[StringStream](#StringStream)|☑️      |       |

<a name="EntityHasProperty"></a>
## EntityHasProperty

**Boolean**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Entity   |[Entity](#Entity)            |☑️      |       |
|Property |[StringStream](#StringStream)|☑️      |       |

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

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Entity   |[Entity](#Entity)            |☑️      |       |
|Property |[StringStream](#StringStream)|☑️      |       |
|Value    |[T](#T)                      |☑️      |       |

<a name="EntityStreamConcat"></a>
## EntityStreamConcat

**EntityStream**

|Parameter    |Type                               |Required|Summary|
|:-----------:|:---------------------------------:|:------:|:-----:|
|EntityStreams|List<[EntityStream](#EntityStream)>|☑️      |       |

<a name="EntityStreamCreate"></a>
## EntityStreamCreate

**EntityStream**

|Parameter|Type                    |Required|Summary|
|:-------:|:----------------------:|:------:|:-----:|
|Elements |IStep<[Entity](#Entity)>|☑️      |       |

<a name="EntityStreamDistinct"></a>
## EntityStreamDistinct

**EntityStream**

|Parameter   |Type                         |Required|Summary|Default Value|
|:----------:|:---------------------------:|:------:|:-----:|:-----------:|
|EntityStream|[EntityStream](#EntityStream)|☑️      |       |             |
|KeySelector |[StringStream](#StringStream)|☑️      |       |             |
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
|KeySelector |[StringStream](#StringStream)|☑️      |       |             |
|Descending  |`bool`                       |        |       |False        |

<a name="FileExists"></a>
## FileExists

**Boolean**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Path     |[StringStream](#StringStream)|☑️      |       |

<a name="FileExtract"></a>
## FileExtract

**Unit**

|Parameter      |Type                         |Required|Summary|Default Value|
|:-------------:|:---------------------------:|:------:|:-----:|:-----------:|
|ArchiveFilePath|[StringStream](#StringStream)|☑️      |       |             |
|Destination    |[StringStream](#StringStream)|☑️      |       |             |
|Overwrite      |`bool`                       |        |       |false        |

<a name="FileWrite"></a>
## FileWrite

**Unit**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Path     |[StringStream](#StringStream)|☑️      |       |
|Stream   |[StringStream](#StringStream)|☑️      |       |

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

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|String   |[StringStream](#StringStream)|☑️      |       |
|SubString|[StringStream](#StringStream)|☑️      |       |

<a name="FindSubstring"></a>
## FindSubstring

**Int32**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|String   |[StringStream](#StringStream)|☑️      |       |
|SubString|[StringStream](#StringStream)|☑️      |       |

<a name="For"></a>
## For

**Unit**

|Parameter|Type         |Required|Summary|
|:-------:|:-----------:|:------:|:-----:|
|From     |`int`        |☑️      |       |
|To       |`int`        |☑️      |       |
|Increment|`int`        |☑️      |       |
|Action   |[Unit](#Unit)|☑️      |       |

<a name="ForEach<T>"></a>
## ForEach<T>

**Unit**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Array    |List<[T](#T)>                |☑️      |       |
|Variable |[VariableName](#VariableName)|☑️      |       |
|Action   |[Unit](#Unit)                |☑️      |       |

<a name="FromConcordance"></a>
## FromConcordance

**EntityStream**

|Parameter          |Type                         |Required|Summary|Default Value|
|:-----------------:|:---------------------------:|:------:|:-----:|:-----------:|
|Stream             |[StringStream](#StringStream)|☑️      |       |             |
|Delimiter          |[StringStream](#StringStream)|        |       |\\u0014 - DC4|
|QuoteCharacter     |[StringStream](#StringStream)|        |       |þ            |
|MultiValueDelimiter|[StringStream](#StringStream)|        |       |\|           |

<a name="FromCSV"></a>
## FromCSV

**EntityStream**

|Parameter          |Type                         |Required|Summary|Default Value|
|:-----------------:|:---------------------------:|:------:|:-----:|:-----------:|
|Stream             |[StringStream](#StringStream)|☑️      |       |             |
|Delimiter          |[StringStream](#StringStream)|        |       |,            |
|CommentCharacter   |[StringStream](#StringStream)|        |       |#            |
|QuoteCharacter     |[StringStream](#StringStream)|        |       |"            |
|MultiValueDelimiter|[StringStream](#StringStream)|        |       |             |

<a name="GenerateDocumentation"></a>
## GenerateDocumentation

**StringStream**

<a name="GetSubstring"></a>
## GetSubstring

**StringStream**

|Parameter|Type                         |Required|Summary|Default Value|
|:-------:|:---------------------------:|:------:|:-----:|:-----------:|
|String   |[StringStream](#StringStream)|☑️      |       |             |
|Length   |`int`                        |☑️      |       |             |
|Index    |`int`                        |        |       |0            |

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

**Unit**

|Parameter|Type                         |Required|Summary|Default Value|
|:-------:|:---------------------------:|:------:|:-----:|:-----------:|
|Variable |[VariableName](#VariableName)|☑️      |       |             |
|Amount   |`int`                        |        |       |1            |

<a name="Not"></a>
## Not

**Boolean**

|Parameter|Type  |Required|Summary|
|:-------:|:----:|:------:|:-----:|
|Boolean  |`bool`|☑️      |       |

<a name="PathCombine"></a>
## PathCombine

**StringStream**

|Parameter|Type                               |Required|Summary|
|:-------:|:---------------------------------:|:------:|:-----:|
|Paths    |List<[StringStream](#StringStream)>|☑️      |       |

<a name="Print<T>"></a>
## Print<T>

**Unit**

|Parameter|Type   |Required|Summary|
|:-------:|:-----:|:------:|:-----:|
|Value    |[T](#T)|☑️      |       |

<a name="Print<T>"></a>
## Print<T>

**Unit**

|Parameter|Type   |Required|Summary|
|:-------:|:-----:|:------:|:-----:|
|Value    |[T](#T)|☑️      |       |

<a name="ReadFile"></a>
## ReadFile

**StringStream**

|Parameter|Type                         |Required|Summary|Default Value|
|:-------:|:---------------------------:|:------:|:-----:|:-----------:|
|Path     |[StringStream](#StringStream)|☑️      |       |             |
|Encoding |[EncodingEnum](#EncodingEnum)|        |       |UTF8 no BOM  |

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

|Parameter|Type                               |Required|Summary|Default Value   |
|:-------:|:---------------------------------:|:------:|:-----:|:--------------:|
|Path     |[StringStream](#StringStream)      |☑️      |       |                |
|Arguments|List<[StringStream](#StringStream)>|        |       |No arguments    |
|Encoding |[EncodingEnum](#EncodingEnum)      |        |       |Default encoding|

<a name="Sequence<T>"></a>
## Sequence<T>

**The same type as the final step**

|Parameter   |Type                |Required|Summary|
|:----------:|:------------------:|:------:|:-----:|
|InitialSteps|IStep<[Unit](#Unit)>|☑️      |       |
|FinalStep   |[T](#T)             |☑️      |       |

<a name="SetVariable<T>"></a>
## SetVariable<T>

**Unit**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|Variable |[VariableName](#VariableName)|☑️      |       |
|Value    |[T](#T)                      |☑️      |       |

<a name="StringContains"></a>
## StringContains

**Boolean**

|Parameter |Type                         |Required|Summary|Default Value|
|:--------:|:---------------------------:|:------:|:-----:|:-----------:|
|String    |[StringStream](#StringStream)|☑️      |       |             |
|Substring |[StringStream](#StringStream)|☑️      |       |             |
|IgnoreCase|`bool`                       |        |       |False        |

<a name="StringIsEmpty"></a>
## StringIsEmpty

**Boolean**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|String   |[StringStream](#StringStream)|☑️      |       |

<a name="StringJoin"></a>
## StringJoin

**StringStream**

|Parameter|Type                               |Required|Summary|
|:-------:|:---------------------------------:|:------:|:-----:|
|Delimiter|[StringStream](#StringStream)      |☑️      |       |
|Strings  |List<[StringStream](#StringStream)>|☑️      |       |

<a name="StringLength"></a>
## StringLength

**Int32**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|String   |[StringStream](#StringStream)|☑️      |       |

<a name="StringSplit"></a>
## StringSplit

**List`1**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|String   |[StringStream](#StringStream)|☑️      |       |
|Delimiter|[StringStream](#StringStream)|☑️      |       |

<a name="StringToCase"></a>
## StringToCase

**StringStream**

|Parameter|Type                         |Required|Summary|
|:-------:|:---------------------------:|:------:|:-----:|
|String   |[StringStream](#StringStream)|☑️      |       |
|Case     |[TextCase](#TextCase)        |☑️      |       |

<a name="StringTrim"></a>
## StringTrim

**StringStream**

|Parameter|Type                         |Required|Summary|Default Value|
|:-------:|:---------------------------:|:------:|:-----:|:-----------:|
|String   |[StringStream](#StringStream)|☑️      |       |             |
|Side     |[TrimSide](#TrimSide)        |        |       |Both         |

<a name="ToConcordance"></a>
## ToConcordance

**StringStream**

|Parameter          |Type                         |Required|Summary|Default Value                                                  |Example            |
|:-----------------:|:---------------------------:|:------:|:-----:|:-------------------------------------------------------------:|:-----------------:|
|Entities           |[EntityStream](#EntityStream)|☑️      |       |                                                               |                   |
|Encoding           |[EncodingEnum](#EncodingEnum)|        |       |UTF8 no BOM                                                    |                   |
|Delimiter          |[StringStream](#StringStream)|        |       |                                                              |                   |
|QuoteCharacter     |[StringStream](#StringStream)|        |       |þ                                                              |                   |
|AlwaysQuote        |`bool`                       |        |       |false                                                          |                   |
|MultiValueDelimiter|[StringStream](#StringStream)|        |       |                                                               |                   |
|DateTimeFormat     |[StringStream](#StringStream)|        |       |O - ISO 8601 compliant - e.g. 2009-06-15T13:45:30.0000000-07:00|yyyy/MM/dd HH:mm:ss|

<a name="ToCSV"></a>
## ToCSV

**StringStream**

|Parameter          |Type                         |Required|Summary|Default Value                                                  |Example            |
|:-----------------:|:---------------------------:|:------:|:-----:|:-------------------------------------------------------------:|:-----------------:|
|Entities           |[EntityStream](#EntityStream)|☑️      |       |                                                               |                   |
|Encoding           |[EncodingEnum](#EncodingEnum)|        |       |UTF8 no BOM                                                    |                   |
|Delimiter          |[StringStream](#StringStream)|        |       |,                                                              |                   |
|QuoteCharacter     |[StringStream](#StringStream)|        |       |"                                                              |                   |
|AlwaysQuote        |`bool`                       |        |       |false                                                          |                   |
|MultiValueDelimiter|[StringStream](#StringStream)|        |       |                                                               |                   |
|DateTimeFormat     |[StringStream](#StringStream)|        |       |O - ISO 8601 compliant - e.g. 2009-06-15T13:45:30.0000000-07:00|yyyy/MM/dd HH:mm:ss|

<a name="ValueIf<T>"></a>
## ValueIf<T>

**T**

|Parameter|Type   |Required|Summary|
|:-------:|:-----:|:------:|:-----:|
|Condition|`bool` |☑️      |       |
|Then     |[T](#T)|☑️      |       |
|Else     |[T](#T)|☑️      |       |

<a name="While"></a>
## While

**Unit**

|Parameter|Type         |Required|Summary|
|:-------:|:-----------:|:------:|:-----:|
|Condition|`bool`       |☑️      |       |
|Action   |[Unit](#Unit)|☑️      |       |

# Nuix
<a name="NuixAddConcordance"></a>
## NuixAddConcordance

**Unit**

*Requires Nuix Version 7.6*

*Requires NuixCASE_CREATION*

*Requires NuixMETADATA_IMPORT*

|Parameter             |Type                         |Required|Summary|Default Value |Example                   |
|:--------------------:|:---------------------------:|:------:|:-----:|:------------:|:------------------------:|
|CasePath              |[StringStream](#StringStream)|☑️      |       |              |C:/Cases/MyCase           |
|FolderName            |[StringStream](#StringStream)|☑️      |       |              |                          |
|Custodian             |[StringStream](#StringStream)|☑️      |       |              |                          |
|FilePath              |[StringStream](#StringStream)|☑️      |       |              |C:/MyConcordance.dat      |
|ConcordanceDateFormat |[StringStream](#StringStream)|☑️      |       |              |yyyy-MM-dd'T'HH:mm:ss.SSSZ|
|ConcordanceProfileName|[StringStream](#StringStream)|☑️      |       |              |MyProfile                 |
|Description           |[StringStream](#StringStream)|        |       |No description|                          |

<a name="NuixAddItem"></a>
## NuixAddItem

**Unit**

*Requires Nuix Version 5.0*

*Requires NuixCASE_CREATION*

|Parameter                 |Type                               |Required|Summary|Default Value                                   |Example                            |Requirements|
|:------------------------:|:---------------------------------:|:------:|:-----:|:----------------------------------------------:|:---------------------------------:|:----------:|
|CasePath                  |[StringStream](#StringStream)      |☑️      |       |                                                |C:/Cases/MyCase                    |            |
|FolderName                |[StringStream](#StringStream)      |☑️      |       |                                                |                                   |            |
|Custodian                 |[StringStream](#StringStream)      |☑️      |       |                                                |                                   |            |
|Paths                     |List<[StringStream](#StringStream)>|☑️      |       |                                                |C:/Data/File.txt                   |            |
|Description               |[StringStream](#StringStream)      |        |       |No Description                                  |                                   |            |
|ProcessingProfileName     |[StringStream](#StringStream)      |        |       |The default processing profile will be used.    |MyProcessingProfile                |Nuix 7.6    |
|ProcessingProfilePath     |[StringStream](#StringStream)      |        |       |The default processing profile will be used.    |C:/Profiles/MyProcessingProfile.xml|Nuix 7.6    |
|ProcessingSettings        |[Entity](#Entity)                  |        |       |Processing settings will not be changed         |                                   |            |
|ParallelProcessingSettings|[Entity](#Entity)                  |        |       |Parallel processing settings will not be changed|                                   |            |
|PasswordFilePath          |[StringStream](#StringStream)      |        |       |Do not attempt decryption                       |C:/Data/Passwords.txt              |Nuix 7.6    |
|MimeTypeSettings          |[EntityStream](#EntityStream)      |        |       |Use default settings for all MIME types         |                                   |Nuix 8.2    |

<a name="NuixAddToItemSet"></a>
## NuixAddToItemSet

**Unit**

*Requires Nuix Version 5.0*

*Requires NuixANALYSIS*

|Parameter           |Type                                         |Required|Summary|Default Value         |Example                 |
|:------------------:|:-------------------------------------------:|:------:|:-----:|:--------------------:|:----------------------:|
|CasePath            |[StringStream](#StringStream)                |☑️      |       |                      |C:/Cases/MyCase         |
|SearchTerm          |[StringStream](#StringStream)                |☑️      |       |                      |                        |
|ItemSetName         |[StringStream](#StringStream)                |☑️      |       |                      |                        |
|ItemSetDeduplication|[ItemSetDeduplication](#ItemSetDeduplication)|        |       |No deduplication      |                        |
|ItemSetDescription  |[StringStream](#StringStream)                |        |       |No description        |                        |
|DeduplicateBy       |[DeduplicateBy](#DeduplicateBy)              |        |       |Neither               |                        |
|CustodianRanking    |List<[StringStream](#StringStream)>          |        |       |Do not rank custodians|                        |
|Order               |[StringStream](#StringStream)                |        |       |Do not reorder        |name ASC, item-date DESC|
|Limit               |`int`                                        |        |       |No limit              |                        |

<a name="NuixAddToProductionSet"></a>
## NuixAddToProductionSet

**Unit**

*Requires Nuix Version 7.2*

*Requires NuixPRODUCTION_SET*

|Parameter            |Type                         |Required|Summary|Default Value                               |Example                            |Requirements|
|:-------------------:|:---------------------------:|:------:|:-----:|:------------------------------------------:|:---------------------------------:|:----------:|
|CasePath             |[StringStream](#StringStream)|☑️      |       |                                            |C:/Cases/MyCase                    |            |
|SearchTerm           |[StringStream](#StringStream)|☑️      |       |                                            |                                   |            |
|ProductionSetName    |[StringStream](#StringStream)|☑️      |       |                                            |                                   |            |
|Description          |[StringStream](#StringStream)|        |       |No description                              |                                   |            |
|ProductionProfileName|[StringStream](#StringStream)|        |       |The default processing profile will be used.|MyProcessingProfile                |Nuix 7.2    |
|ProductionProfilePath|[StringStream](#StringStream)|        |       |The default processing profile will be used.|C:/Profiles/MyProcessingProfile.xml|Nuix 7.6    |
|Order                |[StringStream](#StringStream)|        |       |Default order                               |name ASC, item-date DESC           |            |
|Limit                |`int`                        |        |       |No limit                                    |                                   |            |

<a name="NuixAnnotateDocumentIdList"></a>
## NuixAnnotateDocumentIdList

**Unit**

*Requires Nuix Version 7.4*

*Requires NuixPRODUCTION_SET*

|Parameter        |Type                         |Required|Summary|Example        |
|:---------------:|:---------------------------:|:------:|:-----:|:-------------:|
|CasePath         |[StringStream](#StringStream)|☑️      |       |C:/Cases/MyCase|
|ProductionSetName|[StringStream](#StringStream)|☑️      |       |               |
|DataPath         |[StringStream](#StringStream)|☑️      |       |               |

<a name="NuixAssertPrintPreviewState"></a>
## NuixAssertPrintPreviewState

**Unit**

*Requires Nuix Version 5.2*

*Requires NuixPRODUCTION_SET*

*Requires NuixANALYSIS*

|Parameter        |Type                                   |Required|Summary|Default Value|Example        |
|:---------------:|:-------------------------------------:|:------:|:-----:|:-----------:|:-------------:|
|CasePath         |[StringStream](#StringStream)          |☑️      |       |             |C:/Cases/MyCase|
|ProductionSetName|[StringStream](#StringStream)          |☑️      |       |             |               |
|ExpectedState    |[PrintPreviewState](#PrintPreviewState)|        |       |All          |               |

<a name="NuixAssignCustodian"></a>
## NuixAssignCustodian

**Unit**

*Requires Nuix Version 5.0*

*Requires NuixANALYSIS*

|Parameter |Type                         |Required|Summary|Example        |
|:--------:|:---------------------------:|:------:|:-----:|:-------------:|
|CasePath  |[StringStream](#StringStream)|☑️      |       |C:/Cases/MyCase|
|SearchTerm|[StringStream](#StringStream)|☑️      |       |\*.txt         |
|Custodian |[StringStream](#StringStream)|☑️      |       |               |

<a name="NuixCloseConnection"></a>
## NuixCloseConnection

**Unit**

<a name="NuixCountItems"></a>
## NuixCountItems

**Int32**

*Requires Nuix Version 5.0*

|Parameter |Type                         |Required|Summary|Example        |
|:--------:|:---------------------------:|:------:|:-----:|:-------------:|
|CasePath  |[StringStream](#StringStream)|☑️      |       |C:/Cases/MyCase|
|SearchTerm|[StringStream](#StringStream)|☑️      |       |\*.txt         |

<a name="NuixCreateCase"></a>
## NuixCreateCase

**Unit**

*Requires Nuix Version 5.0*

*Requires NuixCASE_CREATION*

|Parameter   |Type                         |Required|Summary|Default Value |Example        |
|:----------:|:---------------------------:|:------:|:-----:|:------------:|:-------------:|
|CasePath    |[StringStream](#StringStream)|☑️      |       |              |C:/Cases/MyCase|
|CaseName    |[StringStream](#StringStream)|☑️      |       |              |               |
|Investigator|[StringStream](#StringStream)|☑️      |       |              |               |
|Description |[StringStream](#StringStream)|        |       |No Description|               |

<a name="NuixCreateIrregularItemsReport"></a>
## NuixCreateIrregularItemsReport

**StringStream**

*Requires Nuix Version 5.0*

|Parameter|Type                         |Required|Summary|Example        |
|:-------:|:---------------------------:|:------:|:-----:|:-------------:|
|CasePath |[StringStream](#StringStream)|☑️      |       |C:/Cases/MyCase|

<a name="NuixCreateNRTReport"></a>
## NuixCreateNRTReport

**Unit**

*Requires Nuix Version 7.4*

*Requires NuixANALYSIS*

|Parameter        |Type                         |Required|Summary|Example                                                                         |
|:---------------:|:---------------------------:|:------:|:-----:|:------------------------------------------------------------------------------:|
|CasePath         |[StringStream](#StringStream)|☑️      |       |C:/Cases/MyCase                                                                 |
|NRTPath          |[StringStream](#StringStream)|☑️      |       |                                                                                |
|OutputFormat     |[StringStream](#StringStream)|☑️      |       |PDF                                                                             |
|OutputPath       |[StringStream](#StringStream)|☑️      |       |C:/Temp/report.pdf                                                              |
|LocalResourcesURL|[StringStream](#StringStream)|☑️      |       |C:\\Program Files\\Nuix\\Nuix 8.4\\user-data\\Reports\\Case Summary\\Resources\\|

<a name="NuixCreateReport"></a>
## NuixCreateReport

**StringStream**

*Requires Nuix Version 6.2*

*Requires NuixANALYSIS*

|Parameter|Type                         |Required|Summary|Example        |
|:-------:|:---------------------------:|:------:|:-----:|:-------------:|
|CasePath |[StringStream](#StringStream)|☑️      |       |C:/Cases/MyCase|

<a name="NuixCreateTermList"></a>
## NuixCreateTermList

**StringStream**

*Requires Nuix Version 5.0*

|Parameter|Type                         |Required|Summary|Example        |
|:-------:|:---------------------------:|:------:|:-----:|:-------------:|
|CasePath |[StringStream](#StringStream)|☑️      |       |C:/Cases/MyCase|

<a name="NuixDoesCaseExist"></a>
## NuixDoesCaseExist

**Boolean**

*Requires Nuix Version 5.0*

|Parameter|Type                         |Required|Summary|Example        |
|:-------:|:---------------------------:|:------:|:-----:|:-------------:|
|CasePath |[StringStream](#StringStream)|☑️      |       |C:/Cases/MyCase|

<a name="NuixExportConcordance"></a>
## NuixExportConcordance

**Unit**

*Requires Nuix Version 7.2*

*Requires NuixPRODUCTION_SET*

*Requires NuixEXPORT_ITEMS*

|Parameter        |Type                         |Required|Summary|Example        |
|:---------------:|:---------------------------:|:------:|:-----:|:-------------:|
|CasePath         |[StringStream](#StringStream)|☑️      |       |C:/Cases/MyCase|
|ExportPath       |[StringStream](#StringStream)|☑️      |       |               |
|ProductionSetName|[StringStream](#StringStream)|☑️      |       |               |

<a name="NuixExtractEntities"></a>
## NuixExtractEntities

**Unit**

*Requires Nuix Version 5.0*

|Parameter   |Type                         |Required|Summary|Example        |
|:----------:|:---------------------------:|:------:|:-----:|:-------------:|
|CasePath    |[StringStream](#StringStream)|☑️      |       |C:/Cases/MyCase|
|OutputFolder|[StringStream](#StringStream)|☑️      |       |C:/Output      |

<a name="NuixGeneratePrintPreviews"></a>
## NuixGeneratePrintPreviews

**Unit**

*Requires Nuix Version 5.2*

*Requires NuixPRODUCTION_SET*

|Parameter        |Type                         |Required|Summary|Example        |
|:---------------:|:---------------------------:|:------:|:-----:|:-------------:|
|CasePath         |[StringStream](#StringStream)|☑️      |       |C:/Cases/MyCase|
|ProductionSetName|[StringStream](#StringStream)|☑️      |       |               |

<a name="NuixGetItemProperties"></a>
## NuixGetItemProperties

**StringStream**

*Requires Nuix Version 6.2*

|Parameter    |Type                         |Required|Summary|Default Value              |Example        |
|:-----------:|:---------------------------:|:------:|:-----:|:-------------------------:|:-------------:|
|CasePath     |[StringStream](#StringStream)|☑️      |       |                           |C:/Cases/MyCase|
|SearchTerm   |[StringStream](#StringStream)|☑️      |       |                           |\*.txt         |
|PropertyRegex|[StringStream](#StringStream)|☑️      |       |                           |Date           |
|ValueRegex   |[StringStream](#StringStream)|        |       |All values will be returned|(199\\d)       |

<a name="NuixImportDocumentIds"></a>
## NuixImportDocumentIds

**Unit**

*Requires Nuix Version 7.4*

*Requires NuixPRODUCTION_SET*

|Parameter                    |Type                         |Required|Summary|Default Value|Example        |
|:---------------------------:|:---------------------------:|:------:|:-----:|:-----------:|:-------------:|
|CasePath                     |[StringStream](#StringStream)|☑️      |       |             |C:/Cases/MyCase|
|ProductionSetName            |[StringStream](#StringStream)|☑️      |       |             |               |
|DataPath                     |[StringStream](#StringStream)|☑️      |       |             |               |
|AreSourceProductionSetsInData|`bool`                       |        |       |false        |               |

<a name="NuixMigrateCase"></a>
## NuixMigrateCase

**Unit**

*Requires Nuix Version 7.2*

|Parameter|Type                         |Required|Summary|Example        |
|:-------:|:---------------------------:|:------:|:-----:|:-------------:|
|CasePath |[StringStream](#StringStream)|☑️      |       |C:/Cases/MyCase|

<a name="NuixPerformOCR"></a>
## NuixPerformOCR

**Unit**

*Requires Nuix Version 7.6*

*Requires NuixOCR_PROCESSING*

|Parameter     |Type                         |Required|Summary|Default Value                                                                                                                                      |Example                    |Requirements|
|:------------:|:---------------------------:|:------:|:-----:|:-------------------------------------------------------------------------------------------------------------------------------------------------:|:-------------------------:|:----------:|
|CasePath      |[StringStream](#StringStream)|☑️      |       |                                                                                                                                                   |C:/Cases/MyCase            |            |
|SearchTerm    |[StringStream](#StringStream)|        |       |NOT flag:encrypted AND ((mime-type:application/pdf AND NOT content:\*) OR (mime-type:image/\* AND ( flag:text_not_indexed OR content:( NOT \* ) )))|                           |            |
|OCRProfileName|[StringStream](#StringStream)|        |       |The default profile will be used.                                                                                                                  |MyOcrProfile               |            |
|OCRProfilePath|[StringStream](#StringStream)|        |       |The default profile will be used.                                                                                                                  |C:\\Profiles\\MyProfile.xml|Nuix 7.6    |

<a name="NuixRemoveFromProductionSet"></a>
## NuixRemoveFromProductionSet

**Unit**

*Requires Nuix Version 5.0*

*Requires NuixPRODUCTION_SET*

|Parameter        |Type                         |Required|Summary|Default Value             |Example        |
|:---------------:|:---------------------------:|:------:|:-----:|:------------------------:|:-------------:|
|CasePath         |[StringStream](#StringStream)|☑️      |       |                          |C:/Cases/MyCase|
|ProductionSetName|[StringStream](#StringStream)|☑️      |       |                          |               |
|SearchTerm       |[StringStream](#StringStream)|        |       |All items will be removed.|Tag:sushi      |

<a name="NuixReorderProductionSet"></a>
## NuixReorderProductionSet

**Unit**

*Requires Nuix Version 5.2*

*Requires NuixPRODUCTION_SET*

|Parameter        |Type                                             |Required|Summary|Default Value|Example        |
|:---------------:|:-----------------------------------------------:|:------:|:-----:|:-----------:|:-------------:|
|CasePath         |[StringStream](#StringStream)                    |☑️      |       |             |C:/Cases/MyCase|
|ProductionSetName|[StringStream](#StringStream)                    |☑️      |       |             |               |
|SortOrder        |[ProductionSetSortOrder](#ProductionSetSortOrder)|        |       |Position     |               |

<a name="NuixSearchAndTag"></a>
## NuixSearchAndTag

**Unit**

*Requires Nuix Version 5.0*

*Requires NuixANALYSIS*

|Parameter |Type                         |Required|Summary|Example        |
|:--------:|:---------------------------:|:------:|:-----:|:-------------:|
|CasePath  |[StringStream](#StringStream)|☑️      |       |C:/Cases/MyCase|
|SearchTerm|[StringStream](#StringStream)|☑️      |       |\*.txt         |
|Tag       |[StringStream](#StringStream)|☑️      |       |               |

<a name="NuixRunScript"></a>
## NuixRunScript

**StringStream**

*Requires Nuix Version 8.2*

|Parameter            |Type                         |Required|Summary|Default Value         |
|:-------------------:|:---------------------------:|:------:|:-----:|:--------------------:|
|FunctionName         |[StringStream](#StringStream)|☑️      |       |                      |
|ScriptText           |[StringStream](#StringStream)|☑️      |       |                      |
|Parameters           |[Entity](#Entity)            |☑️      |       |                      |
|EntityStreamParameter|[EntityStream](#EntityStream)|        |       |Do not stream entities|

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
