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
    class Player : Sprite
    {

        const int w = 16;
        const int h = 16;


        Rectangle[] sources = new Rectangle[] {
            new Rectangle(0, 0, w, h), 
            new Rectangle(w, 0, w, h), 
            new Rectangle(2 * w, 0, w, h), 
            new Rectangle(3 * w, 0, w, h)
        };


        const int animationLength = 12;

        float boostTime = 0.0f;

        float shieldRotation = 0.0f;
        
        int currentFrame = 0;

        const int maxHitpoints = 100;

        Vector2 direction = Vector2.Zero;

        Texture2D shield;
        Rectangle shieldSource;

        const string assetName = "images/spritesheet";

        public float Velocity { get; set; }

        public bool Boost { get; set; }

        public float BoostQuantity { get; set; }

        public bool Shield { get; set; } 

        public float ShieldTime { get; set; }

        public int W { get { return w; } }

        public int H { get { return h; } }

        public int Score { get; set; }

        public int Hitpoints { get; set; }

        public Player()
        {
            this.Position = new Vector2(400, 600 - h / 2);
            this.Score = 0;
            this.Velocity = 175f;
            this.BoostQuantity = 0;
            this.Boost = false;
            this.Shield = false;
            this.ShieldTime = 0.0f;
            this.Hitpoints = maxHitpoints;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            base.LoadContent(theContentManager, assetName);
            shield = theContentManager.Load<Texture2D>(assetName);
            shieldSource = new Rectangle(16, 32, 16, 16);
            this.Source = sources[0];
        }

        public void Update(GameTime theGameTime, KeyboardState currentKeyboardState, KeyboardState oldKeyboardState)
        {
            direction = Vector2.Zero;
            
            if (X < w / 2)
                X = w / 2;
            if (X > 800 - w / 2)
                X = 800 - w / 2;
            if (Y < h / 2)
                Y = h / 2;
            if (Y > 600 - h / 2)
                Y = 600 - h / 2;

            if (currentKeyboardState.IsKeyDown(Keys.Left))
                direction.X = -1;
            else if (currentKeyboardState.IsKeyDown(Keys.Right))
                direction.X = 1;
            else if (currentKeyboardState.IsKeyDown(Keys.Up))
                direction.Y = -1;
            else if (currentKeyboardState.IsKeyDown(Keys.Down))
                direction.Y = 1;
            
            this.Position += direction * Velocity * (float)theGameTime.ElapsedGameTime.TotalSeconds;

            this.UpdateBoost(theGameTime);
            this.UpdateShield(theGameTime);
            this.Animate();

        }

        private void UpdateShield(GameTime theGameTime)
        {
            if(ShieldTime > 0.0f)
            {
                ShieldTime -= (float)theGameTime.ElapsedGameTime.TotalSeconds;
                shieldRotation += 20f * (float)theGameTime.ElapsedGameTime.TotalSeconds;
                Shield = true;
            }
            else
            {
                ShieldTime = 0.0f;
                Shield = false;
            }

        }

        private void UpdateBoost(GameTime theGameTime)
        {
            if (Boost)
            {
                boostTime = 10.0f;
                if (Velocity < 225)
                    Velocity += Velocity / 100 * BoostQuantity;
                Boost = false;
            }

            if (boostTime > 0.0f)
                boostTime -= (float)theGameTime.ElapsedGameTime.TotalSeconds;
            else
                Velocity = 175f; 
        }

        private void Animate()
        {
            if (direction.Y >= 0)
                if (currentFrame < animationLength / 2)
                    Source = sources[0];
                else if (currentFrame < animationLength)
                    Source = sources[1];
                else
                    currentFrame = 0;   //its horrible i know
            else if (direction.Y < 0)
                if (currentFrame < animationLength / 2)
                    Source = sources[2];
                else if (currentFrame < animationLength)
                    Source = sources[3];
                else
                    currentFrame = 0;

            if (direction != Vector2.Zero)
                currentFrame++;
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            if (Shield)
                theSpriteBatch.Draw(shield, new Vector2(X, Y), shieldSource, Color.White, shieldRotation, new Vector2(w / 2, h / 2), 1.0f, SpriteEffects.None, 0);
            base.Draw(theSpriteBatch, new Vector2(w / 2, h / 2), this.Position, Color.White, 0.0f);
        }
    }
}
