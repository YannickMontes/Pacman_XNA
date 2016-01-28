using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman_Game_XNA
{
    public class Cell
    {
        /// <summary>
        /// Attribute to know what is on the case.
        /// </summary>
        private CELL_CONTENT content;
        /// <summary>
        /// Line of the cell.
        /// </summary>
        private int line;
        /// <summary>
        /// Column of the cell.
        /// </summary>
        private int column;
        /// <summary>
        /// Constructor of a cell. Initialize all the attributes.
        /// </summary>
        /// <param name="line">Line of the cell</param>
        /// <param name="column">Column of the cell</param>
        /// <param name="content">What type of cell is it</param>
        public Cell(int line, int column, int content)
        {
            this.line = line;
            this.column = column;
            this.content = (CELL_CONTENT)content;
        }

        /** PROPERTIES **/
        public int Column
        {
            get { return column; }
        }

        public int Line
        {
            get { return line; }
            set { line = value; }
        }

        public CELL_CONTENT Content
        {
            get { return content; }
            set { content = value; }
        }
    }
}
