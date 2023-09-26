using DiscordRPC;
using YTMusicDesktop;
using DiscordRPC.Logging;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using YTMusicDesktop.Utility;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace YTMusicDesktop
{
    public partial class Home : Form
    {
        public DiscordRpcClient client;
        public bool discordRPCEnabled;
        public bool WebSettingsEnabled;
        public int discordRPCDateTime;
        public DateTime datetime;
        public Home()
        {
            InitializeComponent();
            KeyPreview = true;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            discordRPCEnabled = Properties.Settings.Default.DiscordRPCEnabled;
            WebSettingsEnabled = Properties.Settings.Default.WebSettingsEnabled;
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;

            if (!discordRPCEnabled) return;
            client = new DiscordRpcClient("1154751494072053791");

            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                Details = "Playing YouTube Music",
                State = "Nothing is playing.",
                Timestamps = new Timestamps()
                {
                    Start = DateTime.Now,
                },
                Assets = new Assets()
                {
                    LargeImageKey = "ytmusic",
                    LargeImageText = "YTMusicDesktop v1.0",
                }
            });
        }

        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            discordRPCEnabled = Properties.Settings.Default.DiscordRPCEnabled;
            WebSettingsEnabled = Properties.Settings.Default.WebSettingsEnabled;
        }

        private async void ytMusicView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            ytMusicView.CoreWebView2.Settings.IsPasswordAutosaveEnabled = false;
            ytMusicView.CoreWebView2.Settings.IsGeneralAutofillEnabled = false;
            ytMusicView.CoreWebView2.Settings.HiddenPdfToolbarItems = CoreWebView2PdfToolbarItems.None;
            ytMusicView.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;

            var syncTimer = new System.Timers.Timer(1000);

            if (WebSettingsEnabled == true)
            {
                menuStrip1.Visible = false;

                ytMusicView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

                await ytMusicView.ExecuteScriptAsync("document.querySelector(\".cast-button.style-scope.ytmusic-cast-button\").addEventListener(\"click\", () => { window.chrome.webview.postMessage(\"LoadSettings_YTMDWebview\") })");
            }

            syncTimer.Elapsed += SyncTimer_Elapsed;
            syncTimer.SynchronizingObject = this;
            syncTimer.AutoReset = true;
            syncTimer.Enabled = true;
        }

        private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            if (e.TryGetWebMessageAsString() == "LoadSettings_YTMDWebview")
            {
                Settings settings = new Settings();
                settings.ShowDialog();
            }
        }

        private void setDiscordActivity(PlayInfo info)
        {
            if (!discordRPCEnabled) return;

            try
            {
                if (discordRPCDateTime == 0)
                {
                    datetime = DateTime.Now;
                    discordRPCDateTime++;
                }
                info.Title = info.Title.Replace("\"", "");
                info.Author = info.Author.Replace("\"", "");
                client.SetPresence(new RichPresence()
                {
                    Details = info.Title,
                    State = info.Author,
                    Timestamps = new Timestamps()
                    {
                        Start = datetime,
                        StartUnixMilliseconds = (ulong?)new DateTimeOffset(datetime).ToUnixTimeMilliseconds()
                    },
                    Buttons = new DiscordRPC.Button[]
                    {
                            new DiscordRPC.Button() {Label = "Open in Browser", Url = info.URL.Replace("\"", "") }
                    },
                    Assets = new Assets()
                    {
                        LargeImageKey = info.Image.Replace("\"", ""),
                        LargeImageText = "YTMusicDesktop v1.0"
                    }
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void setTaskbarState(int progress, int end)
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
            TaskbarManager.Instance.SetProgressValue(progress, end);
        }

        private async void SyncTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var returnYTMusicTitle = await ytMusicView.ExecuteScriptAsync("s(); function s() { return document.querySelectorAll('.title.style-scope.ytmusic-player-bar')[0].textContent; }");
            var returnYTMusicAuthor = await ytMusicView.ExecuteScriptAsync("s(); function s() { return document.querySelectorAll('.byline.style-scope.ytmusic-player-bar.complex-string > .style-scope.yt-formatted-string')[0].textContent}");
            string returnYTMusicImage = await ytMusicView.ExecuteScriptAsync("s(); function s() { return document.querySelector('.thumbnail.ytmusic-player.no-transition').querySelector('.yt-img-shadow').src;}");
            string returnYTMusicURL = await ytMusicView.ExecuteScriptAsync("s(); function s() { return document.querySelector('.ytp-title-link.yt-uix-sessionlink').href;}");
            string returnYTMusicTime = await ytMusicView.ExecuteScriptAsync("s(); function s() { return document.querySelector('#progress-bar').getAttribute('aria-valuenow') }");
            string returnYTMusicEndTime = await ytMusicView.ExecuteScriptAsync("s(); function s() { return document.querySelector('#progress-bar').getAttribute('aria-valuemax') }");
            string returnYTPlaying = await ytMusicView.ExecuteScriptAsync("s(); function s() {return document.querySelector('video').paused; }");

            string SettingsOverwriteLine = "document.querySelector('path[d=\"M22,20h-8v-1h7V5H3v3H2V4h20V20z M2,17.32V20h2.73C4.73,18.52,3.51,17.32,2,17.32z M2,13.75v1.79c2.51,0,4.55,2,4.55,4.46 h1.82C8.36,16.55,5.52,13.75,2,13.75z M2,10.18v1.79c4.52,0,8.18,3.6,8.18,8.03H12C12,14.57,7.52,10.18,2,10.18z\"').setAttribute(\"d\", \"M12,9c1.65,0,3,1.35,3,3s-1.35,3-3,3s-3-1.35-3-3S10.35,9,12,9 M12,8c-2.21,0-4,1.79-4,4s1.79,4,4,4s4-1.79,4-4 S14.21,8,12,8L12,8z M13.22,3l0.55,2.2l0.13,0.51l0.5,0.18c0.61,0.23,1.19,0.56,1.72,0.98l0.4,0.32l0.5-0.14l2.17-0.62l1.22,2.11 l-1.63,1.59l-0.37,0.36l0.08,0.51c0.05,0.32,0.08,0.64,0.08,0.98s-0.03,0.66-0.08,0.98l-0.08,0.51l0.37,0.36l1.63,1.59l-1.22,2.11 l-2.17-0.62l-0.5-0.14l-0.4,0.32c-0.53,0.43-1.11,0.76-1.72,0.98l-0.5,0.18l-0.13,0.51L13.22,21h-2.44l-0.55-2.2l-0.13-0.51 l-0.5-0.18C9,17.88,8.42,17.55,7.88,17.12l-0.4-0.32l-0.5,0.14l-2.17,0.62L3.6,15.44l1.63-1.59l0.37-0.36l-0.08-0.51 C5.47,12.66,5.44,12.33,5.44,12s0.03-0.66,0.08-0.98l0.08-0.51l-0.37-0.36L3.6,8.56l1.22-2.11l2.17,0.62l0.5,0.14l0.4-0.32 C8.42,6.45,9,6.12,9.61,5.9l0.5-0.18l0.13-0.51L10.78,3H13.22 M14,2h-4L9.26,4.96c-0.73,0.27-1.4,0.66-2,1.14L4.34,5.27l-2,3.46 l2.19,2.13C4.47,11.23,4.44,11.61,4.44,12s0.03,0.77,0.09,1.14l-2.19,2.13l2,3.46l2.92-0.83c0.6,0.48,1.27,0.87,2,1.14L10,22h4 l0.74-2.96c0.73-0.27,1.4-0.66,2-1.14l2.92,0.83l2-3.46l-2.19-2.13c0.06-0.37,0.09-0.75,0.09-1.14s-0.03-0.77-0.09-1.14l2.19-2.13 l-2-3.46L16.74,6.1c-0.6-0.48-1.27-0.87-2-1.14L14,2L14,2z\")";

            await ytMusicView.ExecuteScriptAsync(SettingsOverwriteLine);

            var info = new PlayInfo();
            info.Title = returnYTMusicTitle;
            info.Author = returnYTMusicAuthor;
            if (info.Title == "" || info.Author == "null") return;

            info.Image = returnYTMusicImage;
            info.URL = returnYTMusicURL;
            info.Progress = Convert.ToInt32(returnYTMusicTime.Replace("\"", ""));
            info.EndTime = Convert.ToInt32(returnYTMusicEndTime.Replace("\"", ""));
            info.IsPlaying = Convert.ToBoolean(returnYTPlaying.Replace("\"", ""));

            if (info.IsPlaying == true)
            {
                client.SetPresence(new RichPresence()
                {
                    Details = info.Title,
                    State = info.Author,
                    Buttons = new DiscordRPC.Button[]
    {
                            new DiscordRPC.Button() {Label = "Open in Browser", Url = info.URL.Replace("\"", "") }
    },
                    Assets = new Assets()
                    {
                        LargeImageKey = info.Image.Replace("\"", ""),
                        LargeImageText = "YTMusicDesktop v1.0",
                        SmallImageKey = "https://cdn2.iconfinder.com/data/icons/simple-circular-icons-line/84/Vertical_Bars-512.png",
                        SmallImageText = "Paused"
                    }
                });
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Paused);
                return;
            }

            setDiscordActivity(info);
            setTaskbarState(info.Progress, info.EndTime);
        }

        private void editEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        /// <summary>
        /// 指定した文字列内の指定した文字列を別の文字列に置換する。
        /// </summary>
        /// <param name="input">置換する文字列のある文字列。</param>
        /// <param name="oldValue">検索文字列。</param>
        /// <param name="newValue">置換文字列。</param>
        /// <param name="count">置換する回数。負の数が指定されたときは、すべて置換する。</param>
        /// <param name="compInfo">文字列の検索に使用するCompareInfo。</param>
        /// <param name="compOptions">文字列の検索に使用するCompareOptions。</param>
        /// <returns>置換された結果の文字列。</returns>
        public static string StringReplace(
            string input, string oldValue, string newValue, int count,
            System.Globalization.CompareInfo compInfo,
            System.Globalization.CompareOptions compOptions)
        {
            if (input == null || input.Length == 0 ||
                oldValue == null || oldValue.Length == 0 ||
                count == 0)
            {
                return input;
            }

            if (compInfo == null)
            {
                compInfo = System.Globalization.CultureInfo.InvariantCulture.CompareInfo;
                compOptions = System.Globalization.CompareOptions.Ordinal;
            }

            int inputLen = input.Length;
            int oldValueLen = oldValue.Length;
            System.Text.StringBuilder buf = new System.Text.StringBuilder(inputLen);

            int currentPoint = 0;
            int foundPoint = -1;
            int currentCount = 0;

            do
            {
                //文字列を検索する
                foundPoint = compInfo.IndexOf(input, oldValue, currentPoint, compOptions);
                if (foundPoint < 0)
                {
                    buf.Append(input.Substring(currentPoint));
                    break;
                }

                //見つかった文字列を新しい文字列に換える
                buf.Append(input.Substring(currentPoint, foundPoint - currentPoint));
                buf.Append(newValue);

                //次の検索開始位置を取得
                currentPoint = foundPoint + oldValueLen;

                //指定回数置換したか調べる
                currentCount++;
                if (currentCount == count)
                {
                    buf.Append(input.Substring(currentPoint));
                    break;
                }
            }
            while (currentPoint < inputLen);

            return buf.ToString();
        }

        /// <summary>
        /// 指定した文字列内の指定した文字列を別の文字列に置換する。
        /// </summary>
        /// <param name="input">置換する文字列のある文字列。</param>
        /// <param name="oldValue">検索文字列。</param>
        /// <param name="newValue">置換文字列。</param>
        /// <param name="count">置換する回数。負の数が指定されたときは、すべて置換する。</param>
        /// <param name="ignoreCase">大文字と小文字を区別しない時はTrue。</param>
        /// <returns>置換された結果の文字列。</returns>
        public static string StringReplace(
            string input, string oldValue, string newValue, int count, bool ignoreCase)
        {
            if (ignoreCase)
            {
                return StringReplace(input, oldValue, newValue, count,
                    System.Globalization.CultureInfo.InvariantCulture.CompareInfo,
                    System.Globalization.CompareOptions.OrdinalIgnoreCase);
            }
            else
            {
                return StringReplace(input, oldValue, newValue, count,
                    System.Globalization.CultureInfo.InvariantCulture.CompareInfo,
                    System.Globalization.CompareOptions.Ordinal);
            }
        }

        /// <summary>
        /// 指定した文字列内の指定した文字列を別の文字列に置換する。
        /// </summary>
        /// <param name="input">置換する文字列のある文字列。</param>
        /// <param name="oldValue">検索文字列。</param>
        /// <param name="newValue">置換文字列。</param>
        /// <param name="count">置換する回数。負の数が指定されたときは、すべて置換する。</param>
        /// <returns>置換された結果の文字列。</returns>
        public static string StringReplace(
            string input, string oldValue, string newValue, int count)
        {
            return StringReplace(input, oldValue, newValue, count,
                System.Globalization.CultureInfo.InvariantCulture.CompareInfo,
                System.Globalization.CompareOptions.Ordinal);
        }

        private void Home_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyData);
            if (e.KeyData == (Keys.T | Keys.Shift | Keys.Control))
            {
                Settings settings = new Settings();
                settings.ShowDialog();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }
    }
}
