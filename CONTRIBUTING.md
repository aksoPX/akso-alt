# Contributing

## Rules

- Do not commit account data, cookies, passwords or logs.
- Keep WebUI files in RBX Alt Manager/WebUI/.
- Keep C# bridge logic in AccountManager.cs or dedicated classes.
- Prefer safe defaults for WebServer and external connections.

## Build

Run:

`powershell
.\nuget.exe restore "RBX Alt Manager.sln"
`

Then build the solution in Visual Studio.