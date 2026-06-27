
function getSelectedAccounts() {
  return state.accounts.filter(a => state.selectedUserIds.has(Number(a.userId)) || a.selected);
}

function openUtilitiesModal() {
  const selected = getSelectedAccounts();
  const modal = document.getElementById("utilitiesModal");

  if (!modal) return;

  const name = selected.length
    ? `${selected[0].username || "аккаунт"}${selected.length > 1 ? ` + ещё ${selected.length - 1}` : ""}`
    : "Аккаунт не выбран";

  document.getElementById("utilitiesAccountName").textContent = name;
  modal.classList.remove("hidden");
}

function closeUtilitiesModal() {
  document.getElementById("utilitiesModal")?.classList.add("hidden");
}

function sendUtility(action) {
  const selected = getSelectedAccounts();

  if (!selected.length) {
    toast("Выберите аккаунт");
    return;
  }

  const payload = {
    userIds: selected.map(x => Number(x.userId)),
    privacy: Number(document.getElementById("utilFollowPrivacy")?.value || 0),
    pin: document.getElementById("utilPin")?.value || "",
    displayName: document.getElementById("utilDisplayName")?.value || "",
    targetUsername: document.getElementById("utilTargetUsername")?.value || "",
    currentPassword: document.getElementById("utilCurrentPassword")?.value || "",
    newPassword: document.getElementById("utilNewPassword")?.value || "",
    emailPassword: document.getElementById("utilEmailPassword")?.value || "",
    newEmail: document.getElementById("utilNewEmail")?.value || ""
  };

  send(action, payload);
}
const state = {
  accounts: [],
  selectedUserIds: new Set(),
  settings: {},
  servers: []
};

const pages = {
  home: ["Главная", "Обзор аккаунтов, запуска и настроек"],
  accounts: ["Аккаунты", "Поиск, выбор и управление аккаунтами"],
  launch: ["Запуск", "Place ID, Job ID и запуск Roblox"],
  more: ["Прочее", "Игры, настройки, безопасность, backup и тема"]
};

const settingsSchema = [
  {
    title: "Основное",
    items: [
      ["General", "EnableMultiRbx", "bool", "Multi Roblox", "Позволяет запускать несколько Roblox-клиентов. Требует закрыть Roblox и перезапустить менеджер."],
      ["General", "AsyncJoin", "bool", "Асинхронный запуск", "Запускает аккаунты параллельно."],
      ["General", "AccountJoinDelay", "number", "Задержка запуска", "Задержка между аккаунтами в секундах."],
      ["General", "SavePasswords", "bool", "Сохранять пароли", "Локальное сохранение паролей."],
      ["General", "AutoCookieRefresh", "bool", "Автообновление cookies", "Периодическое обновление cookies."],
      ["General", "ShowPresence", "bool", "Показывать presence", "Отображение статуса аккаунтов."]
    ]
  },
  {
    title: "Запуск и серверы",
    items: [
      ["General", "ShuffleJobId", "bool", "Shuffle JobId", "Случайный сервер при запуске."],
      ["General", "ShuffleChoosesLowestServer", "bool", "Shuffle выбирает малый сервер", "Выбирает сервер с меньшим количеством игроков."],
      ["General", "ShufflePageCount", "number", "Страниц для shuffle", "Сколько страниц серверов проверять."],
      ["General", "MaxRecentGames", "number", "Недавних игр", "Максимум недавних игр."],
      ["General", "ServerRegionFormat", "text", "Формат региона", "Формат отображения региона сервера."]
    ]
  },
  {
    title: "FPS / Roblox",
    items: [
      ["General", "UnlockFPS", "bool", "Unlock FPS", "Патчит ClientAppSettings."],
      ["General", "MaxFPSValue", "number", "Лимит FPS", "Значение FPS."],
      ["General", "AutoCloseLastProcess", "bool", "Закрывать старый процесс", "Предотвращает дубли аккаунта."],
      ["General", "DisableAgingAlert", "bool", "Отключить aging alert", "Убирает индикатор давно неиспользованных аккаунтов."],
      ["General", "HideRbxAlert", "bool", "Скрыть Multi Roblox предупреждение", "Не показывать предупреждение при запущенном Roblox."]
    ]
  },
  {
    title: "WebServer",
    items: [
      ["Developer", "DevMode", "bool", "Developer Mode", "Показывает расширенные функции."],
      ["Developer", "EnableWebServer", "bool", "Включить WebServer", "Локальный HTTP API."],
      ["WebServer", "WebServerPort", "number", "Порт WebServer", "Порт локального API."],
      ["WebServer", "EveryRequestRequiresPassword", "bool", "Пароль на каждый запрос", "Повышает безопасность API."],
      ["WebServer", "Password", "password", "Пароль WebServer", "Не показывай другим."],
      ["WebServer", "AllowGetAccounts", "bool", "Allow GetAccounts", "Разрешить список аккаунтов."],
      ["WebServer", "AllowLaunchAccount", "bool", "Allow LaunchAccount", "Разрешить запуск через API."],
      ["WebServer", "AllowAccountEditing", "bool", "Allow Account Editing", "Разрешить изменения через API."],
      ["WebServer", "AllowGetCookie", "bool-danger", "Allow GetCookie", "Опасно: API сможет отдавать cookies."],
      ["WebServer", "AllowExternalConnections", "bool-danger", "External Connections", "Опасно: разрешает подключения извне."]
    ]
  },
  {
    title: "Watcher",
    items: [
      ["Watcher", "Enabled", "bool", "Roblox Watcher", "Следит за процессами Roblox."],
      ["Watcher", "ScanInterval", "number", "Интервал скана", "Секунды."],
      ["Watcher", "ReadInterval", "number", "Интервал чтения", "Миллисекунды."],
      ["Watcher", "ExitOnBeta", "bool", "Закрывать Beta Menu", "Закрывает beta home menu."],
      ["Watcher", "ExitIfNoConnection", "bool", "Закрывать без подключения", "Закрывает Roblox без соединения."],
      ["Watcher", "NoConnectionTimeout", "number", "Таймаут без подключения", "Секунды."],
      ["Watcher", "IgnoreExistingProcesses", "bool", "Игнорировать старые процессы", "Не трогать процессы до запуска watcher."],
      ["Watcher", "SaveWindowPositions", "bool", "Сохранять позиции окон", "Запоминает расположение окон."],
      ["Watcher", "VerifyDataModel", "bool", "Verify DataModel", "Проверка по логам."],
      ["Watcher", "CloseRbxMemory", "bool", "Закрывать при низкой RAM", "Закрывает если память ниже порога."],
      ["Watcher", "MemoryLowValue", "number", "Порог RAM MB", "Минимальный порог памяти."],
      ["Watcher", "CloseRbxWindowTitle", "bool", "Проверять заголовок окна", "Закрывать при неправильном title."],
      ["Watcher", "ExpectedWindowTitle", "text", "Ожидаемый заголовок", "Например Roblox."]
    ]
  },
  {
    title: "Account Control",
    items: [
      ["AccountControl", "StartOnLaunch", "bool", "Старт при запуске", "Запускать Account Control вместе с менеджером."],
      ["AccountControl", "UsePresence", "bool", "Use Presence API", "Использовать Presence API."],
      ["AccountControl", "InternetCheck", "bool", "Проверять интернет", "Перед запуском аккаунтов."],
      ["AccountControl", "RelaunchDelay", "number", "Relaunch delay", "Секунды."],
      ["AccountControl", "LauncherDelayNumber", "number", "Launcher delay", "Секунды."],
      ["AccountControl", "NexusPort", "number", "Nexus port", "Порт WebSocket."],
      ["AccountControl", "AllowExternalConnections", "bool-danger", "External connections", "Опасно: внешние подключения."]
    ]
  }
];

let audio = null;

function sound(type = "tap") {
  try {
    if (localStorage.getItem("aksoSound") === "off") return;
    audio = audio || new (window.AudioContext || window.webkitAudioContext)();
    const osc = audio.createOscillator();
    const gain = audio.createGain();
    const freq = type === "nav" ? 520 : type === "danger" ? 180 : type === "launch" ? 760 : 620;
    osc.frequency.value = freq;
    osc.type = type === "danger" ? "sawtooth" : "triangle";
    gain.gain.setValueAtTime(0.0001, audio.currentTime);
    gain.gain.exponentialRampToValueAtTime(0.10, audio.currentTime + 0.01);
    gain.gain.exponentialRampToValueAtTime(0.0001, audio.currentTime + 0.06);
    osc.connect(gain);
    gain.connect(audio.destination);
    osc.start();
    osc.stop(audio.currentTime + 0.08);
  } catch {}
}

function send(action, data = {}) {
  if (action !== "ready" && action !== "refresh") sound(action.toLowerCase().includes("launch") ? "launch" : "tap");
  window.chrome?.webview?.postMessage({ action, data });
}

function toast(text) {
  const el = document.getElementById("toast");
  const msg = String(text || "");
  const isError = msg.toLowerCase().includes("error") || msg.toLowerCase().includes("ошибка") || msg.toLowerCase().includes("invalid") || msg.length > 120;

  el.textContent = msg;
  el.classList.toggle("error", isError);
  el.classList.add("show");

  clearTimeout(window.__aksoToastTimer);
  window.__aksoToastTimer = setTimeout(() => {
    el.classList.remove("show");
    el.classList.remove("error");
  }, isError ? 12000 : 4000);
}

function showPage(id) {
  sound("nav");
  document.querySelectorAll(".page").forEach(p => p.classList.toggle("active", p.id === id));
  document.querySelectorAll(".nav-btn").forEach(b => b.classList.toggle("active", b.dataset.page === id));
  document.getElementById("pageTitle").textContent = pages[id][0];
  document.getElementById("pageSub").textContent = pages[id][1];
  localStorage.setItem("aksoPage", id);
}

function showMoreTab(id) {
  document.querySelectorAll(".more-panel").forEach(p => p.classList.toggle("active", p.id === "more-" + id));
  document.querySelectorAll(".more-tab").forEach(b => b.classList.toggle("active", b.dataset.more === id));
  localStorage.setItem("aksoMoreTab", id);
}

function render() {
  document.getElementById("statAccounts").textContent = state.accounts.length;
  document.getElementById("statSelected").textContent = state.selectedUserIds.size;
  document.getElementById("statRisk").textContent = state.stats?.riskLevel || "низкий";
  document.getElementById("statMulti").textContent = state.settings["General.EnableMultiRbx"] ? "вкл" : "выкл";
  document.getElementById("sideMulti").textContent = state.settings["General.EnableMultiRbx"] ? "включён" : "выключен";

  for (const id of ["placeId", "dashPlace", "serverPlace"]) {
    const el = document.getElementById(id);
    if (el && !el.matches(":focus")) el.value = state.placeId || "";
  }

  for (const id of ["jobId", "dashJob"]) {
    const el = document.getElementById(id);
    if (el && !el.matches(":focus")) el.value = state.jobId || "";
  }

  renderAccounts();
  renderSettings();
}

function renderAccounts() {
  const box = document.getElementById("accountsList");
  const q = (document.getElementById("accountSearch")?.value || "").toLowerCase();
  const list = state.accounts.filter(a => [a.username, a.alias, a.description, a.group, String(a.userId)].join(" ").toLowerCase().includes(q));
  box.innerHTML = "";
  box.classList.toggle("empty", !list.length);
  if (!list.length) { box.textContent = "Аккаунтов пока нет"; return; }

  for (const a of list) {
    const id = Number(a.userId);
    const selected = state.selectedUserIds.has(id) || a.selected;
    const card = document.createElement("div");
    card.className = "account-card" + (selected ? " selected" : "");
    card.innerHTML = `<div class="account-name">${esc(a.username || "unknown")}</div>
      <div class="account-meta">${esc(a.alias || "Без псевдонима")} · ${esc(a.group || "Default")}</div>
      <div class="account-desc">${esc(a.description || "Нет описания")}</div>
      <div class="account-meta">UserId: ${a.userId || ""} · ${esc(a.lastUse || "не запускался")}</div>`;
    card.onclick = () => {
      if (state.selectedUserIds.has(id)) state.selectedUserIds.delete(id);
      else state.selectedUserIds.add(id);
      if (!state.selectedUserIds.size) state.selectedUserIds.add(id);
      send("selectAccounts", { userIds: [...state.selectedUserIds] });
      renderAccounts();
    };
    box.appendChild(card);
  }
}

function renderSettings() {
  const root = document.getElementById("settingsGrid");
  if (!root || root.dataset.ready) {
    for (const input of document.querySelectorAll("[data-setting-key]")) {
      const full = `${input.dataset.section}.${input.dataset.settingKey}`;
      if (!(full in state.settings)) continue;
      if (input.type === "checkbox") input.checked = !!state.settings[full];
      else if (!input.matches(":focus")) input.value = state.settings[full] ?? "";
    }
    return;
  }

  root.innerHTML = "";
  root.dataset.ready = "1";

  for (const group of settingsSchema) {
    const box = document.createElement("div");
    box.className = "setting-group";
    box.innerHTML = `<h2>${group.title}</h2>`;

    for (const [section, key, type, title, desc] of group.items) {
      const row = document.createElement("div");
      row.className = "setting" + (type.includes("danger") ? " danger-zone" : "");
      const full = `${section}.${key}`;
      const inputType = type.includes("bool") ? "checkbox" : type === "password" ? "password" : type === "number" ? "number" : "text";
      row.innerHTML = `<div><div class="setting-title">${title}</div><div class="setting-desc">${desc}</div></div>
        <input type="${inputType}" data-section="${section}" data-setting-key="${key}">`;
      box.appendChild(row);
    }

    root.appendChild(box);
  }

  renderSettings();
}

function renderServers(servers = []) {
  const box = document.getElementById("serversList");
  box.innerHTML = "";
  box.classList.toggle("empty", !servers.length);
  if (!servers.length) { box.textContent = "Серверы не найдены"; return; }

  for (const s of servers) {
    const card = document.createElement("div");
    card.className = "server-card";
    card.innerHTML = `<div class="server-name">${s.playing}/${s.maxPlayers} игроков</div>
      <div class="server-meta">Ping: ${s.ping ?? "-"} · FPS: ${s.fps ?? "-"}</div>
      <div class="server-meta">Job ID: ${esc(s.id)}</div>
      <button class="btn primary wide">Войти на сервер</button>`;
    card.querySelector("button").onclick = e => {
      e.stopPropagation();
      send("joinServer", { placeId: document.getElementById("serverPlace").value, jobId: s.id });
    };
    box.appendChild(card);
  }
}

function esc(s) {
  return String(s).replace(/[&<>'"]/g, m => ({ "&": "&amp;", "<": "&lt;", ">": "&gt;", "'": "&#039;", '"': "&quot;" }[m]));
}

function runAction(action) {
  if (!action) return;

  if (action === "utilities") {
    openUtilitiesModal();
    return;
  }

  if (action === "closeUtilities") {
    closeUtilitiesModal();
    return;
  }

  if (action.startsWith("util")) {
    sendUtility(action);
    return;
  }

  if (action === "selectAll") {
    state.selectedUserIds = new Set(state.accounts.map(a => Number(a.userId)));
    send("selectAccounts", { userIds: [...state.selectedUserIds] });
    renderAccounts();
    return;
  }

  if (action === "launch" || action === "launchDash") {
    send("selectAccounts", { userIds: [...state.selectedUserIds] });
    send("launch", {
      placeId: document.getElementById(action === "launch" ? "placeId" : "dashPlace").value,
      jobId: document.getElementById(action === "launch" ? "jobId" : "dashJob").value,
      userIds: [...state.selectedUserIds]
    });
    return;
  }

  if (action === "launchAll" || action === "launchAllDash") {
    send("launchAll", {
      placeId: document.getElementById(action === "launchAll" ? "placeId" : "dashPlace").value,
      jobId: document.getElementById(action === "launchAll" ? "jobId" : "dashJob").value,
      userIds: state.accounts.map(a => Number(a.userId))
    });
    return;
  }

  if (action === "loadServers") {
    send("loadServers", { placeId: document.getElementById("serverPlace").value });
    toast("Загружаю серверы");
    return;
  }

  if (action === "toggleSound") {
    localStorage.setItem("aksoSound", localStorage.getItem("aksoSound") === "off" ? "on" : "off");
    toast("Звук переключён");
    return;
  }

  send(action);
}

document.addEventListener("click", e => {
  const nav = e.target.closest("[data-page]");
  if (nav) { showPage(nav.dataset.page); return; }

  const shortcut = e.target.closest("[data-page-shortcut]");
  if (shortcut) {
    showPage(shortcut.dataset.pageShortcut);
    if (shortcut.dataset.moreTab) showMoreTab(shortcut.dataset.moreTab);
    return;
  }

  const more = e.target.closest("[data-more]");
  if (more) { showMoreTab(more.dataset.more); return; }

  const theme = e.target.closest(".theme[data-theme]");
  if (theme) {
    document.body.dataset.theme = theme.dataset.theme;
    localStorage.setItem("aksoTheme", theme.dataset.theme);
    
    toast("Тема применена");
    return;
  }

  const themeButton = e.target.closest(".theme[data-theme]");
  if (themeButton) {
    e.preventDefault();
    e.stopPropagation();
    document.body.dataset.theme = themeButton.dataset.theme;
    localStorage.setItem("aksoTheme", themeButton.dataset.theme);
toast("Тема применена");
    return;
  }

  // AKSO_THEME_CLICK_FIXED
  const action = e.target.closest("[data-action]");
  if (action) runAction(action.dataset.action);
});

document.addEventListener("change", e => {
  const input = e.target.closest("[data-setting-key]");
  if (!input) return;
  send("updateSetting", {
    section: input.dataset.section,
    key: input.dataset.settingKey,
    value: input.type === "checkbox" ? input.checked : input.value
  });
});

document.getElementById("accountSearch")?.addEventListener("input", renderAccounts);

window.chrome?.webview?.addEventListener("message", e => {
  const m = e.data;
  if (m.type === "state") {
    state.accounts = m.accounts || [];
    state.stats = m.stats || {};
    state.settings = m.settings || {};
    state.placeId = m.placeId || "";
    state.jobId = m.jobId || "";
    const selected = state.accounts.filter(a => a.selected).map(a => Number(a.userId));
    if (selected.length) state.selectedUserIds = new Set(selected);
    render();
  }
  if (m.type === "servers") renderServers(m.servers || []);
  if (m.type === "toast") toast(m.text);
});

document.body.dataset.theme = localStorage.getItem("aksoTheme") || "blue";
showPage(localStorage.getItem("aksoPage") || "home");
showMoreTab(localStorage.getItem("aksoMoreTab") || "settings");
send("ready");
let aksoSavedGames = [];

function postAkso(action, data = {}) {
  window.chrome?.webview?.postMessage({ action, data });
}

function injectSavedGamesUI() {
  const panel = document.querySelector("#more-servers .card.full") || document.querySelector("#servers .card.full") || document.querySelector("#more-servers");

  if (!panel || document.getElementById("savedGamesBox")) return;

  const box = document.createElement("div");
  box.id = "savedGamesBox";
  box.className = "saved-games-box";
  box.innerHTML = `
    <div class="saved-head">
      <div>
        <h2>Сохранённые игры / VIP</h2>
        <p class="muted">Сохраняй Place ID, Job ID, VIP-ссылки и описание.</p>
      </div>
      <button class="btn secondary" data-action="getSavedGames">Обновить сохранения</button>
    </div>

    <div class="saved-form">
      <input id="savedGameId" type="hidden">
      <input id="savedGameTitle" placeholder="Название / метка">
      <input id="savedGamePlace" placeholder="Place ID или ссылка на игру">
      <input id="savedGameJob" placeholder="Job ID, если нужен">
      <input id="savedGameVip" placeholder="VIP ссылка, если есть">
      <textarea id="savedGameDescription" placeholder="Описание / заметка"></textarea>
      <div class="row">
        <button class="btn primary" data-action="addSavedGame">Сохранить</button>
        <button class="btn secondary" data-action="clearSavedGameForm">Очистить</button>
      </div>
    </div>

    <div id="savedGamesList" class="saved-games-list empty">Пока ничего не сохранено</div>
  `;

  panel.appendChild(box);
}

function clearSavedGameForm() {
  for (const id of ["savedGameId", "savedGameTitle", "savedGamePlace", "savedGameJob", "savedGameVip", "savedGameDescription"]) {
    const el = document.getElementById(id);
    if (el) el.value = "";
  }
}

function getSavedGameForm() {
  return {
    id: document.getElementById("savedGameId")?.value || "",
    title: document.getElementById("savedGameTitle")?.value || "",
    placeId: document.getElementById("savedGamePlace")?.value || "",
    jobId: document.getElementById("savedGameJob")?.value || "",
    vipLink: document.getElementById("savedGameVip")?.value || "",
    description: document.getElementById("savedGameDescription")?.value || ""
  };
}

function setSavedGameForm(game) {
  document.getElementById("savedGameId").value = game.id || "";
  document.getElementById("savedGameTitle").value = game.title || "";
  document.getElementById("savedGamePlace").value = game.placeId || "";
  document.getElementById("savedGameJob").value = game.jobId || "";
  document.getElementById("savedGameVip").value = game.vipLink || "";
  document.getElementById("savedGameDescription").value = game.description || "";
}

function renderSavedGames() {
  injectSavedGamesUI();

  const list = document.getElementById("savedGamesList");
  if (!list) return;

  list.innerHTML = "";
  list.classList.toggle("empty", !aksoSavedGames.length);

  if (!aksoSavedGames.length) {
    list.textContent = "Пока ничего не сохранено";
    return;
  }

  for (const game of aksoSavedGames) {
    const card = document.createElement("div");
    card.className = "saved-game-card";

    card.innerHTML = `
      <div class="saved-title">${escapeHtml(game.title || "Без названия")}</div>
      <div class="saved-meta">Place ID: ${escapeHtml(game.placeId || "-")}</div>
      <div class="saved-meta">Job/VIP: ${escapeHtml(game.vipLink || game.jobId || "-")}</div>
      <div class="saved-desc">${escapeHtml(game.description || "Нет описания")}</div>
      <div class="saved-meta">Обновлено: ${escapeHtml(game.updatedAt || "")}</div>
      <div class="row saved-actions">
        <button class="btn primary" data-saved-launch="${game.id}">Запустить</button>
        <button class="btn secondary" data-saved-fill="${game.id}">Изменить</button>
        <button class="btn danger" data-saved-remove="${game.id}">Удалить</button>
      </div>
    `;

    list.appendChild(card);
  }
}

document.addEventListener("click", event => {
  const action = event.target.closest("[data-action]")?.dataset.action;

  if (action === "addSavedGame") {
    event.preventDefault();
    event.stopPropagation();
    postAkso("addSavedGame", getSavedGameForm());
    return;
  }

  if (action === "clearSavedGameForm") {
    event.preventDefault();
    event.stopPropagation();
    clearSavedGameForm();
    return;
  }

  if (action === "getSavedGames") {
    event.preventDefault();
    event.stopPropagation();
    postAkso("getSavedGames");
    return;
  }

  const launch = event.target.closest("[data-saved-launch]")?.dataset.savedLaunch;
  if (launch) {
    const game = aksoSavedGames.find(x => x.id === launch);
    if (game) {
      postAkso("launchSavedGame", {
        placeId: game.placeId,
        jobId: game.jobId,
        vipLink: game.vipLink,
        userIds: [...state.selectedUserIds]
      });
    }
    return;
  }

  const fill = event.target.closest("[data-saved-fill]")?.dataset.savedFill;
  if (fill) {
    const game = aksoSavedGames.find(x => x.id === fill);
    if (game) setSavedGameForm(game);
    return;
  }

  const remove = event.target.closest("[data-saved-remove]")?.dataset.savedRemove;
  if (remove) {
    postAkso("removeSavedGame", { id: remove });
    return;
  }
}, true);

window.chrome?.webview?.addEventListener("message", event => {
  const message = event.data;

  if (message.type === "savedGames") {
    aksoSavedGames = message.savedGames || [];
    renderSavedGames();
  }
});

setTimeout(() => {
  injectSavedGamesUI();
  postAkso("getSavedGames");
}, 700);
