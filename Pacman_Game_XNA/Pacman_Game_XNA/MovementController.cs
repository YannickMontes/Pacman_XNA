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

        public MovementController()
        {
        }

        public void Update(Pacman pacman, Map map)
        {
            this.UpdatePacman(pacman, map);
            this.UpdateGhosts();
        }

        private void UpdateGhosts()
        {
            
        }

        private void UpdatePacman(Pacman pacman, Map map)
        {
            this.keyboard = Keyboard.GetState();
            if (pacman.Direction == DIRECTION.UP || pacman.Direction == DIRECTION.DOWN || pacman.Direction == DIRECTION.NONE)
            {
                if (keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.Down))
                {
                    pacman.Direction = (keyboard.IsKeyDown(Keys.Up) ? DIRECTION.UP : DIRECTION.DOWN);
                    if (!Game.IN_GAME)
                        Game.IN_GAME = true;
                }
                else if (keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.Left))
                {
                    if (pacman.GetCaseYSup(map.Tile_size) != -1 || pacman.GetCaseYInf(map.Tile_size) != -1)
                    {
                        pacman.Direction = (keyboard.IsKeyDown(Keys.Right) ? DIRECTION.RIGHT : DIRECTION.LEFT);
                        if (!Game.IN_GAME)
                            Game.IN_GAME = true;
                    }
                }
            }
            if (pacman.Direction == DIRECTION.RIGHT || pacman.Direction == DIRECTION.LEFT || pacman.Direction == DIRECTION.NONE)
            {
                if (keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.Left))
                {
                    pacman.Direction = (keyboard.IsKeyDown(Keys.Right) ? DIRECTION.RIGHT : DIRECTION.LEFT);
                    if (!Game.IN_GAME)
                        Game.IN_GAME = true;
                }
                else if (keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.Down))
                {
                    if (pacman.GetCaseXSup(map.Tile_size) != -1 || pacman.GetCaseXInf(map.Tile_size) != -1)
                    {
                        pacman.Direction = (keyboard.IsKeyDown(Keys.Up) ? DIRECTION.UP : DIRECTION.DOWN);
                        if (!Game.IN_GAME)
                            Game.IN_GAME = true;
                    }
                }
            }
        }

        
    }
}
