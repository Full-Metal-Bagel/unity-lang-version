> replace project's `<LangVersion>` based on `-langVersion` option of "csc.rsp" file

```
// csc.rsp
-langVersion:10

// *.csproj
...
    <LangVersion>10.0</LangVersion>
...
```

```
// csc.rsp
-langVersion:preview

// *.csproj
...
    <LangVersion>11.0</LangVersion>
...
```
