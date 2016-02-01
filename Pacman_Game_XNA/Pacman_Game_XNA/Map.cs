using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman_Game_XNA
{
    public class Map
    {
        /// <summary>
        /// Attribute grid, contains all the cell of the map. 
        /// </summary>
        private Cell[][] grid;
        /// <summary>
        /// Width of the map (number of cell)
        /// </summary>
        private int width;
        /// <summary>
        /// Height of the map (number of cell)
        /// </summary>
        private int height;
        /// <summary>
        /// The size of each cell of the grid.
        /// </summary>
        private int tile_size = 20;

        /// <summary>
        /// Basic constructor. WARNING: This constructor will not initialize the grid.
        /// </summary>
        /// <param name="width">Width map (number of cell)</param>
        /// <param name="height">Height map (number of cell)</param>
        public Map(int width, int height)
        {
            this.width=width;
            this.height=height;
        }

        /// <summary>
        /// Basic constructor. WARNING: This constructor will not initialize the grid.
        /// </summary>
        /// <param name="width">Width map (number of cell)</param>
        /// <param name="height">Height map (number of cell)</param>
        /// <param name="map">The map you want.</param>
        public Map(int width, int height, byte[,] map)
        {
            this.width = width;
            this.height = height;
            this.BuildMap(map);
        }

        /// <summary>
        /// Internal function, used to build the map from a byte matrix
        /// </summary>
        /// <param name="map">Matrix which contains the map data.</param>
        private void BuildMap(byte[,] map)
        {
            this.grid = new Cell[this.height][];
            for(int i=0; i<this.height; i++)
            {
                this.grid[i] = new Cell[this.width];
                for(int j=0; j<this.width; j++)
                {
                    this.grid[i][j] = new Cell(i, j, map[i,j]);
                }
            }
        }

        public void Update()
        {
            if(this.GetNbBonus()==0)
            {
                Pacman.LEVEL++;
                Pacman.NB_LIVES++;
                this.FillMap();
            }
        }

        private int GetNbBonus()
        {
            int compteur = 0;
            
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    if(this.grid[i][j].Content != CELL_CONTENT.WALL && this.grid[i][j].Content != CELL_CONTENT.EMPTY)
                    {
                        compteur++;
                    }
                }
            }

            return compteur;
        }

        private void FillMap()
        {
            Random rd = new Random();
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    if ((i == 1 && j == 1) || (i == this.height-2 && j == 1) || (i == 1 && j == this.width-2) || (i == this.height-2 && j == this.width-2))
                    {
                        this.grid[i][j].Content = CELL_CONTENT.PACGUM;
                    }
                    else
                    {
                        int nb = rd.Next(100);
                        if(nb<80)
                        {
                            this.grid[i][j].Content = CELL_CONTENT.BEAN;
                        }
                        else
                        {
                            this.grid[i][j].Content = CELL_CONTENT.BIGBEAN;
                        }
                    }
                }
            }   
        }

        /** PROPERTIES **/
        public Cell[][] Grid
        {
            get { return grid; }
        }

        public int Tile_size
        {
            get { return tile_size; }
            set { tile_size = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }
    }
}
