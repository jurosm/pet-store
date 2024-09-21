# Run tests and get coverage report

- go to a test project
- run tests, dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
- reportgenerator -reports:coverage.cobertura.xml -targetdir:"coveragereport" -reporttypes:Html
