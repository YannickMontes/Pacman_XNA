using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Pacman_Game_XNA
{
    public class Ghost : AnimateObject
    {
        public static int RANGE_ACTION = 10;
        private bool enable;
        private Stopwatch chrono;

        public bool Enable
        {
            get { return enable; }
            set {
                if (value==false)
                {
                    chrono.Start();
                }
                else
                {
                    chrono.Stop();
                }
                enable = value;
            }
        }

        public Ghost(string name, Texture2D texture, int x, int y) : base(name, texture)
        {
            this.enable = true;
            this.position.X = x;
            this.position.Y = y;
            this.chrono = new Stopwatch();
        }

        public void Update(Collision collision)
        {
            if((this.GetAcutalCaseX(collision.Map.Tile_size) > 10 && this.GetAcutalCaseX(collision.Map.Tile_size) < 17) 
                && (this.GetAcutalCaseY(collision.Map.Tile_size) > 11 && this.GetAcutalCaseY(collision.Map.Tile_size) < 16))
            {
                this.GetOutOriginCage(collision);
            }
            else
            {
                this.RandomMoove(collision);
            }
            this.MooveObject(collision);
            this.CheckActualTexture();
        }

        private void GetOutOriginCage(Collision collision)
        {
            if(this.GetAcutalCaseY(collision.Map.Tile_size)==11)
            {
                Random rd = new Random();
                int nb = rd.Next(2);
                if(nb==0)
                {
                    this.direction = DIRECTION.LEFT;
                }
                else
                {
                    this.direction = DIRECTION.RIGHT;
                }
            }
            else
            {
                this.direction = DIRECTION.UP;
            }
        }

        public void CheckActualTexture()
        {
            if(this.enable)
            {
                this.actualTexture = textures.ElementAt(0);
            }
            else
            {
                if(chrono.IsRunning)
                { 
                    if(chrono.ElapsedMilliseconds < 5000)
                    {
                        this.actualTexture = textures.ElementAt(1);
                    }
                    if(chrono.ElapsedMilliseconds > 5000 && chrono.ElapsedMilliseconds < 5500)
                    {
                        this.actualTexture = textures.ElementAt(0);
                    }
                    if (chrono.ElapsedMilliseconds > 5500 && chrono.ElapsedMilliseconds < 6000)
                    {
                        this.actualTexture = textures.ElementAt(1);
                    }
                    if (chrono.ElapsedMilliseconds > 6000 && chrono.ElapsedMilliseconds < 6500)
                    {
                        this.actualTexture = textures.ElementAt(0);
                    }
                    if (chrono.ElapsedMilliseconds > 6500 && chrono.ElapsedMilliseconds < 7000)
                    {
                        this.actualTexture = textures.ElementAt(1);
                    }
                    if (chrono.ElapsedMilliseconds > 7000 && chrono.ElapsedMilliseconds < 7100)
                    {
                        this.actualTexture = textures.ElementAt(0);
                    }
                    if (chrono.ElapsedMilliseconds > 7100 && chrono.ElapsedMilliseconds < 7200)
                    {
                        this.actualTexture = textures.ElementAt(1);
                    }
                    if (chrono.ElapsedMilliseconds > 7200 && chrono.ElapsedMilliseconds < 7300)
                    {
                        this.actualTexture = textures.ElementAt(0);
                    }
                    if (chrono.ElapsedMilliseconds > 7300 && chrono.ElapsedMilliseconds < 7400)
                    {
                        this.actualTexture = textures.ElementAt(1);
                    }
                    if (chrono.ElapsedMilliseconds > 7400 && chrono.ElapsedMilliseconds < 7500)
                    {
                        this.actualTexture = textures.ElementAt(0);
                    }
                    if (chrono.ElapsedMilliseconds > 7500 && chrono.ElapsedMilliseconds < 7600)
                    {
                        this.actualTexture = textures.ElementAt(1);
                    }
                    if (chrono.ElapsedMilliseconds > 7600 && chrono.ElapsedMilliseconds < 7700)
                    {
                        this.actualTexture = textures.ElementAt(0);
                    }
                    if (chrono.ElapsedMilliseconds > 7700 && chrono.ElapsedMilliseconds < 7800)
                    {
                        this.actualTexture = textures.ElementAt(1);
                    }
                    if (chrono.ElapsedMilliseconds > 7800 && chrono.ElapsedMilliseconds < 7900)
                    {
                        this.actualTexture = textures.ElementAt(0);
                    }
                    if (chrono.ElapsedMilliseconds > 7900 && chrono.ElapsedMilliseconds < 8000)
                    {
                        this.actualTexture = textures.ElementAt(1);
                    }
                    if(chrono.ElapsedMilliseconds > 8000)
                    {
                        this.Enable = true;
                    }
                }
            }
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

        public void GoToBase()
        {
            this.position.X = 13 * 20;
            this.position.Y = 13 * 20;
        }
    }
}

