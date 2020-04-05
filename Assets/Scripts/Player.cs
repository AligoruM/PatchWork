using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Player
    {
        public int numberOfButtons;

        public int numberOfButtonsOnField;

        public int numberOfEmptyCells;

        public int position;

        public bool finishOfGame;

        public Player()
        {
            this.numberOfButtons = 5;
            this.numberOfButtonsOnField = 0;
            this.position = 0;
            this.finishOfGame = false;
            this.numberOfEmptyCells = 81;
        }
    }
}
