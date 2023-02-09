using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memory_game
{
    public class Player
    {
        private string _name;
        private bool _turn;
        private int _score;

        public string name { get => _name; set => _name = value; }
        public bool turn { get => _turn; set => _turn = value; }
        public int score { get => _score; set => _score = value; }


        public Player() { }
        public Player(string nameVal, PictureBox profilePictureVal, bool turnVal)
        {
            name = nameVal;
            turn = turnVal;
            score = 0;
        }
    }
}
