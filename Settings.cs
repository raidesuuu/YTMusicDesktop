using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YTMusicDesktop
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            check_websettings.Checked = Properties.Settings.Default.WebSettingsEnabled;
            check_discordrpc.Checked = Properties.Settings.Default.DiscordRPCEnabled;
        }

        private void check_discordrpc_CheckStateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DiscordRPCEnabled = check_discordrpc.Checked;
            Properties.Settings.Default.Save();
        }

        private void check_websettings_CheckStateChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.WebSettingsEnabled = check_websettings.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
