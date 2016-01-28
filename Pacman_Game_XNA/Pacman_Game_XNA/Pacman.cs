using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman_Game_XNA
{
    public class Pacman : AnimateObject
    {
        private MovementController movementController;

        public Pacman(string name, Texture2D texture, Map map) : base(name, texture)
        {
            this.movementController = new MovementController(this, map);
        }

        public void Update()
        {
            this.Direction = this.movementController.GetMovement();
            this.CheckActualTexture();
            this.movementController.Update();
        }
    }
}
    