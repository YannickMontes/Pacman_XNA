using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman_Game_XNA
{
    public class MovementController
    {
        private KeyboardState keyboard;
        private Pacman pacman;
        private Map map;
        private Collision collision;

        public MovementController(Pacman pacman, Map map, Collision collison)
        {
            this.pacman = pacman;
            this.map = map;
            this.collision = collison;
        }

        public void Update()
        {
            this.keyboard = Keyboard.GetState();
            if(this.pacman.Direction == DIRECTION.UP || this.pacman.Direction == DIRECTION.DOWN || this.pacman.Direction == DIRECTION.NONE)
            {
                if(keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.Down))
                {
                    this.pacman.Direction = (keyboard.IsKeyDown(Keys.Up) ? DIRECTION.UP : DIRECTION.DOWN);
                }
                else if(keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.Left))
                {
                    if(this.pacman.GetCaseYSup(map.Tile_size)!=-1 || this.pacman.GetCaseYInf(map.Tile_size)!=-1)
                    {
                        this.pacman.Direction = (keyboard.IsKeyDown(Keys.Right) ? DIRECTION.RIGHT : DIRECTION.LEFT);
                    }
                }
            }
            if (this.pacman.Direction == DIRECTION.RIGHT || this.pacman.Direction == DIRECTION.LEFT || this.pacman.Direction == DIRECTION.NONE)
            {
                if(keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.Left))
                {
                    this.pacman.Direction = (keyboard.IsKeyDown(Keys.Right) ? DIRECTION.RIGHT : DIRECTION.LEFT);
                }
                else if(keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.Down))
                {
                    if(this.pacman.GetCaseXSup(map.Tile_size)!=-1 || this.pacman.GetCaseXInf(map.Tile_size)!=-1)
                    {
                        this.pacman.Direction = (keyboard.IsKeyDown(Keys.Up) ? DIRECTION.UP : DIRECTION.DOWN);
                    }
                }
            }
        }

        
    }
}
