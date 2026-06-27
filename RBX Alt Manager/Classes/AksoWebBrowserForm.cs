using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using RBX_Alt_Manager.Classes;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RBX_Alt_Manager.Classes
{
    public class AksoWebBrowserForm : Form
    {
        private readonly WebView2 WebView;
        private readonly Label StatusLabel;
        private readonly Button CheckButton;
        private readonly Button CloseButton;
        private readonly Timer CookieTimer;
        private readonly bool LoginMode;
        private readonly Account BrowserAccount;

        public static void Login()
        {
            AksoWebBrowserForm form = new AksoWebBrowserForm(true, null);
            form.Show();
        }

        public static void Open(Account account, string url = null)
        {
            if (account == null)
            {
                MessageBox.Show("Аккаунт не выбран.", RussianLocalization.AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AksoWebBrowserForm form = new AksoWebBrowserForm(false, account, url);
            form.Show();
        }

        private AksoWebBrowserForm(bool loginMode, Account account, string url = null)
        {
            LoginMode = loginMode;
            BrowserAccount = account;

            Text = loginMode ? "Добавление аккаунта — akso alt" : $"Браузер — {account?.Username}";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(980, 720);
            Size = new Size(1120, 780);
            BackColor = Color.FromArgb(8, 13, 25);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10f);
            ShowIcon = false;

            Panel top = new Panel
            {
                Dock = DockStyle.Top,
                Height = 54,
                BackColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(12)
            };

            StatusLabel = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(226, 232, 240),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = loginMode
                    ? "Войди в Roblox. После успешного входа аккаунт добавится автоматически."
                    : "Открываю браузер аккаунта..."
            };

            CheckButton = new Button
            {
                Text = "Проверить вход",
                Dock = DockStyle.Right,
                Width = 160,
                BackColor = Color.FromArgb(37, 99, 235),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            CheckButton.FlatAppearance.BorderSize = 0;
            CheckButton.Click += async (s, e) => await TryImportCookie();

            CloseButton = new Button
            {
                Text = "Закрыть",
                Dock = DockStyle.Right,
                Width = 110,
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            CloseButton.FlatAppearance.BorderSize = 0;
            CloseButton.Click += (s, e) => Close();

            top.Controls.Add(StatusLabel);
            top.Controls.Add(CheckButton);
            top.Controls.Add(CloseButton);

            WebView = new WebView2
            {
                Dock = DockStyle.Fill,
                DefaultBackgroundColor = Color.FromArgb(8, 13, 25)
            };

            Controls.Add(WebView);
            Controls.Add(top);

            CookieTimer = new Timer { Interval = 1500 };
            CookieTimer.Tick += async (s, e) => await TryImportCookie();

            Load += async (s, e) =>
            {
                await Initialize(url);
            };

            FormClosing += (s, e) =>
            {
                CookieTimer.Stop();
            };
        }

        private async Task Initialize(string url)
        {
            try
            {
                await WebView.EnsureCoreWebView2Async();

                WebView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = true;
                WebView.CoreWebView2.Settings.AreDevToolsEnabled = false;

                if (LoginMode)
                {
                    WebView.CoreWebView2.CookieManager.DeleteAllCookies();
                    WebView.CoreWebView2.Navigate("https://www.roblox.com/login");
                    CookieTimer.Start();
                }
                else
                {
                    WebView.CoreWebView2.CookieManager.DeleteAllCookies();

                    CoreWebView2Cookie cookie = WebView.CoreWebView2.CookieManager.CreateCookie(
                        ".ROBLOSECURITY",
                        BrowserAccount.SecurityToken,
                        ".roblox.com",
                        "/"
                    );

                    cookie.IsHttpOnly = true;
                    cookie.IsSecure = true;
                    cookie.Expires = DateTime.Now.AddYears(1);

                    WebView.CoreWebView2.CookieManager.AddOrUpdateCookie(cookie);
                    WebView.CoreWebView2.Navigate(string.IsNullOrWhiteSpace(url) ? "https://www.roblox.com/home" : url);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось открыть WebView2 браузер. Установи Microsoft Edge WebView2 Runtime.\n\n" + ex.Message,
                    RussianLocalization.AppName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private async Task TryImportCookie()
        {
            if (!LoginMode || WebView?.CoreWebView2 == null) return;

            try
            {
                var cookies = await WebView.CoreWebView2.CookieManager.GetCookiesAsync("https://www.roblox.com");
                var rsec = cookies.FirstOrDefault(x => x.Name == ".ROBLOSECURITY");

                if (rsec == null || string.IsNullOrWhiteSpace(rsec.Value))
                {
                    cookies = await WebView.CoreWebView2.CookieManager.GetCookiesAsync("https://roblox.com");
                    rsec = cookies.FirstOrDefault(x => x.Name == ".ROBLOSECURITY");
                }

                if (rsec == null || string.IsNullOrWhiteSpace(rsec.Value))
                {
                    StatusLabel.Text = "Ожидаю успешный вход...";
                    return;
                }

                CookieTimer.Stop();
                StatusLabel.Text = "Cookie получена, добавляю аккаунт...";

                Account added = AccountManager.AddAccount(rsec.Value);

                WebView.CoreWebView2.CookieManager.DeleteAllCookies();

                if (added != null)
                {
                    MessageBox.Show("Аккаунт добавлен: " + added.Username, RussianLocalization.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Cookie получена, но аккаунт не добавился. Возможно, сессия недействительна.", RussianLocalization.AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CookieTimer.Start();
                }
            }
            catch
            {
                StatusLabel.Text = "Проверяю вход...";
            }
        }
    }
}