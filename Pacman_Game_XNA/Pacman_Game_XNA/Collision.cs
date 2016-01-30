using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman_Game_XNA
{
    public class Collision
    {
        private Pacman pacman;
        private Map map;

        public Collision(Pacman pacman, Map map)
        {
            this.pacman = pacman;
            this.map = map;
        }

        public bool SideCaseIsNotWall(DIRECTION dir)
        {
            switch(dir)
            {
                case DIRECTION.DOWN:
                    if(pacman.GetCaseYSup(map.Tile_size)!=-1)
                    {
                        if (map.Grid[pacman.GetCaseYSup(map.Tile_size)][pacman.GetAcutalCaseX(map.Tile_size)].Content == CELL_CONTENT.WALL)
                        {
                            return false;
                        }
                    }
                    break;
                case DIRECTION.UP:
                    if (pacman.GetCaseYInf(map.Tile_size) != -1)
                    {
                        if (map.Grid[pacman.GetCaseYInf(map.Tile_size)-1][pacman.GetAcutalCaseX(map.Tile_size)].Content == CELL_CONTENT.WALL)
                        {
                            return false;
                        }
                    }
                    break;
                case DIRECTION.LEFT:
                    if (pacman.GetCaseXInf(map.Tile_size) != -1)
                    {
                        if (map.Grid[pacman.GetAcutalCaseY(map.Tile_size)][pacman.GetCaseXInf(map.Tile_size)-1].Content == CELL_CONTENT.WALL)
                        {
                            return false;
                        }
                    }
                    break;
                case DIRECTION.RIGHT:
                    if (pacman.GetCaseXSup(map.Tile_size) != -1)
                    {
                        if (map.Grid[pacman.GetAcutalCaseY(map.Tile_size)][pacman.GetCaseXSup(map.Tile_size)].Content == CELL_CONTENT.WALL)
                        {
                            return false;
                        }
                    }
                    break; 
            }
            return true;
        }

        public void Upadte()
        {
            switch (this.map.Grid[this.pacman.GetAcutalCaseY(map.Tile_size)][this.pacman.GetAcutalCaseX(map.Tile_size)].Content) 
            { 
                case CELL_CONTENT.BEAN:
                    this.map.Grid[this.pacman.GetAcutalCaseY(map.Tile_size)][this.pacman.GetAcutalCaseX(map.Tile_size)].Content = CELL_CONTENT.EMPTY;
                    break;
                case CELL_CONTENT.BIGBEAN:
                    this.map.Grid[this.pacman.GetAcutalCaseY(map.Tile_size)][this.pacman.GetAcutalCaseX(map.Tile_size)].Content = CELL_CONTENT.EMPTY;
                    break; 
                case CELL_CONTENT.PACGUM:
                    this.map.Grid[this.pacman.GetAcutalCaseY(map.Tile_size)][this.pacman.GetAcutalCaseX(map.Tile_size)].Content = CELL_CONTENT.EMPTY;
                    break;
            }
        }
    }
}
