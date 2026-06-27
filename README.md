<div align=""center"">

# akso alt

### Русская WebUI-версия Roblox Account Manager

!Windows
!.NET Framework
!WebView2
!License

</div>

---

## О проекте

**akso alt** — изменённая русская версия менеджера аккаунтов с современным интерфейсом на **HTML/CSS/JavaScript** через **Microsoft WebView2**.

Приложение сохраняет C#-логику исходного проекта, но основной интерфейс переносится в WebUI: карточки аккаунтов, настройки, темы, запуск, сохранённые игры и инструменты.

> Основа проекта: https://github.com/ic3w0lf22/Roblox-Account-Manager  
> Оригинальная лицензия: **GPL-3.0**

---

## Возможности

- Современный WebUI-интерфейс
- Русский интерфейс
- Добавление аккаунтов через встроенный WebView2-браузер
- Запуск одного или нескольких аккаунтов
- Multi Roblox
- Запуск по Place ID / Job ID / VIP-ссылке
- Сохранение игр, VIP-ссылок, Job ID и описаний
- HTML-настройки вместо старого окна настроек
- Темы интерфейса
- Проверка рисков
- Backup данных
- Очистка буфера обмена
- GitHub-ready структура проекта

---

## Разделы интерфейса

| Раздел | Назначение |
|---|---|
| Главная | Быстрый обзор и запуск |
| Аккаунты | Поиск, выбор и управление аккаунтами |
| Запуск | Place ID, Job ID, запуск выбранных/всех аккаунтов |
| Прочее → Игры и серверы | Загрузка серверов и сохранённые VIP/Place ID |
| Прочее → Настройки | Multi Roblox, WebServer, FPS, Watcher и другие настройки |
| Прочее → Безопасность / Backup | Проверка рисков, backup, очистка буфера |
| Прочее → Тема | Цветовые темы WebUI |
| Прочее → Расширенное | Сервисные действия |

---

## Скриншоты

Скриншоты можно добавить позже в папку:

`	ext
docs/assets/
`

Пример вставки:

`md
!Главная
`

---

## Сборка

### Требования

- Windows 10/11
- Visual Studio 2022
- workload: **.NET desktop development**
- .NET Framework 4.7.2 Developer Pack
- Microsoft Edge WebView2 Runtime
- NuGet packages restore

### Восстановление пакетов

`powershell
.\nuget.exe restore "RBX Alt Manager.sln"
`

Если 
uget.exe отсутствует:

`powershell
Invoke-WebRequest https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile nuget.exe
.\nuget.exe restore "RBX Alt Manager.sln"
`

### Сборка

Открыть:

`	ext
RBX Alt Manager.sln
`

В Visual Studio:

`	ext
Сборка -> Собрать решение
`

---

## Важная безопасность

Не публикуйте и не передавайте:

- AccountData.json
- RAMSettings.ini
- RAMTheme.ini
- SavedGames.json
- .ROBLOSECURITY
- cookies
- пароли
- backup-файлы аккаунтов

Эти данные могут давать доступ к аккаунтам.

---

## Файлы, которые не должны попадать в Git

`	ext
.vs/
build/
packages/
bin/
obj/
AccountData.json
RecentGames.json
SavedGames.json
RAMSettings.ini
RAMTheme.ini
Backups/
nuget.exe
`

Они уже добавлены в .gitignore.

---

## Документация

- Сборка проекта
- Руководство пользователя
- План развития
- Уведомление об основе проекта
- Политика безопасности

---

## Основа и лицензия

Проект основан на:

**Roblox Account Manager**  
https://github.com/ic3w0lf22/Roblox-Account-Manager

Оригинальный проект распространяется под лицензией **GPL-3.0**.

kso alt является изменённой версией с русским WebUI, новым оформлением и дополнительными функциями.

---

## Статус

Проект находится в активной доработке.
