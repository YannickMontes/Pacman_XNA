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
        private MovementController movementController;
        private Collision collision;

        public Pacman(string name, Texture2D texture, Map map) : base(name, texture)
        {
            this.collision = new Collision(this, map);
            this.movementController = new MovementController(this, map, this.collision);
        }

        public void Update()
        {
            this.movementController.Update();
            this.collision.Upadte();
            this.CheckActualTexture();
            this.MooveObject(this.collision);
        }
    }
}
    