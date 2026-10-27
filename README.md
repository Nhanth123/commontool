# simple app
An app to do ez things



dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet tool install --global dotnet-ef


CommonTool/
├── Models/
│   └── DailyTask.cs
├── Data/
│   └── AppDbContext.cs
├── ViewModels/
│   └── MainWindowViewModel.cs
├── Views/
│   └── MainWindow.axaml
├── App.axaml
├── App.axaml.cs
├── Program.cs
├── appsettings.json
└── CommonTool.csproj