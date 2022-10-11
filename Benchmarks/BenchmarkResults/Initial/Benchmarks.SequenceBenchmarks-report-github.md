``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.978/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=6.0.401
  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2


```
|      Method |            key |          Mean |         Error |        StdDev |
|------------ |--------------- |--------------:|--------------:|--------------:|
| **RunSCLAsync** |       **BasicSCL** |      **81.81 μs** |      **1.504 μs** |      **1.174 μs** |
| **RunSCLAsync** | **CombineColumns** | **323,689.61 μs** |  **5,488.939 μs** |  **5,134.357 μs** |
| **RunSCLAsync** |   **ConvertDates** | **774,813.99 μs** | **11,798.879 μs** | **10,459.399 μs** |
| **RunSCLAsync** |       **DatToDat** | **108,424.80 μs** |  **2,034.670 μs** |  **1,998.318 μs** |
| **RunSCLAsync** |      **DatToJson** |  **98,515.58 μs** |  **1,707.830 μs** |  **1,426.115 μs** |
| **RunSCLAsync** |      **JsonToDat** |  **86,368.24 μs** |  **1,368.454 μs** |  **1,280.053 μs** |
| **RunSCLAsync** |   **RemapColumns** | **113,671.07 μs** |  **2,269.823 μs** |  **1,895.405 μs** |
| **RunSCLAsync** | **SchemaValidate** | **154,976.12 μs** |  **2,212.042 μs** |  **2,069.145 μs** |
