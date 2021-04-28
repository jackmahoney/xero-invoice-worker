# Invoice worker technical exercise - Jack Mahoney

A repository containing the technical exercise results by Jack Mahoney.
Please see comments in the code and explanations inside PLANNING and ARCHITECTURE for more information.

## Structure
Three projects in one. See root Makefile or README in each folder.

- `application`: DotNET Core console application
- `test`: xUnit tests in C#
- `mock-server`: NodeJS demo endpoint serving events

- [PLANNING.md](./PLANNING.md): establish task 
- [ARCHITECTURE.md](./ARCHITECTURE.md) discuss design decisions

## Run
See `Makefile` or run dotnet with arguments passed.

```
cd application && dotnet run -- --input-url='' --invoice-dir=''
```

## Mock server
There is a mock server provided you run the app against:

```
make spawn_mock 
make run
```
