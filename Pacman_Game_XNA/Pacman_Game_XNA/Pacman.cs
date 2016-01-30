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
        private Collision collision;

        public Pacman(string name, Texture2D texture, Map map) : base(name, texture)
        {
            this.collision = new Collision(this, map);
            this.movementController = new MovementController(this, map, this.collision);
        }

        public void Update()
        {
            this.movementController.Update();
            this.MooveObject(this.collision);
        }
    }
}
    