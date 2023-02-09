using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memory_game
{
    public class Card : Position
    {
        private PictureBox _backImage;
        private PictureBox _frontImage;
        private int _imageNumber;
        private static int _click;

        public PictureBox frontImage { get => _frontImage; set => _frontImage = value; }
        public PictureBox backImage { get => _backImage; set => _backImage = value; }
        public int imageNumber { get => _imageNumber; set => _imageNumber = value; }

        public Card(PictureBox frontImage, PictureBox backImage, int x, int y) : base(x, y)
        {
            this._frontImage = frontImage;
            this._backImage = backImage;
            _click = 1;
        }
        public Card() : base()
        {
        }

        public static int GetClick()
        {
            return _click;
        }

        public static void SetClick(int click)
        {
            _click = click;
        }

        public void ShowCard()
        {
            backImage.Hide();
            backImage.Enabled = false;
            frontImage.Show();
            frontImage.Enabled = true;
        }

        public void HideCard()
        {
            backImage.Show();
            backImage.Enabled = true;
            frontImage.Hide();
            frontImage.Enabled = false;
        }

    }
}
