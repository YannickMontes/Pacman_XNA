using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman_Game_XNA
{
    public class Pacman : AnimateObject
    {
        public static int NB_FRAMES_OPEN_MOUTH_PACMAN = 0;
        public static int LEVEL = 1;
        public static int NB_LIVES = 3;
        private int score = 0;

        public Pacman(string name, Texture2D texture) : base(name, texture)
        {

        }

        public void Update(Collision collision)
        {
            this.CheckActualTexture();
            this.MooveObject(collision);
        }

        public void CheckActualTexture()
        {
            if (this.direction != DIRECTION.NONE)
            {
                if (Pacman.NB_FRAMES_OPEN_MOUTH_PACMAN < 5)
                {
                    this.actualTexture = this.textures.ElementAt((int)this.direction + 4);
                }
                else
                {
                    this.actualTexture = this.textures.ElementAt((int)this.direction);
                }
            }
            if (Pacman.NB_FRAMES_OPEN_MOUTH_PACMAN >= 10)
            {
                Pacman.NB_FRAMES_OPEN_MOUTH_PACMAN = 0;
            }
            if (this.direction == DIRECTION.NONE && this.textures.IndexOf(this.actualTexture) > 3)
            {
                this.actualTexture = this.textures.ElementAt(this.textures.IndexOf(this.actualTexture) - 4);
            }
        }

        public void Replace()
        {
            this.SetPosition(15 * 20, 17 * 20);
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }
    }
}
    