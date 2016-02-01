using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman_Game_XNA
{
    public class AnimateObject
    {
        /// <summary>
        /// Sprite list of the object
        /// </summary>
        protected List<Texture2D> textures;
        /// <summary>
        /// Name of the object (we totally don't care about it, but anyway)
        /// </summary>
        protected string name;
        protected Texture2D actualTexture;
        protected Vector2 position;
        protected Vector2 size;
        protected int speed = 1;
        protected DIRECTION direction;
        protected MovementController movementController;

        /// <summary>
        /// Base constructor. Create the list of textures and add it to the list. Create the vector position and size.
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="texture">The first texture</param>
        public AnimateObject(string name, Texture2D texture)
        {
            this.name = name;
            this.size = new Vector2();
            this.position = new Vector2(0.0f, 0.0f);
            this.textures = new List<Texture2D>();
            this.textures.Add(texture);
            this.direction = DIRECTION.NONE;
            this.actualTexture = texture;
        }

        /// <summary>
        /// Used to moove the object. It makes it moove in the actual direction, with the actual speed.
        /// </summary>
        public void MooveObject(Collision c)
        {
            if (!c.SideCaseIsNotWall(this.direction, this))
            {
                this.direction = DIRECTION.NONE;
            }
            switch(this.direction)
            {
                case DIRECTION.DOWN:     
                    this.position.Y += this.speed;
                    break;
                case DIRECTION.UP:
                    this.position.Y -= this.speed;
                    break;
                case DIRECTION.LEFT:
                    this.position.X -= this.speed;
                    break;
                case DIRECTION.RIGHT:
                    this.position.X += this.speed;
                    break;
            }
        }

        public void SetPosition(int x, int y)
        {
            this.position.X = x;
            this.position.Y = y;
        }

        /// <summary>
        /// Used to add a texture to the list.
        /// </summary>
        /// <param name="texture"></param>
        public void AddTexture(Texture2D texture)
        {
            this.textures.Add(texture);
        }

        /** PROPERTIES AND GET/SET **/
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Vector2 Position
        {
            get { return this.position; }
        }

        public Texture2D ActualTexture
        {
            get { return this.actualTexture; }
        }

        public DIRECTION Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Texture2D GetTextureAtPosition(int i)
        {
            return this.textures.ElementAt(i);
        }

        public int GetXPos()
        {
            return (int)this.position.X;
        }

        public int GetYPos()
        {
            return (int)this.position.Y;
        }

        public int GetCaseXInf(int tile_size)
        {
            if(this.position.X % tile_size == 0)
            {
                return (int)this.position.X / tile_size;
            }
            return -1;
        }

        public int GetCaseXSup(int tile_size)
        {
            if ((this.position.X + tile_size) % tile_size == 0)
            {
                return (int)(this.position.X + tile_size) / tile_size;
            }
            return -1;
        }

        public int GetCaseYInf(int tile_size)
        {
            if (this.position.Y % tile_size == 0)
            {
                return (int)this.position.Y / tile_size;
            }
            return -1;
        }

        public int GetCaseYSup(int tile_size)
        {
            if ((this.position.Y + tile_size) % tile_size == 0)
            {
                return (int)(this.position.Y + tile_size) / tile_size;
            }
            return -1;
        }

        public int GetAcutalCaseX(int tile_size)
        {
            return (int)(this.position.X+(tile_size/2))/tile_size;
        }

        public int GetAcutalCaseY(int tile_size)
        {
            return (int)(this.position.Y+(tile_size/2))/tile_size;
        }
    }
}
