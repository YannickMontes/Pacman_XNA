using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman_Game_XNA
{
    public class Collision
    {
        private Map map;

        public Map Map
        {
            get { return map; }
        }

        public Collision(Pacman pacman, Map map)
        {
            this.map = map;
        }

        public List<DIRECTION> GetPossibleDirection(AnimateObject obj)
        {
            List<DIRECTION> ret = new List<DIRECTION>();

            if (this.SideCaseIsNotWall(DIRECTION.DOWN, obj))
            {
                ret.Add(DIRECTION.DOWN);
            }
            if (this.SideCaseIsNotWall(DIRECTION.UP, obj))
            {
                ret.Add(DIRECTION.UP);
            }
            if (this.SideCaseIsNotWall(DIRECTION.RIGHT, obj))
            {
                ret.Add(DIRECTION.RIGHT);
            }
            if (this.SideCaseIsNotWall(DIRECTION.LEFT, obj))
            {
                ret.Add(DIRECTION.LEFT);
            }

            return ret;
        }

        public bool SideCaseIsNotWall(DIRECTION dir, AnimateObject obj)
        {
            switch(dir)
            {
                case DIRECTION.DOWN:
                    if (obj.GetCaseYSup(map.Tile_size) != -1)
                    {
                        if (map.Grid[obj.GetCaseYSup(map.Tile_size)][obj.GetAcutalCaseX(map.Tile_size)].Content == CELL_CONTENT.WALL)
                        {
                            return false;
                        }
                    }
                    break;
                case DIRECTION.UP:
                    if (obj.GetCaseYInf(map.Tile_size) != -1)
                    {
                        if (map.Grid[obj.GetCaseYInf(map.Tile_size) - 1][obj.GetAcutalCaseX(map.Tile_size)].Content == CELL_CONTENT.WALL)
                        {
                            return false;
                        }
                    }
                    break;
                case DIRECTION.LEFT:
                    if (obj.GetCaseXInf(map.Tile_size) != -1)
                    {
                        if (map.Grid[obj.GetAcutalCaseY(map.Tile_size)][obj.GetCaseXInf(map.Tile_size) - 1].Content == CELL_CONTENT.WALL)
                        {
                            return false;
                        }
                    }
                    break;
                case DIRECTION.RIGHT:
                    if (obj.GetCaseXSup(map.Tile_size) != -1)
                    {
                        if (map.Grid[obj.GetAcutalCaseY(map.Tile_size)][obj.GetCaseXSup(map.Tile_size)].Content == CELL_CONTENT.WALL)
                        {
                            return false;
                        }
                    }
                    break; 
            }
            return true;
        }

        public void Update(Pacman pacman)
        {
            if(!Game.IN_GAME)
            {
                pacman.Replace();
            }
            this.UpdatePacman(pacman);
            this.CheckCollisionPacmanGhosts(pacman);
        }

        private bool CheckCollisionPacmanGhosts(Pacman pacman)
        {
            foreach(Ghost g in Game.GHOSTS)
            {
                if(g.GetAcutalCaseX(map.Tile_size) == pacman.GetAcutalCaseX(map.Tile_size) && g.GetAcutalCaseY(map.Tile_size) == pacman.GetAcutalCaseY(map.Tile_size))
                {
                    if(g.Enable)
                    {
                        Pacman.NB_LIVES--;
                        Game.IN_GAME = false;
                        Game.ReplaceElements();
                        pacman.Replace();
                    }
                    else
                    {
                        g.GoToBase();
                        pacman.Score += 200;
                    }               
                }
            }
            return false;
        }

        private void UpdatePacman(Pacman pacman)
        {
            switch (this.map.Grid[pacman.GetAcutalCaseY(map.Tile_size)][pacman.GetAcutalCaseX(map.Tile_size)].Content)
            {
                case CELL_CONTENT.BEAN:
                    pacman.Score += 10;
                    map.Grid[pacman.GetAcutalCaseY(map.Tile_size)][pacman.GetAcutalCaseX(map.Tile_size)].Content = CELL_CONTENT.EMPTY;
                    break;
                case CELL_CONTENT.BIGBEAN:
                    pacman.Score += 20;
                    map.Grid[pacman.GetAcutalCaseY(map.Tile_size)][pacman.GetAcutalCaseX(map.Tile_size)].Content = CELL_CONTENT.EMPTY;
                    break;
                case CELL_CONTENT.PACGUM:
                    pacman.Score += 50;
                    foreach(Ghost g in Game.GHOSTS)
                    {
                        g.Enable = false;
                    }
                    map.Grid[pacman.GetAcutalCaseY(map.Tile_size)][pacman.GetAcutalCaseX(map.Tile_size)].Content = CELL_CONTENT.EMPTY;
                    break;
            }
        }
    }
}
