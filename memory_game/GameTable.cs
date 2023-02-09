using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memory_game
{
    public partial class GameTable : Form
    {
        private Card[,] cards = new Card[4, 8];
        private PictureBox[,] pictureBoxesBackSide = new PictureBox[4, 8];
        private PictureBox[,] pictureBoxesFrontSide = new PictureBox[4, 8];
        private Card firstClickedCard = new Card();
        private Player player1 = new Player();
        private Player player2 = new Player();
        public GameTable()
        {
            InitializeComponent();
        }
        public GameTable(PictureBox pictureBoxPlayer1, PictureBox pictureBoxPlayer2, string name1, string name2)
        {
            InitializeComponent();

            this.pictureBoxPlayer1.BackgroundImage = pictureBoxPlayer1.BackgroundImage;
            this.pictureBoxPlayer2.BackgroundImage = pictureBoxPlayer2.BackgroundImage;
            labelPlayer1.Text = name1;
            labelPlayer2.Text = name2;
            player1 = new Player(labelPlayer1.Text, this.pictureBoxPlayer1, true);
            player2 = new Player(labelPlayer2.Text, this.pictureBoxPlayer2, false);

            InitializeCards();
        }

        private void GameTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        public void InitializeCards()
        {
            int xCardSize = 100;
            int yCardSize = 125;
            int yLocation = 90;
            int tagBackSide = 0;
            int tagFrontSide = 32;
            for (int i = 0; i < 4; i++)
            {
                int xLocation = 110;
                for (int j = 0; j < 8; j++)
                {
                    pictureBoxesBackSide[i, j] = new PictureBox();
                    pictureBoxesFrontSide[i, j] = new PictureBox();

                    Controls.Add(pictureBoxesBackSide[i, j]);
                    Controls.Add(pictureBoxesFrontSide[i, j]);

                    pictureBoxesBackSide[i, j].Size = new Size(xCardSize, yCardSize);
                    pictureBoxesBackSide[i, j].Location = new Point(xLocation, yLocation);

                    //pictureBoxesBackSide[i, j].BackgroundImage = Image.FromFile("D:\\FACULTATE\\`POO LAB\\Memory_game\\MemoryGame\\MemoryGame\\Images\\cardBack.jpg");
                    pictureBoxesBackSide[i, j].BackgroundImage = Image.FromFile("cardBack.jpg");
                    pictureBoxesBackSide[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    pictureBoxesBackSide[i, j].Tag = tagBackSide;
                    tagBackSide++;

                    pictureBoxesFrontSide[i, j].Size = new Size(xCardSize, yCardSize);
                    pictureBoxesFrontSide[i, j].Location = new Point(xLocation, yLocation);
                    pictureBoxesFrontSide[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    pictureBoxesFrontSide[i, j].Visible = false;
                    pictureBoxesFrontSide[i, j].Enabled = false;
                    pictureBoxesFrontSide[i, j].Tag = tagFrontSide;
                    tagFrontSide++;

                    cards[i, j] = new Card(pictureBoxesFrontSide[i, j], pictureBoxesBackSide[i, j], xLocation, yLocation);

                    //FLIPCARD
                    pictureBoxesBackSide[i, j].Click += new EventHandler(FlipCard_Click);
                    //pictureBoxesFrontSide[i, j].Click += new EventHandler(FlipCard_Click);

                    xLocation += xCardSize + 15;
                }
                yLocation += yCardSize + 15;
            }
            GetRandomImages(cards);
        }
        public void GetRandomImages(Card[,] cardsArray)
        {
            int[] imageNumber = new int[cardsArray.Length];
            for (int i = 0; i < cardsArray.Length; i++)
            {
                imageNumber[i] = (i % (cardsArray.Length / 2));
            }
            Random random = new Random();
            imageNumber = imageNumber.OrderBy(x => random.Next()).ToArray();

            int k = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    //cardsArray[i, j].frontImage.BackgroundImage = Image.FromFile("D:\\FACULTATE\\`POO LAB\\Memory_game\\MemoryGame\\MemoryGame\\Images\\" + imageNumber[k].ToString() + ".PNG");
                    cardsArray[i, j].frontImage.BackgroundImage = Image.FromFile(imageNumber[k].ToString() + ".PNG");
                    cardsArray[i, j].imageNumber = imageNumber[k];
                    k++;
                }
            }

        }

        public void FlipCard_Click(object sender, EventArgs e)
        {

            Card thisCard = FindClickedCard((PictureBox)sender);

            if (Card.GetClick() == 1)
            {
                firstClickedCard = thisCard;
                firstClickedCard.ShowCard();
                Card.SetClick(2);
            }
            else
            {
                if (firstClickedCard.imageNumber != thisCard.imageNumber)
                {
                    thisCard.ShowCard();
                    Thread.Sleep(1000);
                    thisCard.HideCard();
                    firstClickedCard.HideCard();
                    Card.SetClick(1);

                    if (player1.turn == true)
                    {
                        player1.turn = false;
                        player2.turn = true;
                    }
                    else
                    {
                        player2.turn = false;
                        player1.turn = true;
                    }
                    pictureBoxPlayer1.Refresh();
                    pictureBoxPlayer2.Refresh();
                }
                if (firstClickedCard.imageNumber == thisCard.imageNumber)
                {
                    thisCard.ShowCard();
                    Thread.Sleep(1000);
                    Card.SetClick(1);

                    if (player1.turn == true)
                    {
                        player1.score += 1;
                        labelPlayer1Score.Text = player1.score.ToString();
                    }
                    else
                    {
                        player2.score += 1;
                        labelPlayer2Score.Text = player2.score.ToString();
                    }
                    if (player1.score + player2.score == 16)
                    {
                        if (player1.score > player2.score)
                        {
                            MessageBox.Show(player1.name + " wins!");
                        }
                        else if (player1.score < player2.score)
                        {
                            MessageBox.Show(player2.name + " wins!");
                        }
                        else
                        {
                            MessageBox.Show("It's a draw!");
                        }
                        System.Windows.Forms.Application.Exit();
                    }


                }
            }
        }

        public Card FindClickedCard(PictureBox cardClicked)
        {
            foreach (Card card in cards)
            {
                if (card.backImage.Tag == cardClicked.Tag)
                {
                    return card;
                }
            }
            return null;
        }

        private void pictureBoxPlayer1_Paint(object sender, PaintEventArgs e)
        {
            if (player1.turn == true)
            {
                ControlPaint.DrawBorder(e.Graphics, pictureBoxPlayer1.ClientRectangle, Color.Green, 5, ButtonBorderStyle.Solid,
                                                                                       Color.Green, 5, ButtonBorderStyle.Solid,
                                                                                       Color.Green, 5, ButtonBorderStyle.Solid,
                                                                                       Color.Green, 5, ButtonBorderStyle.Solid);
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, pictureBoxPlayer1.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Solid,
                                                                                       Color.Red, 5, ButtonBorderStyle.Solid,
                                                                                       Color.Red, 5, ButtonBorderStyle.Solid,
                                                                                       Color.Red, 5, ButtonBorderStyle.Solid);
            }
        }

        private void pictureBoxPlayer2_Paint(object sender, PaintEventArgs e)
        {
            if (player2.turn == true)
            {
                ControlPaint.DrawBorder(e.Graphics, pictureBoxPlayer2.ClientRectangle, Color.Green, 5, ButtonBorderStyle.Solid,
                                                                                       Color.Green, 5, ButtonBorderStyle.Solid,
                                                                                       Color.Green, 5, ButtonBorderStyle.Solid,
                                                                                       Color.Green, 5, ButtonBorderStyle.Solid);
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, pictureBoxPlayer2.ClientRectangle, Color.Red, 5, ButtonBorderStyle.Solid,
                                                                                       Color.Red, 5, ButtonBorderStyle.Solid,
                                                                                       Color.Red, 5, ButtonBorderStyle.Solid,
                                                                                       Color.Red, 5, ButtonBorderStyle.Solid);
            }
        }

        private void GameTable_Load(object sender, EventArgs e)
        {

        }
    }
}
