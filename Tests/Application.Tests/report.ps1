dotnet test --collect:"XPlat Code Coverage"

reportgenerator -reports:".\TestResults\{guid}\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html -classfilters:'-Infrastructure.Persistence.*'