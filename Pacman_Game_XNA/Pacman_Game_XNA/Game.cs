using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pacman_Game_XNA
{
    /// <summary>
    /// Enumeration to control the direction.
    /// </summary>
    public enum DIRECTION
    {
        RIGHT,
        DOWN,
        LEFT,
        UP,
        NONE
    }

    /// <summary>
    /// The enum used to know what is on a cell.
    /// </summary>
    public enum CELL_CONTENT
    {
        WALL,//A wall, 0
        BEAN,//A bean, 1
        BIGBEAN,//A big-bean, 2
        PACGUM,//A pacgum, 3
        EMPTY//Nothing, 4
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static List<Ghost> GHOSTS;
        private Texture2D wall;
        private Texture2D bean;
        private Texture2D bigbean;
        private Texture2D pacgum;
        private Map map;
        private Pacman pacman;
        private Collision collision;
        private MovementController movementController;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 660;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            this.map = new Map(28, 31, new byte[,]{
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
                {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
                {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
                {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0},
                {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0},
                {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 2, 2, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
                {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
                {0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0},
                {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0},
                {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0},
                {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            });
            this.collision = new Collision(this.pacman, map);
            this.movementController = new MovementController();
            GHOSTS = new List<Ghost>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            wall = Content.Load<Texture2D>(@"Sprites\\Background\\wall");
            bean = Content.Load<Texture2D>(@"Sprites\\Background\\bean");
            bigbean = Content.Load<Texture2D>(@"Sprites\\Background\\big_bean");
            pacgum = Content.Load<Texture2D>(@"Sprites\\Background\\pacgum");

            pacman = new Pacman("Pacman", Content.Load<Texture2D>(@"Sprites\\Pacman\\pacman_RIGHT"));
            pacman.AddTexture(Content.Load<Texture2D>(@"Sprites\\Pacman\\pacman_DOWN"));
            pacman.AddTexture(Content.Load<Texture2D>(@"Sprites\\Pacman\\pacman_LEFT"));
            pacman.AddTexture(Content.Load<Texture2D>(@"Sprites\\Pacman\\pacman_UP"));
            pacman.AddTexture(Content.Load<Texture2D>(@"Sprites\\Pacman\\pacman_RIGHT_F"));
            pacman.AddTexture(Content.Load<Texture2D>(@"Sprites\\Pacman\\pacman_DOWN_F"));
            pacman.AddTexture(Content.Load<Texture2D>(@"Sprites\\Pacman\\pacman_LEFT_F"));
            pacman.AddTexture(Content.Load<Texture2D>(@"Sprites\\Pacman\\pacman_UP_F"));
            pacman.setPosition(15*20, 17*20);

            GHOSTS.Add(new Ghost("Ghost 1", Content.Load<Texture2D>(@"Sprites\\Ghosts\\ghost_sky"), 15 * 20, 17 * 20));
            //GHOSTS.Add(new Ghost("Ghost 2", Content.Load<Texture2D>(@"Sprites\\Ghosts\\ghost_red"), 14 * 20, 13 * 20));
            //GHOSTS.Add(new Ghost("Ghost 3", Content.Load<Texture2D>(@"Sprites\\Ghosts\\ghost_pink"), 13 * 20, 13 * 20));
            //GHOSTS.Add(new Ghost("Ghost 4", Content.Load<Texture2D>(@"Sprites\\Ghosts\\ghost_orange"), 13 * 20, 14 * 20));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            this.movementController.Update(this.pacman, this.map);
            this.pacman.Update(this.collision);
            foreach(Ghost g in GHOSTS)
            {
                g.Update(this.collision);
            }
            this.collision.Update(this.pacman);

            Pacman.NB_FRAMES_OPEN_MOUTH_PACMAN++;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            this.DisplayMap();
            this.DisplayPacman();
            this.DisplayGhosts();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Method used to display the map to the screen.
        /// Loop through the map, and check each cell content.
        /// </summary>
        private void DisplayMap()
        {
            for (int i = 0; i < this.map.Height; i++)
            {
                for(int j=0; j< this.map.Width; j++)
                {
                    switch(this.map.Grid[i][j].Content)
                    {
                        case CELL_CONTENT.BEAN:
                            spriteBatch.Draw(this.bean, new Vector2(j*this.map.Tile_size, i*this.map.Tile_size), Color.White);
                            break;
                        case CELL_CONTENT.BIGBEAN:
                            spriteBatch.Draw(this.bigbean, new Vector2(j * this.map.Tile_size, i * this.map.Tile_size), Color.White);
                            break;
                        case CELL_CONTENT.PACGUM:
                            spriteBatch.Draw(this.pacgum, new Vector2(j * this.map.Tile_size, i * this.map.Tile_size), Color.White);
                            break; 
                        case CELL_CONTENT.WALL:
                            spriteBatch.Draw(this.wall, new Vector2(j * this.map.Tile_size, i * this.map.Tile_size), Color.White);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Internal function used to display pacman.
        /// </summary>
        private void DisplayPacman()
        {
            spriteBatch.Draw(this.pacman.ActualTexture, this.pacman.Position, Color.White);
        }

        private void DisplayGhosts()
        {
            foreach (Ghost g in GHOSTS)
            {
                spriteBatch.Draw(g.ActualTexture, g.Position, Color.White);
            }
        }
    }
}
