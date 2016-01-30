using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman_Game_XNA
{
    public class Ghost : AnimateObject
    {
        public static int RANGE_ACTION = 10;
        private bool enable;

        public Ghost(string name, Texture2D texture, int x, int y) : base(name, texture)
        {
            this.enable = true;
            this.position.X = x;
            this.position.Y = y;
        }

        public void Update(Collision collision)
        {
            this.RandomMoove(collision);
        }

        public void CheckActualTexture()
        {

        }

        private void Dijikstra()
        {

        }

        private void RandomMoove(Collision collision)
        {
            Random rd = new Random();
            DIRECTION tmp;
            List<DIRECTION> possibilites = collision.GetPossibleDirection(this);

            if(this.direction!=DIRECTION.NONE)
            {
                possibilites.Remove(this.direction);
                possibilites.Remove(this.OppositeDirection(this.direction));
            }

            if(possibilites.Count!=0)
            {
                tmp = possibilites.ElementAt(rd.Next(possibilites.Count));
                if (this.Direction == DIRECTION.UP || this.Direction == DIRECTION.DOWN || this.Direction == DIRECTION.NONE)
                {
                    if (tmp == DIRECTION.UP || tmp == DIRECTION.DOWN)
                    {
                        this.Direction = (tmp == DIRECTION.UP ? DIRECTION.UP : DIRECTION.DOWN);
                    }
                    else if (tmp == DIRECTION.RIGHT || tmp == DIRECTION.LEFT)
                    {
                        if (this.GetCaseYSup(collision.Map.Tile_size) != -1 || this.GetCaseYInf(collision.Map.Tile_size) != -1)
                        {
                            this.Direction = (tmp == DIRECTION.RIGHT ? DIRECTION.RIGHT : DIRECTION.LEFT);
                        }
                    }
                }
                if (this.Direction == DIRECTION.RIGHT || this.Direction == DIRECTION.LEFT || this.Direction == DIRECTION.NONE)
                {
                    if (tmp == DIRECTION.RIGHT || tmp == DIRECTION.LEFT)
                    {
                        this.Direction = (tmp == DIRECTION.RIGHT ? DIRECTION.RIGHT : DIRECTION.LEFT);
                    }
                    else if (tmp == DIRECTION.UP || tmp == DIRECTION.DOWN)
                    {
                        if (this.GetCaseXSup(collision.Map.Tile_size) != -1 || this.GetCaseXInf(collision.Map.Tile_size) != -1)
                        {
                            this.Direction = (tmp == DIRECTION.UP ? DIRECTION.UP : DIRECTION.DOWN);
                        }
                    }
                }
            }
            
            this.MooveObject(collision);
        }

        private DIRECTION OppositeDirection(DIRECTION dir)
        {
            if(dir == DIRECTION.LEFT || dir == DIRECTION.RIGHT)
            {
                return (dir == DIRECTION.RIGHT ? DIRECTION.LEFT : DIRECTION.RIGHT);
            }
            if (dir == DIRECTION.UP || dir == DIRECTION.DOWN)
            {
                return (dir == DIRECTION.UP ? DIRECTION.DOWN : DIRECTION.UP);
            }
            return DIRECTION.NONE;
        }
    }
}

