# utils.fs
Use paket and modify project file to include .fsx utils module

```paket
github RickyYCheng/utils.fs:main utils.fsx
```

But we must reference in project file manually instead of using `paket.references`. 
```fsproj
<Compile Include="..\..\paket-files\RickyYCheng\utils.fs\utils.fsx" Link="utils.fsx" />
```
