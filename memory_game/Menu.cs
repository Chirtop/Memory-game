using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memory_game
{
    public partial class Menu : Form
    {
        private PlayerInfo playerInfoForm;

        public Menu()
        {
            InitializeComponent();
            playerInfoForm = new PlayerInfo();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.Hide();
            playerInfoForm.ShowDialog();
        }
    }
}
