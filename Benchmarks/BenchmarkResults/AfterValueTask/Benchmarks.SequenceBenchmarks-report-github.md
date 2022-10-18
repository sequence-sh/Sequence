``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1098/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=6.0.401
  [Host]     : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2


```
|      Method |              key |          Mean |        Error |       StdDev |
|------------ |----------------- |--------------:|-------------:|-------------:|
| **RunSCLAsync** |         **BasicSCL** |      **64.56 μs** |     **0.470 μs** |     **0.440 μs** |
| **RunSCLAsync** |   **CombineColumns** | **306,125.29 μs** | **6,032.978 μs** | **5,643.251 μs** |
| **RunSCLAsync** | **ConvertDatesOnly** | **120,553.24 μs** |   **832.727 μs** |   **778.934 μs** |
| **RunSCLAsync** |         **DatToDat** |  **93,733.29 μs** |   **776.055 μs** |   **648.041 μs** |
| **RunSCLAsync** |        **DatToJson** |  **86,622.87 μs** |   **822.600 μs** |   **769.461 μs** |
| **RunSCLAsync** |        **JsonToDat** |  **74,910.93 μs** | **1,415.744 μs** | **1,630.373 μs** |
| **RunSCLAsync** |     **RemapColumns** |  **98,365.44 μs** | **1,929.528 μs** | **2,369.633 μs** |
| **RunSCLAsync** |     **RemoveColumn** | **148,584.52 μs** | **2,108.815 μs** | **1,972.587 μs** |
| **RunSCLAsync** |   **SchemaValidate** | **131,679.90 μs** |   **772.463 μs** |   **684.768 μs** |
| **RunSCLAsync** |   **StringReplaces** | **328,927.93 μs** | **2,399.181 μs** | **2,244.195 μs** |
