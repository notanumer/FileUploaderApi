## Required software

- Visual Studio 2022 (https://www.visualstudio.com/downloads/download-visual-studio-vs.aspx) or JetBrains Rider
- .NET SDK 7 (https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- PostgreSQL (https://www.postgresql.org/download/)
- git

## Project initialization

1. Copy `src\FileUploader.Server\appsettings.json.template` file to `src\FileUploader.Server\appsettings.Development.json`

2. Update the `ConnectionStrings:AppDatabase` and `ServerUrl` settings in that file to target your local development host/database

3. Run application