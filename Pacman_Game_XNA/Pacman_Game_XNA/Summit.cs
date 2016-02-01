using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman_Game_XNA
{
    public class Summit
    {
        private int caseX;
        private int caseY;
        private int weight = 200000;
        private bool marqued;
        private Summit previous;

        public Summit(int x, int y)
        {
            this.caseX = x;
            this.caseY = y;
            this.marqued = false;
        }

        public bool Marqued
        {
            get { return marqued; }
            set { marqued = value; }
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public int CaseX
        {
            get { return caseX; }
            set { caseX = value; }
        }

        public int CaseY
        {
            get { return caseY; }
            set { caseY = value; }
        }

        public Summit Previous
        {
            get { return previous; }
            set { previous = value; }
        }
    }
}
