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

namespace LD26_minimalism
{

    enum State{
        Start,
        Running,
        Paused,
        Over
    };

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font;
        Texture2D startScreen;
        Texture2D overScreen;

        State currentState;

        int frames = 0;
        int FPS;
        float time = 0.0f;

        int currentLevel;
        bool levelChanged;

        Player player;
        ItemHandler items;
        EnemyHandler enemies;
        CollisionHandler collisions;

        KeyboardState currentKeyboardState;
        KeyboardState oldKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            currentLevel = 1;
            levelChanged = true;
            currentState = State.Start;

            player = new Player();
            items = new ItemHandler(this.Content);
            enemies = new EnemyHandler(this.Content);
            collisions = new CollisionHandler();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            startScreen = Content.Load<Texture2D>("images/startScreen");
            overScreen = Content.Load<Texture2D>("images/gameOverScreen");
            player.LoadContent(this.Content); 
        }

        protected override void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            if (currentState == State.Over)
            {
                if (currentKeyboardState.IsKeyDown(Keys.Escape) && oldKeyboardState.IsKeyUp(Keys.Escape))
                    this.Exit();
                if (currentKeyboardState.IsKeyDown(Keys.Back) && oldKeyboardState.IsKeyUp(Keys.Back))
                    this.Initialize(); 
            }

            if (currentState == State.Start && currentKeyboardState.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter))
                currentState = State.Running;

            if(currentState == State.Running)
            {
                player.Update(gameTime, currentKeyboardState, oldKeyboardState);
                enemies.Update(gameTime, player);
                items.Update();

                collisions.HandleItemCollision(player, items);
                collisions.HandleBulletCollisions(player, enemies);

                if (levelChanged)
                {
                    if (player.Hitpoints < 100)
                        player.Hitpoints = 100;
                    items.Create = 3 * currentLevel;
                    enemies.Create = currentLevel;
                    levelChanged = false;
                }

                if (items.items.Count == 0 && items.Create == 0)
                {
                    levelChanged = true;
                    currentLevel += 1;
                }

                if (player.Hitpoints <= 0)
                    currentState = State.Over;
            }

            oldKeyboardState = currentKeyboardState;

            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(time > 1.0f)
                {
                    FPS = frames;
                    frames = 0;
                    time = 0.0f;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) 
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            if(currentState == State.Start)
            {
                spriteBatch.Draw(startScreen, Vector2.Zero, Color.White);
            }

            if(currentState == State.Running)
            {
                player.Draw(spriteBatch);
                items.DrawItems(spriteBatch);
                enemies.DrawEnemies(spriteBatch);
                spriteBatch.DrawString(font, string.Format("Score: {0}", player.Score), new Vector2(16, 16), Color.Black);
                spriteBatch.DrawString(font, string.Format("{0} HP", player.Hitpoints), new Vector2(400 - 30 - 16, 16), Color.Black);
            }

            if(currentState == State.Over)
            {
                spriteBatch.Draw(overScreen, Vector2.Zero, Color.White);
                spriteBatch.DrawString(font, string.Format("Final Score: {0}", player.Score), new Vector2(16, 16), Color.Black);
            }
            spriteBatch.DrawString(font, string.Format("{0} FPS", FPS), new Vector2(800 - 50 - 16, 16), Color.Black);
            spriteBatch.End();

            frames++;

            base.Draw(gameTime);
        }
    }
}
