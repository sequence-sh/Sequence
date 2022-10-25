``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1098/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=6.0.401
  [Host]     : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2


```
|      Method |              key |          Mean |        Error |       StdDev |
|------------ |----------------- |--------------:|-------------:|-------------:|
| **RunSCLAsync** |         **BasicSCL** |      **35.81 μs** |     **0.704 μs** |     **0.723 μs** |
| **RunSCLAsync** |   **CombineColumns** |  **70,840.40 μs** | **1,335.530 μs** | **1,484.438 μs** |
| **RunSCLAsync** | **ConvertDatesOnly** |  **62,156.69 μs** | **1,130.109 μs** | **1,546.906 μs** |
| **RunSCLAsync** |         **DatToDat** |  **61,528.62 μs** | **1,209.297 μs** | **1,882.729 μs** |
| **RunSCLAsync** |        **DatToJson** |  **41,558.64 μs** |   **710.775 μs** |   **760.521 μs** |
| **RunSCLAsync** |        **JsonToDat** |  **50,542.41 μs** |   **988.105 μs** |   **924.274 μs** |
| **RunSCLAsync** |     **RemapColumns** |  **60,397.03 μs** |   **288.945 μs** |   **270.280 μs** |
| **RunSCLAsync** |     **RemoveColumn** |  **62,817.56 μs** | **1,238.112 μs** | **2,200.743 μs** |
| **RunSCLAsync** |   **SchemaValidate** | **104,101.35 μs** | **1,274.464 μs** | **1,129.779 μs** |
| **RunSCLAsync** |   **StringReplaces** |  **52,792.74 μs** | **1,041.253 μs** | **1,022.650 μs** |
