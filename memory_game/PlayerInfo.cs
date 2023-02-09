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
    public partial class PlayerInfo : Form
    {
        private GameTable gameTableForm;
        public PlayerInfo()
        {
            InitializeComponent();
        }

        private void PlayerInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnPlayer1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";

            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBoxPlayer1.BackgroundImage = new Bitmap(open.FileName);
            }
        }

        private void btnPlayer2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";

            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBoxPlayer2.BackgroundImage = new Bitmap(open.FileName);
            }
        }

        private void btnStartGame_Click_1(object sender, EventArgs e)
        {
            if (textBoxPlayer1.Text != "" && textBoxPlayer2.Text != "" && pictureBoxPlayer1.BackgroundImage != null && pictureBoxPlayer2.BackgroundImage != null)
            {
                gameTableForm = new GameTable(pictureBoxPlayer1, pictureBoxPlayer2, textBoxPlayer1.Text, textBoxPlayer2.Text);
                this.Hide();
                gameTableForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Enter all player information");
            }

        }
    }
}
