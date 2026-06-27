# Сборка akso alt

## Требования

- Windows 10/11
- Visual Studio 2022
- .NET desktop development
- .NET Framework 4.7.2 Developer Pack
- Microsoft Edge WebView2 Runtime

## Восстановление NuGet

`powershell
Invoke-WebRequest https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile nuget.exe
.\nuget.exe restore "RBX Alt Manager.sln"
`

## Сборка

Открой RBX Alt Manager.sln и нажми:

`	ext
Сборка -> Собрать решение
`

## Частые ошибки

### Не найден Microsoft.Web.WebView2

Выполни:

`powershell
.\nuget.exe install Microsoft.Web.WebView2 -Version 1.0.2535.41 -OutputDirectory .\packages -Source https://api.nuget.org/v3/index.json
.\nuget.exe restore "RBX Alt Manager.sln"
`

### Файлы .resx заблокированы

`powershell
Get-ChildItem -Recurse -File | ForEach-Object {
    Remove-Item -LiteralPath $_.FullName -Stream Zone.Identifier -Force -ErrorAction SilentlyContinue
}
`
"@

Write-Utf8 "docs/USER_GUIDE_RU.md" @"
# Руководство пользователя

## Добавление аккаунта

1. Открой вкладку **Аккаунты**.
2. Нажми **Добавить**.
3. Войди в Roblox через WebView2-окно.
4. После успешного входа аккаунт добавится автоматически.

## Запуск аккаунта

1. Выбери аккаунт на вкладке **Аккаунты**.
2. Открой **Запуск**.
3. Введи Place ID.
4. Нажми **Запустить выбранные**.

## Несколько аккаунтов

1. Открой **Прочее -> Настройки**.
2. Включи **Multi Roblox**.
3. Закрой Roblox.
4. Перезапусти приложение.
5. Выбери несколько аккаунтов и запусти.

## Сохранённые игры / VIP

Открой **Прочее -> Игры и серверы** и сохрани:

- название
- Place ID
- Job ID
- VIP-ссылку
- описание

## Backup

Открой **Прочее -> Безопасность / Backup** и нажми **Создать backup**.