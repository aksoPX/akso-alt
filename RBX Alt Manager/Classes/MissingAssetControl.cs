using Newtonsoft.Json.Linq;
using RBX_Alt_Manager.Forms;
using RestSharp;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RBX_Alt_Manager.Classes
{
    public partial class MissingAssetControl : UserControl
    {
        private long AssetId, Cost;
        private string AssetName;
        private JObject Asset;

        public MissingAssetControl(long AssetId)
        {
            InitializeComponent();
            RussianLocalization.Apply(this);

            this.AssetId = AssetId;

            AssetImage.SizeMode = PictureBoxSizeMode.Zoom;
            
            ParentChanged += MissingAssetControl_ParentChanged;
        }

        public MissingAssetControl(string Name, long Price, string Image)
        {
            InitializeComponent();
            RussianLocalization.Apply(this);

            AssetNameLabel.Text = Name;
            PriceLabel.Text = $"R${Price}";

            AssetImage.SizeMode = PictureBoxSizeMode.Zoom;
            AssetImage.Load(Image);

            ParentChanged += MissingAssetControl_ParentChanged;
        }

        private async void MissingAssetControl_Load(object sender, EventArgs e)
        {
            await Download();

            AssetNameLabel.Text = AssetName;
            AssetNameLabel.LinkColor = ThemeEditor.LabelForeground;
            AssetNameLabel.VisitedLinkColor = ThemeEditor.LabelForeground;
            AssetNameLabel.Links.Add(0, AssetName.Length, $"https://www.roblox.com/catalog/{AssetId}/-");
            AssetNameLabel.LinkClicked += (s, e) => Process.Start(e.Link.LinkData as string);
            
            PriceLabel.Text = Cost == 0 ? "Цена: бесплатно" : (Cost > 0 ? $"Цена: {Cost}" : "Цена: недоступна");

            if (Cost < 0)
            {
                BuyButton.Enabled = false;
                BuyButton.BackColor = ControlPaint.Dark(BuyButton.BackColor, 0.06f);
            }

            AssetImage.Load(await Batch.GetImage(AssetId, "Asset"));
        }

        private async Task Download()
        {
            int Attempts = 0;

            while (Attempts < 8)
            {
                Attempts++;

                var Request = new RestRequest($"v2/assets/{AssetId}/details");

                Request.AddCookie(".ROBLOSECURITY", AccountManager.SelectedAccount?.SecurityToken ?? AccountManager.LastValidAccount?.SecurityToken ?? "", "/", ".roblox.com");

                var Response = await AccountManager.EconClient.ExecuteAsync(Request);

                if (Response.IsSuccessful)
                {
                    Asset = JObject.Parse(Response.Content);

                    AssetName = Asset["Name"]?.Value<string>() ?? "Unknown";
                    Cost = Asset["IsForSale"].Value<bool>() ? Asset["PriceInRobux"]?.Value<long>() ?? -1 : -1;

                    break;
                }

                await Task.Delay(350 * Attempts);
            }
        }

        private void MissingAssetControl_ParentChanged(object sender, EventArgs e)
        {
            Width = Parent.Width - 26;
            Parent.SizeChanged += Parent_SizeChanged;
        }

        private async void BuyButton_Click(object sender, EventArgs e)
        {
            if (!Asset.ContainsKey("ProductId") || !Asset.ContainsKey("PriceInRobux") || !Asset.ContainsKey("Creator")) return;

            if (Parent?.Parent is MissingAssets MA && MA.account.GetCSRFToken(out string CSRF) && Utilities.YesNoPrompt($"Покупка {AssetName}", $"Точно купить {AssetName} за {PriceLabel.Text}?", "Это действие нельзя отменить.", false))
            {
                BuyButton.Enabled = false;
                BuyButton.BackColor = ControlPaint.Dark(BuyButton.BackColor, 0.06f);

                var Request = new RestRequest($"v1/purchases/products/{Asset["ProductId"].Value<long>()}", Method.Post);
                Request.AddJsonBody(new { expectedCurrency = 1, expectedPrice = Asset["PriceInRobux"].Value<long>(), expectedSellerId = Asset["Creator"]["Id"].Value<long>() });
                Request.AddHeader("X-CSRF-Token", CSRF);
                Request.AddCookie(".ROBLOSECURITY", MA.account.SecurityToken, "/", ".roblox.com");

                var Response = await AccountManager.EconClient.ExecuteAsync(Request);

                Program.Logger.Info($"Attempting to purchase {AssetId} (Product: {Asset["ProductId"]}): [{Response.StatusCode}] {Response.Content}");

                if (Response.IsSuccessful && Response.Content.TryParseJson(out JObject Rsp))
                {
                    if (Rsp["purchased"].Value<bool>())
                        MessageBox.Show($"Успешно куплено: {AssetName} за {PriceLabel.Text}", "Покупка предмета", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show($"Не удалось купить {AssetName}: {Rsp["errorMsg"].Value<string>()}", Rsp["title"].Value<string>(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show($"Не удалось купить {AssetName} [{Response.StatusCode}]\n\n{Response.Content}", "Покупка предмета", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Parent_SizeChanged(object sender, EventArgs e) => Width = Parent.Width - 26;
    }
}