using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

/// <summary>
/// Runtime Russian localization for the akso alt build.
///
/// The original project stores most UI strings directly in *.Designer.cs files.
/// Editing Designer files by hand is fragile: Visual Studio may overwrite them.
/// This helper localizes forms after InitializeComponent(), using control/field names.
/// </summary>
public static class RussianLocalization
{
    public const string AppName = "akso alt";

    private static readonly Dictionary<string, string> FormTitles = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["AccountManager"] = AppName,
        ["SettingsForm"] = "Настройки — " + AppName,
        ["ServerList"] = "Инструменты — " + AppName,
        ["AccountControl"] = "Управление аккаунтами — " + AppName,
        ["AccountUtils"] = "Утилиты аккаунта — " + AppName,
        ["AccountFields"] = "Поля аккаунта — " + AppName,
        ["ArgumentsForm"] = "Аргументы запуска — " + AppName,
        ["ImportForm"] = "Импорт аккаунтов — " + AppName,
        ["MissingAssets"] = "Недостающие предметы — " + AppName,
        ["RecentGamesForm"] = "Недавние игры — " + AppName,
        ["ThemeEditor"] = "Редактор темы — " + AppName,
        ["AutoUpdater"] = "Автообновление — " + AppName,
    };

    private static readonly Dictionary<string, string> Text = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        // Main window
        ["AccountManager.LabelJobID"] = "Job ID / ID сервера",
        ["AccountManager.manualToolStripMenuItem"] = "Ручной вход",
        ["AccountManager.bulkUserPassToolStripMenuItem"] = "Логин:Пароль",
        ["AccountManager.byCookieToolStripMenuItem"] = "Cookie(s)",
        ["AccountManager.customURLJSToolStripMenuItem"] = "Свой URL + JS",
        ["AccountManager.Remove"] = "Удалить",
        ["AccountManager.JoinServer"] = "Запустить / войти",
        ["AccountManager.SetDescription"] = "Задать описание",
        ["AccountManager.SetAlias"] = "Задать псевдоним",
        ["AccountManager.Follow"] = "Follow",
        ["AccountManager.LabelUserID"] = "Имя пользователя",
        ["AccountManager.ServerList"] = "Инструменты",
        ["AccountManager.addAccountsToolStripMenuItem"] = "Добавить аккаунт",
        ["AccountManager.removeAccountToolStripMenuItem"] = "Удалить аккаунт",
        ["AccountManager.copyToolStripMenuItem"] = "Копировать",
        ["AccountManager.copyUsernameToolStripMenuItem"] = "Копировать имя",
        ["AccountManager.copyPasswordToolStripMenuItem"] = "⚠ Копировать пароль",
        ["AccountManager.copyUserPassComboToolStripMenuItem"] = "⚠ Копировать логин:пароль",
        ["AccountManager.copyProfileToolStripMenuItem"] = "Копировать профиль",
        ["AccountManager.copyUserIdToolStripMenuItem"] = "Копировать UserId",
        ["AccountManager.sortAlphabeticallyToolStripMenuItem"] = "Сортировать по алфавиту",
        ["AccountManager.quickLogInToolStripMenuItem"] = "Быстрый вход",
        ["AccountManager.moveGroupUpToolStripMenuItem"] = "Группы",
        ["AccountManager.toggleToolStripMenuItem"] = "Переключить",
        ["AccountManager.moveToToolStripMenuItem"] = "Переместить аккаунт в...",
        ["AccountManager.copyGroupToolStripMenuItem"] = "Копировать группу",
        ["AccountManager.infoToolStripMenuItem"] = "Справка",
        ["AccountManager.groupsToolStripMenuItem"] = "Группы",
        ["AccountManager.infoToolStripMenuItem1"] = "Информация",
        ["AccountManager.viewFieldsToolStripMenuItem"] = "Поля аккаунта",
        ["AccountManager.ShowDetailsToolStripMenuItem"] = "Выгрузить детали",
        ["AccountManager.getAuthenticationTicketToolStripMenuItem"] = "⚠ Получить Authentication Ticket",
        ["AccountManager.copySecurityTokenToolStripMenuItem"] = "⚠ Копировать Security Token",
        ["AccountManager.copyRbxplayerLinkToolStripMenuItem"] = "⚠ Копировать rbx-player ссылку",
        ["AccountManager.copyAppLinkToolStripMenuItem"] = "⚠ Копировать app-ссылку",
        ["AccountManager.HideUsernamesCheckbox"] = "Скрыть имена",
        ["AccountManager.BrowserButton"] = "Утилиты аккаунта",
        ["AccountManager.CurrentPlace"] = "Текущая игра",
        ["AccountManager.LabelPlaceID"] = "Place ID / ID игры",
        ["AccountManager.customURLToolStripMenuItem"] = "URL",
        ["AccountManager.URLJSToolStripMenuItem"] = "URL + JavaScript",
        ["AccountManager.joinGroupToolStripMenuItem"] = "Вступить в группу",
        ["AccountManager.DefaultEncryptionButton"] = "Обычное шифрование Windows",
        ["AccountManager.PasswordEncryptionButton"] = "Защита паролем (рекомендуется)",
        ["AccountManager.Username"] = "Имя",
        ["AccountManager.AccountAlias"] = "Псевдоним",
        ["AccountManager.Description"] = "Описание",
        ["AccountManager.LastUsedColumn"] = "Последнее использование",
        ["AccountManager.EditTheme"] = "Тема",
        ["AccountManager.LaunchNexus"] = "Управление аккаунтами",
        ["AccountManager.UnlockButton"] = "Продолжить",
        ["AccountManager.AccessLabel"] = "Ограниченный доступ",
        ["AccountManager.PasswordRequiredLabel"] = "Введите пароль, чтобы продолжить",
        ["AccountManager.SetPasswordButton"] = "Продолжить",
        ["AccountManager.Password2Label"] = "Пароль",
        ["AccountManager.EncSelectionLabel"] = "Выберите способ защиты данных",
        ["AccountManager.DLChromiumLabel"] = "Загрузка Chromium...",
        ["AccountManager.Add"] = "Добавить аккаунт",
        ["AccountManager.OpenBrowser"] = "Открыть браузер",
        ["AccountManager.Alias"] = "Псевдоним",
        ["AccountManager.DescriptionBox"] = "Описание",

        // Account utilities
        ["AccountUtils.WhoFollow"] = "Кто может следовать",
        ["AccountUtils.label1"] = "Текущий пароль",
        ["AccountUtils.label2"] = "Новый пароль",
        ["AccountUtils.button1"] = "Сменить пароль",
        ["AccountUtils.button2"] = "Выйти из других сессий",
        ["AccountUtils.button3"] = "Сменить email",
        ["AccountUtils.textBox3"] = "Email",
        ["AccountUtils.textBox5"] = "PIN",
        ["AccountUtils.button7"] = "Разблокировать",
        ["AccountUtils.label3"] = "Редактируется аккаунт, выбранный\r\nв главном окне менеджера\r\n",
        ["AccountUtils.Block"] = "Блок / разблокировка",
        ["AccountUtils.SetDisplayName"] = "Задать Display Name",
        ["AccountUtils.AddFriend"] = "Добавить в друзья",
        ["AccountUtils.label4"] = "Целевой username",

        // Settings
        ["SettingsForm.AutoUpdateCB"] = "Проверять обновления",
        ["SettingsForm.AsyncJoinCB"] = "Асинхронный запуск",
        ["SettingsForm.DelayLabel"] = "Задержка запуска",
        ["SettingsForm.SavePasswordCB"] = "Сохранять пароли",
        ["SettingsForm.DisableAgingAlertCB"] = "Отключить индикатор давности",
        ["SettingsForm.HideMRobloxCB"] = "Скрыть предупреждение Multi Roblox",
        ["SettingsForm.StartOnPCStartup"] = "Запускать вместе с Windows",
        ["SettingsForm.ShuffleLowestServerCB"] = "Shuffle выбирает малый сервер",
        ["SettingsForm.MultiRobloxCB"] = "Multi Roblox",
        ["SettingsForm.AutoCookieRefreshCB"] = "Автообновление cookies",
        ["SettingsForm.RegionFormatLabel"] = "Формат региона",
        ["SettingsForm.MRGLabel"] = "Макс. недавних игр",
        ["SettingsForm.EncryptionSelectionButton"] = "Сменить способ шифрования",
        ["SettingsForm.WSPWLabel"] = "Пароль веб-сервера",
        ["SettingsForm.GeneralTab"] = "Основное",
        ["SettingsForm.DeveloperTab"] = "Разработчик",
        ["SettingsForm.EnableDMCB"] = "Включить режим разработчика",
        ["SettingsForm.EnableWSCB"] = "Включить веб-сервер",
        ["SettingsForm.PortLabel"] = "Порт",
        ["SettingsForm.ERRPCB"] = "Пароль для каждого запроса",
        ["SettingsForm.AllowGCCB"] = "Разрешить метод GetCookie",
        ["SettingsForm.AllowGACB"] = "Разрешить метод GetAccounts",
        ["SettingsForm.AllowLACB"] = "Разрешить метод LaunchAccount",
        ["SettingsForm.AllowAECB"] = "Разрешить изменение аккаунтов",
        ["SettingsForm.DisableImagesCB"] = "Отключить загрузку картинок [меньше RAM]",
        ["SettingsForm.AllowExternalConnectionsCB"] = "Разрешить внешние подключения",
        ["SettingsForm.MiscellaneousTab"] = "Разное",
        ["SettingsForm.PresenceCB"] = "Показывать presence аккаунтов",
        ["SettingsForm.PresenceUpdateLabel"] = "Обновление (мин)",
        ["SettingsForm.UnlockFPSCB"] = "Разблокировать FPS",
        ["SettingsForm.FPSCapLabel"] = "Макс. FPS",
        ["SettingsForm.OverrideWithCustomCB"] = "Использовать свой ClientAppSettings",
        ["SettingsForm.ForceUpdateButton"] = "Принудительно обновить",

        // Server list / utilities
        ["ServerList.ServerID"] = "JobId сервера",
        ["ServerList.Players"] = "Игроки",
        ["ServerList.Ping"] = "Пинг",
        ["ServerList.FPS"] = "FPS",
        ["ServerList.joinServerToolStripMenuItem"] = "Войти на сервер",
        ["ServerList.copyJobIdToolStripMenuItem"] = "Копировать JobId",
        ["ServerList.loadRegionToolStripMenuItem"] = "Загрузить регион",
        ["ServerList.joinGameToolStripMenuItem"] = "Запустить игру",
        ["ServerList.addToFavoritesToolStripMenuItem"] = "В избранное",
        ["ServerList.copyPlaceIdToolStripMenuItem1"] = "Копировать PlaceId",
        ["ServerList.ID"] = "PlaceID",
        ["ServerList.toolStripMenuItem1"] = "Запустить игру",
        ["ServerList.toolStripMenuItem2"] = "Переименовать",
        ["ServerList.removeToolStripMenuItem"] = "Удалить",
        ["ServerList.copyPlaceIDToolStripMenuItem"] = "Копировать PlaceID",
        ["ServerList.placeIdCol"] = "Place Id",
        ["ServerList.universeIdCol"] = "Universe Id",
        ["ServerList.ServerTypeColumn"] = "Тип",
        ["ServerList.Favorite"] = "Добавить текущую игру",
        ["ServerList.VerifyDataModelCB"] = "Проверка DataModel",
        ["ServerList.ServersTab"] = "Серверы",
        ["ServerList.JobId"] = "Job ID",
        ["ServerList.Playing"] = "Играют",
        ["ServerList.PingColumn"] = "Пинг",
        ["ServerList.RegionColumn"] = "Регион",
        ["ServerList.RefreshServers"] = "Обновить",
        ["ServerList.UsernameLabel"] = "Username",
        ["ServerList.SearchPlayer"] = "Найти",
        ["ServerList.GamesPage"] = "Игры",
        ["ServerList.ListViewCB"] = "Список",
        ["ServerList.label1"] = "Страница",
        ["ServerList.Search"] = "Поиск",
        ["ServerList.name"] = "Название игры",
        ["ServerList.playerCount"] = "Игроки",
        ["ServerList.likeRatio"] = "Лайки %",
        ["ServerList.FavoritesPage"] = "Избранное",
        ["ServerList.FavoriteListViewCB"] = "Список",
        ["ServerList.GameName"] = "Название игры",
        ["ServerList.UniversePage"] = "Universe",
        ["ServerList.ViewUniverse"] = "Показать игры",
        ["ServerList.uUniverseIdLabel"] = "Universe ID",
        ["ServerList.uPlaceIDLabel"] = "Place ID",
        ["ServerList.GetUniverseID"] = "Получить Universe ID",
        ["ServerList.OutfitsPage"] = "Outfits",
        ["ServerList.WearCustomButton"] = "Надеть свой",
        ["ServerList.OutfitUsernameLabel"] = "Username",
        ["ServerList.ViewOutfits"] = "Показать outfits",
        ["ServerList.RobloxScan"] = "Watcher",
        ["ServerList.RobloxScannerCB"] = "Включить Roblox Watcher",
        ["ServerList.ScanESLabel"] = "Интервал скана (сек)",
        ["ServerList.label2"] = "Интервал чтения (мс)",
        ["ServerList.ExitIfBetaDetectedCB"] = "Закрывать Beta Home Menu",
        ["ServerList.ExitIfNoConnectionCB"] = "Закрывать, если нет подключения",
        ["ServerList.ConnectionSecondsLabel"] = "секунд",
        ["ServerList.IgnoreExistingProcesses"] = "Игнорировать процессы при старте",
        ["ServerList.RbxMemoryCB"] = "Закрывать Roblox, если RAM меньше",
        ["ServerList.MBLabel"] = "МБ",
        ["ServerList.CloseRbxWindowTitleCB"] = "Закрывать, если заголовок окна не",
        ["ServerList.SaveWindowPositionsCB"] = "Сохранять позиции окон",

        // Account Control / Nexus
        ["AccountControl.AutoRelaunchCheckBox"] = "Автоперезапуск",
        ["AccountControl.label1"] = "Place ID",
        ["AccountControl.label2"] = "Job ID",
        ["AccountControl.label3"] = "Команда",
        ["AccountControl.SendCommand"] = "Отправить",
        ["AccountControl.ExecutionPage"] = "Executor",
        ["AccountControl.ClearButton"] = "Очистить",
        ["AccountControl.ExecuteButton"] = "Выполнить",
        ["AccountControl.AutoexecPage"] = "Автовыполнение",
        ["AccountControl.ClearAutoExecScript"] = "Очистить",
        ["AccountControl.ClearOutputButton"] = "Очистить",
        ["AccountControl.SaveOutputToFileCheck"] = "Сохранять вывод в файл",
        ["AccountControl.ScriptBox"] = "print(\"Привет!\")\r\n\r\n-- ЭТО НЕ ВСТРОЕННЫЙ EXECUTOR.\r\n-- Это только прокси через Nexus.\r\n-- Нужно подключить свой executor\r\n-- к Nexus (см. вкладку Помощь).",
        ["AccountControl.label7"] = "Разница между отмеченными и выбранными аккаунтами:\r\nОтмеченные аккаунты получают команды/скрипты.\r\nВыбранные аккаунты используются для применения настроек.",
        ["AccountControl.ControlPage"] = "Панель управления",
        ["AccountControl.cUsername"] = "Username",
        ["AccountControl.cJobId"] = "Job ID",
        ["AccountControl.copyJobIdToolStripMenuItem"] = "Копировать JobId",
        ["AccountControl.removeToolStripMenuItem"] = "Удалить",
        ["AccountControl.SettingsTab"] = "Настройки",
        ["AccountControl.StartOnLaunch"] = "Запускать Nexus вместе с менеджером",
        ["AccountControl.AllowExternalConnectionsCB"] = "Разрешить внешние подключения",
        ["AccountControl.InternetCheckCB"] = "Проверять интернет перед запуском",
        ["AccountControl.UsePresenceCB"] = "Использовать Presence API",
        ["AccountControl.RLLabel"] = "Задержка перезапуска на аккаунт (сек)",
        ["AccountControl.LDLabel"] = "Задержка запуска (сек)",
        ["AccountControl.PortLabel"] = "Порт",
        ["AccountControl.MinimizeRoblox"] = "Свернуть Roblox",
        ["AccountControl.AutoMinimizeCB"] = "Автосворачивание",
        ["AccountControl.label8"] = "Интервал (сек)",
        ["AccountControl.CloseRoblox"] = "Закрыть Roblox",
        ["AccountControl.AutoCloseType"] = "На окно",
        ["AccountControl.ACLabel"] = "Интервал (мин)",
        ["AccountControl.MaxInstanceLabel"] = "Макс. окон",
        ["AccountControl.AutoCloseCB"] = "Автозакрытие",
        ["AccountControl.HelpPage"] = "Помощь",
        ["AccountControl.NexusDocsButton"] = "Документация",
        ["AccountControl.label6"] = "Нашёл баг? Сообщи в Discord или на GitHub",
        ["AccountControl.NexusDL"] = "Nexus.lua",

        // Other forms / controls
        ["ImportForm.label1"] = "Импорт аккаунтов по .ROBLOSECURITY, каждый с новой строки",
        ["ImportForm.ImportButton"] = "Импортировать",
        ["AccountFields.AccountName"] = "Поля аккаунта: ",
        ["ThemeEditor.SetBG"] = "Фон",
        ["ThemeEditor.SetFG"] = "Текст",
        ["ThemeEditor.SetBorder"] = "Цвет рамки",
        ["ThemeEditor.ChangeStyle"] = "Стиль кнопок",
        ["ThemeEditor.HideHeaders"] = "Скрыть заголовки",
        ["ThemeEditor.ToggleDarkTopBar"] = "Тёмная верхняя панель",
        ["ThemeEditor.ToggleTransparentBG"] = "Прозрачный фон label",
        ["ArgumentsForm.TeleportCB"] = "isTeleport",
        ["ArgumentsForm.OldJoin"] = "Старый метод входа",
        ["ArgumentsForm.button1"] = "Задать версию",
        ["AvatarControl.wearAvatarToolStripMenuItem"] = "Надеть аватар",
        ["AvatarControl.copyAvatarJSONToolStripMenuItem"] = "Копировать JSON аватара",
        ["GameControl.copyPlaceLinkToolStripMenuItem"] = "Копировать ссылку place",
        ["GameControl.copyPlaceIdToolStripMenuItem"] = "Копировать PlaceId",
        ["GameControl.copyNameToolStripMenuItem"] = "Копировать название",
        ["GameControl.copyPlaceDetailsToolStripMenuItem"] = "Копировать детали place",
        ["MissingAssetControl.BuyButton"] = "Купить",
        ["AutoUpdater.Status"] = "Ожидание...",
    };

    private static readonly Dictionary<string, string> TextByValue = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["Add Account"] = "Добавить аккаунт",
        ["Open Browser"] = "Открыть браузер",
        ["Join Server"] = "Запустить / войти",
        ["Utilities"] = "Инструменты",
        ["Settings"] = "Настройки",
        ["Account Control"] = "Управление аккаунтами",
        ["Account Utilities"] = "Утилиты аккаунта",
        ["Remove"] = "Удалить",
        ["Copy"] = "Копировать",
        ["Help"] = "Помощь",
        ["Info"] = "Информация",
        ["Username"] = "Username",
        ["Alias"] = "Псевдоним",
        ["Description"] = "Описание",
        ["Last Used"] = "Последнее использование",
        ["Refresh"] = "Обновить",
        ["Search"] = "Поиск",
        ["Favorite"] = "Избранное",
        ["Favorites"] = "Избранное",
        ["Games"] = "Игры",
        ["Servers"] = "Серверы",
        ["Watcher"] = "Watcher",
        ["General"] = "Основное",
        ["Developer"] = "Разработчик",
        ["Miscellaneous"] = "Разное",
        ["Clear"] = "Очистить",
        ["Execute"] = "Выполнить",
        ["Send"] = "Отправить",
        ["Documentation"] = "Документация",
        ["Waiting ..."] = "Ожидание...",
    };

    private static readonly Dictionary<string, string> ToolTips = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["AccountManager.Add"] = "Добавить Roblox-аккаунт в локальный список.",
        ["AccountManager.OpenBrowser"] = "Открыть встроенный браузер с выбранным аккаунтом.",
        ["AccountManager.JoinServer"] = "Запустить выбранные аккаунты в указанную игру/сервер.",
        ["AccountManager.ServerList"] = "Серверы, игры, избранное, universe, outfits и watcher.",
        ["AccountManager.LaunchNexus"] = "Открыть Account Control / Nexus для управления подключёнными клиентами.",
        ["AccountManager.copySecurityTokenToolStripMenuItem"] = "Осторожно: .ROBLOSECURITY даёт доступ к аккаунту. Никому не отправляй этот токен.",
        ["AccountManager.copyPasswordToolStripMenuItem"] = "Осторожно: копирует сохранённый пароль аккаунта.",
        ["AccountManager.copyUserPassComboToolStripMenuItem"] = "Осторожно: копирует связку username:password.",
        ["AccountManager.getAuthenticationTicketToolStripMenuItem"] = "Осторожно: authentication ticket можно использовать для входа/запуска сессии.",
        ["AccountManager.copyRbxplayerLinkToolStripMenuItem"] = "Осторожно: rbx-player ссылка может запускать Roblox от имени аккаунта.",
        ["SettingsForm.AllowGCCB"] = "Критично: endpoint GetCookie отдаёт cookie через локальный API.",
        ["SettingsForm.AllowExternalConnectionsCB"] = "Критично: разрешает подключения не только с localhost.",
        ["AccountControl.ExecuteButton"] = "Отправить Lua-код подключённым клиентам через Nexus.",
    };

    public static void Apply(Form form)
    {
        if (form == null) return;

        string formName = form.GetType().Name;

        if (FormTitles.TryGetValue(formName, out string title))
            form.Text = title;
        else if (!string.IsNullOrEmpty(form.Text))
            form.Text = form.Text.Replace("Roblox Account Manager", AppName).Replace("RBX Alt Manager", AppName);

        ApplyToControlTree(form, formName);
        ApplyPrivateFields(form, formName);
        ApplyToolTips(form, formName);
        AksoStyle.Apply(form);
    }

    public static void Apply(Control control)
    {
        if (control == null) return;

        string ownerName = control.FindForm()?.GetType().Name ?? control.GetType().Name;
        ApplyToControlTree(control, ownerName);
        ApplyPrivateFields(control, ownerName);
    }

    private static void ApplyToControlTree(Control root, string ownerName)
    {
        ApplyObject(root, ownerName, root.Name);

        if (root.ContextMenuStrip != null)
            ApplyToolStripItems(root.ContextMenuStrip.Items, ownerName);

        if (root is ToolStrip strip)
            ApplyToolStripItems(strip.Items, ownerName);

        if (root is TabControl tabControl)
            foreach (TabPage page in tabControl.TabPages)
                ApplyObject(page, ownerName, page.Name);

        if (root is ListView listView)
            foreach (ColumnHeader column in listView.Columns)
                ApplyObject(column, ownerName, column.Name);

        foreach (Control child in root.Controls)
            ApplyToControlTree(child, ownerName);
    }

    private static void ApplyPrivateFields(object owner, string ownerName)
    {
        foreach (FieldInfo field in owner.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            object value;

            try { value = field.GetValue(owner); }
            catch { continue; }

            if (value == null) continue;

            ApplyObject(value, ownerName, field.Name);

            if (value is ToolStrip toolStrip)
                ApplyToolStripItems(toolStrip.Items, ownerName);
            else if (value is ContextMenuStrip contextMenu)
                ApplyToolStripItems(contextMenu.Items, ownerName);
            else if (value is ToolStripDropDownItem dropDownItem)
                ApplyToolStripItems(dropDownItem.DropDownItems, ownerName);
            else if (value is TabControl tabControl)
                foreach (TabPage page in tabControl.TabPages)
                    ApplyObject(page, ownerName, page.Name);
            else if (value is ListView listView)
                foreach (ColumnHeader column in listView.Columns)
                    ApplyObject(column, ownerName, column.Name);
        }
    }

    private static void ApplyToolStripItems(ToolStripItemCollection items, string ownerName)
    {
        foreach (ToolStripItem item in items)
        {
            ApplyObject(item, ownerName, item.Name);

            if (item is ToolStripDropDownItem dropDown)
                ApplyToolStripItems(dropDown.DropDownItems, ownerName);
        }
    }

    private static void ApplyObject(object obj, string ownerName, string name)
    {
        if (obj == null || string.IsNullOrEmpty(name)) return;

        if (!TryGetText(ownerName, name, out string localized) &&
            !TryGetTextByCurrentValue(obj, out localized)) return;

        switch (obj)
        {
            case ToolStripItem item:
                item.Text = localized;

                if (TryGetToolTip(ownerName, name, out string itemToolTip))
                    item.ToolTipText = itemToolTip;
                break;

            case ColumnHeader column:
                column.Text = localized;
                break;

            case TabPage page:
                page.Text = localized;
                break;

            case ButtonBase buttonBase:
                buttonBase.Text = localized;
                break;

            case Label label:
                label.Text = localized;
                break;

            case GroupBox groupBox:
                groupBox.Text = localized;
                break;

            case Form form:
                form.Text = localized;
                break;

            case ComboBox comboBox:
                LocalizeComboBoxItems(ownerName, name, comboBox);
                comboBox.Text = localized;
                break;

            case TextBoxBase textBox when ShouldLocalizeTextBox(ownerName, name):
                textBox.Text = localized;
                break;

            case Control control when !(control is TextBoxBase):
                control.Text = localized;
                break;
        }
    }

    private static void ApplyToolTips(Form form, string ownerName)
    {
        List<ToolTip> toolTips = new List<ToolTip>();

        foreach (FieldInfo field in form.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            object value;

            try { value = field.GetValue(form); }
            catch { continue; }

            if (value is ToolTip toolTip)
                toolTips.Add(toolTip);
        }

        if (toolTips.Count == 0) return;

        ApplyToolTipsToControlTree(form, ownerName, toolTips);
    }

    private static void ApplyToolTipsToControlTree(Control root, string ownerName, List<ToolTip> toolTips)
    {
        if (TryGetToolTip(ownerName, root.Name, out string tip))
            foreach (ToolTip toolTip in toolTips)
                toolTip.SetToolTip(root, tip);

        foreach (Control child in root.Controls)
            ApplyToolTipsToControlTree(child, ownerName, toolTips);
    }

    private static void LocalizeComboBoxItems(string ownerName, string name, ComboBox comboBox)
    {
        if (string.Equals(ownerName + "." + name, "AccountControl.AutoCloseType", StringComparison.OrdinalIgnoreCase))
        {
            int selectedIndex = comboBox.SelectedIndex;

            if (comboBox.Items.Count >= 2)
            {
                comboBox.Items[0] = "На окно";
                comboBox.Items[1] = "Общее";
            }

            comboBox.SelectedIndex = selectedIndex;
        }
    }

    private static bool TryGetText(string ownerName, string name, out string localized)
    {
        return Text.TryGetValue(ownerName + "." + name, out localized) || Text.TryGetValue(name, out localized);
    }

    private static bool TryGetTextByCurrentValue(object obj, out string localized)
    {
        localized = null;
        string current = null;

        switch (obj)
        {
            case ToolStripItem item:
                current = item.Text;
                break;
            case ColumnHeader column:
                current = column.Text;
                break;
            case TabPage page:
                current = page.Text;
                break;
            case Control control when !(control is TextBoxBase):
                current = control.Text;
                break;
        }

        return !string.IsNullOrWhiteSpace(current) && TextByValue.TryGetValue(current.Trim(), out localized);
    }

    private static bool TryGetToolTip(string ownerName, string name, out string tip)
    {
        return ToolTips.TryGetValue(ownerName + "." + name, out tip) || ToolTips.TryGetValue(name, out tip);
    }

    private static bool ShouldLocalizeTextBox(string ownerName, string name)
    {
        // Most text boxes contain user data or IDs. Only localize explicit placeholder-like fields.
        return string.Equals(ownerName + "." + name, "AccountUtils.textBox3", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(ownerName + "." + name, "AccountUtils.textBox5", StringComparison.OrdinalIgnoreCase);
    }
}
