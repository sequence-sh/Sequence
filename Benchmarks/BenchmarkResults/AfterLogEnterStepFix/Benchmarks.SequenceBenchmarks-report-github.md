``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1098/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=6.0.401
  [Host]     : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2


```
|      Method |              key |          Mean |        Error |       StdDev |
|------------ |----------------- |--------------:|-------------:|-------------:|
| **RunSCLAsync** |         **BasicSCL** |      **35.39 μs** |     **0.267 μs** |     **0.237 μs** |
| **RunSCLAsync** |   **CombineColumns** | **103,820.13 μs** | **2,068.652 μs** | **2,616.183 μs** |
| **RunSCLAsync** | **ConvertDatesOnly** | **119,108.53 μs** | **2,321.698 μs** | **2,171.718 μs** |
| **RunSCLAsync** |         **DatToDat** |  **92,621.39 μs** | **1,679.359 μs** | **1,649.355 μs** |
| **RunSCLAsync** |        **DatToJson** |  **86,615.62 μs** | **1,713.325 μs** | **1,973.067 μs** |
| **RunSCLAsync** |        **JsonToDat** |  **77,218.89 μs** | **1,181.162 μs** |   **986.324 μs** |
| **RunSCLAsync** |     **RemapColumns** |  **97,755.47 μs** | **1,150.587 μs** |   **960.792 μs** |
| **RunSCLAsync** |     **RemoveColumn** |  **98,192.86 μs** | **1,937.276 μs** | **2,586.208 μs** |
| **RunSCLAsync** |   **SchemaValidate** | **131,270.77 μs** | **1,755.001 μs** | **1,641.629 μs** |
| **RunSCLAsync** |   **StringReplaces** |  **95,778.85 μs** | **1,545.883 μs** | **1,370.385 μs** |
