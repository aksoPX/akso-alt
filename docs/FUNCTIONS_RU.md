# Roblox Account Manager — русская карта функций

> Цель документа: быстро понять, что делает проект, где находится код и какие функции потенциально опасны.  
> Репозиторий: `ic3w0lf22/Roblox-Account-Manager`  
> Локальная копия в workspace: `roblox-account-manager/`

## 1. Что это за программа

`Roblox Account Manager` — Windows-приложение на **C# WinForms** для локального управления несколькими Roblox-аккаунтами.

Основные возможности:

- добавление аккаунтов через браузер/логин или `.ROBLOSECURITY` cookie;
- локальное хранение списка аккаунтов в `AccountData.json`;
- шифрование данных аккаунтов;
- запуск Roblox под выбранным аккаунтом;
- запуск нескольких аккаунтов/окон Roblox;
- сохранение `PlaceId` / `JobId` для аккаунтов;
- просмотр серверов игры;
- утилиты аккаунта: пароль, email, follow privacy, блокировки, display name;
- локальный HTTP API;
- Account Control / Nexus через WebSocket для управления подключёнными Roblox-клиентами;
- watcher для процессов Roblox.

## 2. Важные файлы и папки

| Путь | Назначение |
|---|---|
| `RBX Alt Manager.sln` | Visual Studio solution. |
| `RBX Alt Manager/RBX Alt Manager.csproj` | Основной C# проект. |
| `RBX Alt Manager/AccountManager.cs` | Главное окно, загрузка аккаунтов, настройки, запуск Roblox, локальный Web API. Очень большой файл. |
| `RBX Alt Manager/AccountManager.Designer.cs` | Разметка главного WinForms окна. |
| `RBX Alt Manager/Classes/Account.cs` | Модель аккаунта и методы Roblox API: CSRF, auth ticket, смена пароля/email, блокировки, запуск, quick login и т.д. |
| `RBX Alt Manager/Classes/AccountBrowser.cs` | Открытие встроенного браузера, логин, перехват cookies. |
| `RBX Alt Manager/Forms/SettingsForm.cs` | Окно настроек. |
| `RBX Alt Manager/Forms/ServerList.cs` | Список серверов, поиск игрока, игры, избранное, universe/outfits/watcher. |
| `RBX Alt Manager/Nexus/AccountControl.cs` | Окно Account Control. Управляет подключёнными клиентами через WebSocket. |
| `RBX Alt Manager/Nexus/Nexus.lua` | Lua-скрипт, который запускается в Roblox-клиенте и подключается к Account Control. |
| `RAMAccount.lua` | Lua API-клиент для локального HTTP API программы. |
| `RBX Alt Manager/Classes/Cryptography.cs` | Парольное шифрование AccountData через libsodium. |
| `RBX Alt Manager/Classes/IniFile.cs` | Работа с `RAMSettings.ini`. |
| `RBX Alt Manager/Resources/handle.exe` | Sysinternals Handle, используется для multi Roblox / мьютекса. |
| `RBX Alt Manager/Resources/libsodium.dll` | Библиотека для шифрования. |

## 3. Главный экран

### Add Account

Кнопка/меню добавления аккаунта.

Варианты:

| UI | Что делает | Код |
|---|---|---|
| `Add Account` | Основной способ добавить аккаунт, обычно через встроенный браузер. | `AccountManager.Add_Click`, `AccountBrowser` |
| `Manual Login` | Ручной логин через браузер. | `manualToolStripMenuItem_Click` |
| `User:Pass` | Массовый импорт логин:пароль. | `bulkUserPassToolStripMenuItem_Click`, `AccountBrowser` |
| `Cookie(s)` | Импорт `.ROBLOSECURITY` cookies. | `byCookieToolStripMenuItem_Click`, `ImportForm` |
| `Custom (URL + JS)` | Открыть кастомный URL и выполнить JavaScript для автоматизации входа. | `customURLJSToolStripMenuItem_Click` |

**Риск:** `.ROBLOSECURITY` cookie даёт доступ к аккаунту. Любая функция копирования/импорта cookies должна быть явно помечена как опасная.

### Remove

Удаляет выбранный аккаунт из локального списка.

Код: `Remove_Click`, `removeAccountToolStripMenuItem_Click`.

### Open Browser

Открывает встроенный браузер уже с cookie выбранного аккаунта.

Варианты меню:

| UI | Что делает |
|---|---|
| `URL` | Открывает выбранный URL в браузере аккаунта. |
| `URL + Javascript` | Открывает URL и выполняет JS. |
| `Join Group` | Через браузер выполняет вступление в группу. |

Код: `OpenBrowser_Click`, `AccountBrowser`.

### Join Server

Запускает Roblox для выбранных аккаунтов.

Использует:

- `Place ID` — ID игры/place;
- `Job ID` — ID конкретного сервера;
- VIP-ссылку, если вставлена вместо обычного JobId/PlaceId;
- режим follow user.

Код:

- `JoinServer_Click`
- `LaunchAccounts`
- `LaunchNextAccount`
- `Account.JoinServer(...)`

### Follow

Запуск аккаунта с попыткой присоединиться к игре указанного пользователя.

Код: `Follow_Click`, `GetUserID`, `Account.JoinServer(..., follow=true)`.

### Account Utilities

Открывает окно операций над выбранным аккаунтом.

Код: `Forms/AccountUtils.cs`.

Функции:

| UI | Что делает | Код |
|---|---|---|
| `Who Can Follow` | Меняет privacy: кто может следовать за аккаунтом. | `SetFollowPrivacy` |
| `Change Password` | Меняет пароль аккаунта через Roblox API. | `ChangePassword` |
| `Sign out of all other sessions` | Обновляет cookie/логаут других сессий. | `LogOutOfOtherSessions` |
| `Change Email` | Меняет email. | `ChangeEmail` |
| `Unlock Pin` | Пытается разблокировать PIN. | `UnlockPin` |
| `Block` | Заблокировать/разблокировать пользователя. | `BlockUserId`, `UnblockUserId` |
| `Set Display Name` | Меняет display name. | `SetDisplayName` |
| `Add Friend` | Отправляет friend request. | `SendFriendRequest` |

### Utilities / ServerList

Окно дополнительных инструментов.

Код: `Forms/ServerList.cs`.

Вкладки:

| Вкладка | Что делает |
|---|---|
| `Servers` | Загружает сервера по PlaceId: JobId, Players, Ping, FPS, Region. Можно присоединиться или скопировать JobId. |
| `Games` | Поиск/просмотр Roblox games. |
| `Favorites` | Избранные игры. |
| `Universe` | Получение UniverseId и places внутри universe. |
| `Outfits` | Просмотр/примерка outfits пользователя. |
| `Watcher` | Наблюдение за процессами Roblox, авто-закрытие при ошибках/условиях. |

### Edit Theme

Редактор темы WinForms.

Код: `Forms/ThemeEditor.cs`.

Меняет:

- цвета аккаунтов;
- цвета кнопок;
- фон/текст форм;
- стиль кнопок;
- заголовки таблицы;
- тёмный top bar;
- прозрачность label background.

### Account Control

Открывает окно управления подключёнными Roblox-клиентами.

Код:

- `Nexus/AccountControl.cs`
- `Nexus/WebsocketServer.cs`
- `Nexus/ControlledAccount.cs`
- `Nexus/Nexus.lua`

Функции:

| UI | Что делает |
|---|---|
| Drag & Drop аккаунтов | Добавить аккаунты в панель контроля. |
| `Command` + `Send` | Отправить команду подключённым клиентам. |
| `Executor` | Отправить Lua-код в клиент через Nexus. Это не встроенный executor, а прокси к уже подключённому окружению. |
| `Auto Execute` | Скрипт, который будет отправлен при подключении клиента. |
| `Auto Relaunch` | Автоматически перезапускать аккаунт. |
| `Minimize Roblox` | Минимизировать окна Roblox. |
| `Close Roblox` | Закрыть окна Roblox. |
| `Auto Minimize` | Автоматическая минимизация по интервалу. |
| `Auto Close` | Автоматическое закрытие по интервалу/лимиту. |
| `Use Presence API` | Проверка состояния через Roblox Presence API вместо Nexus. |

**Риск:** выполнение Lua-кода и external WebSocket connections — мощные функции. Для безопасной сборки лучше оставить только localhost и добавить явные предупреждения.

## 4. Контекстное меню аккаунта

| Пункт | Что делает | Риск |
|---|---|---|
| `Copy Username` | Копирует username. | Низкий |
| `Copy Password` | Копирует сохранённый пароль. | Высокий |
| `Copy User Pass Combo` | Копирует `user:pass`. | Высокий |
| `Copy Profile` | Копирует ссылку на профиль. | Низкий |
| `Copy UserId` | Копирует UserId. | Низкий |
| `Quick Log In` | Использует Roblox Quick Login. | Средний/высокий |
| `Groups -> Toggle` | Переключает группировку аккаунтов. | Низкий |
| `Groups -> Move Account To` | Перемещает аккаунт в группу. | Низкий |
| `Groups -> Copy Group` | Копирует имя группы. | Низкий |
| `View Fields` | Пользовательские поля аккаунта. | Низкий/средний |
| `Dump Details` | Выгрузка деталей аккаунта. | Средний |
| `Get Authentication Ticket` | Получает auth ticket Roblox. | Высокий |
| `Copy Security Token` | Копирует `.ROBLOSECURITY`. | Критический |
| `Copy rbx-player Link` | Копирует ссылку запуска Roblox. | Высокий |
| `Copy App Link` | Копирует app link. | Высокий |

## 5. Настройки

### General

| Настройка | Значение по умолчанию | Что делает |
|---|---:|---|
| `CheckForUpdates` | `true` | Проверять обновления на GitHub. |
| `AccountJoinDelay` | `8` | Задержка между запуском аккаунтов. |
| `AsyncJoin` | `false` | Асинхронный запуск аккаунтов. |
| `DisableAgingAlert` | `false` | Отключить цветовую метку давно не использованных аккаунтов. |
| `SavePasswords` | `true` | Сохранять пароли аккаунтов локально. Для безопасной сборки лучше `false`. |
| `ServerRegionFormat` | `<city>, <countryCode>` | Формат региона сервера. |
| `MaxRecentGames` | `8` | Максимум недавних игр. |
| `ShuffleChoosesLowestServer` | `false` | Shuffle выбирает самый маленький сервер. |
| `ShufflePageCount` | `5` | Количество страниц для shuffle. |
| `IPApiLink` | `http://ip-api.com/json/<ip>` | API для региона IP сервера. |
| `AutoCookieRefresh` | `true` | Периодически обновлять cookies старых аккаунтов. |
| `AutoCloseLastProcess` | `true` | Закрывать старый процесс того же аккаунта. |
| `ShowPresence` | `true` | Показывать presence аккаунтов. |
| `PresenceUpdateRate` | `5` | Интервал обновления presence. |
| `UnlockFPS` | `false` | Патчить ClientAppSettings для FPS cap. |
| `MaxFPSValue` | `120` | FPS cap. |
| `UseCefSharpBrowser` | `false` | Использовать CefSharp вместо Chromium/Puppeteer. |

### Developer / WebServer

| Настройка | По умолчанию | Что делает |
|---|---:|---|
| `DevMode` | `false` | Показывает скрытые/опасные функции. |
| `EnableWebServer` | `false` | Включает локальный HTTP API. |
| `WebServerPort` | `7963` | Порт HTTP API. |
| `AllowGetCookie` | `false` | Разрешить endpoint выдачи cookies. Критический риск. |
| `AllowGetAccounts` | `false` | Разрешить endpoint списка аккаунтов. |
| `AllowLaunchAccount` | `false` | Разрешить запуск аккаунтов через API. |
| `AllowAccountEditing` | `false` | Разрешить изменение alias/description/fields через API. |
| `Password` | пусто | Пароль вебсервера. Должен быть 6+ символов. |
| `EveryRequestRequiresPassword` | `false` | Требовать пароль для каждого запроса. |
| `AllowExternalConnections` | `false` | Разрешить подключения не с localhost. Для безопасности лучше всегда `false`. |

### Account Control

| Настройка | По умолчанию | Что делает |
|---|---:|---|
| `NexusPort` | `5242` | WebSocket порт Account Control. |
| `AllowExternalConnections` | `false` | Разрешить WebSocket извне. Лучше `false`. |
| `RelaunchDelay` | `60` | Задержка автоперезапуска. |
| `LauncherDelayNumber` | `9` | Задержка лаунчера. |
| `StartOnLaunch` | не задано/false | Запускать Nexus при старте RAM. |
| `UsePresence` | не задано/false | Использовать Presence API для статуса. |

### Watcher

Watcher-настройки хранятся в `RAMSettings.ini`, используются в `ServerList.cs` и `RobloxWatcher.cs`:

| Настройка | Что делает |
|---|---|
| `Enabled` | Включить Roblox Watcher. |
| `ScanInterval` | Как часто сканировать процессы. |
| `ReadInterval` | Как часто читать лог. |
| `ExitOnBeta` | Закрывать Beta Home Menu. |
| `ExitIfNoConnection` | Закрывать Roblox, если нет подключения к серверу. |
| `NoConnectionTimeout` | Таймаут отсутствия соединения. |
| `IgnoreExistingProcesses` | Игнорировать процессы, которые были до старта watcher. |
| `SaveWindowPositions` | Сохранять позиции окон. |
| `VerifyDataModel` | Проверять DataModel по логам. |
| `CloseRbxMemory` | Закрывать Roblox при низком потреблении памяти. |
| `MemoryLowValue` | Порог памяти. |
| `CloseRbxWindowTitle` | Закрывать Roblox, если заголовок окна не ожидаемый. |
| `ExpectedWindowTitle` | Ожидаемый заголовок окна. |

## 6. Локальный HTTP API

Включается только при `Developer.EnableWebServer=true`.

Главный обработчик: `AccountManager.SendResponse(HttpListenerContext Context)`.

Основные endpoints:

| Endpoint | Что делает | Разрешение/риск |
|---|---|---|
| `/Running` | Проверка, что приложение запущено. | Низкий |
| `/GetAccounts` | Список usernames. | `AllowGetAccounts` |
| `/GetAccountsJson` | JSON по аккаунтам. Может включать cookies при настройках. | `AllowGetAccounts`, риск высокий |
| `/ImportCookie?Cookie=...` | Импортирует cookie как аккаунт. | Высокий |
| `/GetCookie?Account=...` | Возвращает `.ROBLOSECURITY`. | `AllowGetCookie`, критический |
| `/LaunchAccount?Account=...&PlaceId=...` | Запускает аккаунт. | `AllowLaunchAccount` |
| `/FollowUser?Account=...&Username=...` | Follow user. | `AllowLaunchAccount` |
| `/GetCSRFToken` | Возвращает CSRF token. | Средний |
| `/GetAlias`, `/GetDescription` | Возвращает локальные поля. | Низкий |
| `/SetAlias`, `/SetDescription`, `/AppendDescription` | Меняет локальное описание. | `AllowAccountEditing` |
| `/GetField`, `/SetField`, `/RemoveField` | Пользовательские поля. | `AllowAccountEditing` для записи |
| `/BlockUser`, `/UnblockUser`, `/GetBlockedList`, `/UnblockEveryone` | Управление блокировками Roblox. | Средний |
| `/SetServer`, `/SetRecommendedServer` | Сохраняет/подбирает сервер. | Средний |
| `/SetAvatar` | Меняет аватар через JSON. | Средний/высокий |

Для безопасной персональной сборки рекомендуется:

- `EnableWebServer=false` по умолчанию;
- если включён — только `localhost`;
- обязательный пароль на каждый запрос;
- `AllowGetCookie=false` всегда;
- отдельное окно предупреждения перед включением любых cookie endpoints.

## 7. Хранение и шифрование аккаунтов

Файл: `AccountData.json` в папке приложения.

Код:

- `AccountManager.LoadAccounts`
- `AccountManager.SaveAccounts`
- `Classes/Cryptography.cs`

Варианты защиты:

1. **Default Encryption**  
   Через Windows `ProtectedData` / DPAPI. Данные привязаны к машине/пользователю.

2. **Password Locked**  
   Пароль пользователя, hash через libsodium, шифрование в `Cryptography.Encrypt`.

3. **NoEncryption.IUnderstandTheRisks.iautamor**  
   Специальный файл отключает шифрование. Использовать крайне нежелательно.

## 8. Встроенный браузер

Код: `Classes/AccountBrowser.cs`.

Использует:

- PuppeteerSharp/Chromium;
- на старых системах — CefSharp;
- перехватывает cookies после логина;
- добавляет аккаунт через `AccountManager.AddAccount(...)`.

Функции:

- обычный login;
- login по user:pass;
- custom URL;
- custom URL + JS;
- join group через браузер.

## 9. Запуск Roblox

Код частично в:

- `Account.cs`
- `AccountManager.cs`
- `RobloxProcess.cs`
- `RobloxWatcher.cs`
- `ClientSettingsPatcher.cs`

Программа получает auth ticket / launch data и запускает Roblox с нужными параметрами.

Связанные функции:

- `JoinServer`
- `FollowUser`
- `ShuffleJobId`
- `QuickLogIn`
- `MultiRoblox`
- `FPS Unlocker`
- `AutoCloseLastProcess`

## 10. Multi Roblox

Идея: позволить запускать несколько клиентов Roblox одновременно.

Код:

- `UpdateMultiRoblox`
- `RobloxWatcher`
- `RobloxProcess`
- ресурс `handle.exe`

Проект предупреждает, что multi Roblox выключен по умолчанию, потому что несколько клиентов могут восприниматься Roblox/играми как нежелательное поведение.

## 11. Nexus.lua

`Nexus.lua` — Lua-скрипт для Roblox-клиента. Он:

- подключается к WebSocket `ws://localhost:5242/Nexus?...`;
- отправляет ping;
- принимает команды от Account Control;
- умеет выполнять команды:
  - `execute`
  - `teleport`
  - `rejoin`
  - `mute`
  - `unmute`
  - `performance`
- может создавать UI-элементы в Account Control через сообщения.

**Важно:** это не встроенный executor. Скрипт зависит от окружения, где доступны `WebSocket.connect`, `loadstring` и т.п.

## 12. Что лучше изменить для твоей версии

### Удобство

- Перевести интерфейс на русский.
- Добавить tooltips ко всем кнопкам.
- Добавить экран первого запуска с выбором режима:
  - простой режим;
  - расширенный режим;
  - developer mode.
- Разделить `Utilities` на понятные кнопки: `Серверы`, `Избранное`, `Watcher`, `Outfits`, `Universe`.
- Добавить встроенную справку “Что делает эта кнопка?”.

### Безопасность

- По умолчанию `SavePasswords=false`.
- По умолчанию `AutoCookieRefresh=false` или с объяснением.
- Спрятать `Copy Security Token` за двойным подтверждением.
- Запретить `AllowExternalConnections=true` без жирного предупреждения.
- Endpoint `GetCookie` оставить выключенным всегда, либо требовать ручного временного включения.
- Добавить лог аудита: когда копировали пароль/cookie/auth ticket.

### Архитектура

Сейчас `AccountManager.cs` слишком большой. Лучше вынести:

| Новый сервис | Что туда перенести |
|---|---|
| `AccountStorageService` | `LoadAccounts`, `SaveAccounts`, backup, encryption. |
| `SettingsService` | `RAMSettings.ini`, defaults, validation. |
| `RobloxLaunchService` | запуск Roblox, place/job/vip/follow. |
| `RobloxApiService` | RestSharp клиенты и Roblox API вызовы. |
| `LocalWebApiService` | `SendResponse`, endpoints, auth. |
| `BrowserLoginService` | логин/импорт через браузер. |
| `PresenceService` | presence update. |
| `NexusService` | WebSocket server, ControlledAccount. |
| `ThemeService` | тема, применение темы. |

