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
    class Bullet : Sprite
    {
        const string assetName = "images/spriteSheet";

        int w = 9;
        int h = 5;

        Vector2 direction = Vector2.Zero;

        public bool Visible { get; set; }

        public bool Bouncing { get; set; }
        
        float velocity;

        float lifeTime;

        public Bullet(Vector2 position, Vector2 direction, bool bouncing, float lifeTime, float velocity)
        {
            this.Position = position;
            this.direction = direction;
            this.Visible = true;
            this.Bouncing = bouncing;
            this.lifeTime = lifeTime;
            this.velocity = velocity;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            base.LoadContent(theContentManager, assetName);
            Source = new Rectangle(0, 48, w, h);
        }

        public void Update(GameTime theGameTime)
        {
            if ((this.X > 800 || this.X < 0 || this.Y < 0 || this.Y > 600) && !Bouncing)
                this.Visible = false;
            else if(Bouncing)
            {
                if (this.X > 800 || this.X < 0)
                    this.direction.X *= -1;
                if (this.Y > 600 || this.Y < 0)
                    this.direction.Y *= -1;
            }

            if (this.lifeTime > 0.0f)
                lifeTime -= (float)theGameTime.ElapsedGameTime.TotalSeconds;
            else
                this.Visible = false;

            Position += direction * velocity * (float)theGameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            base.Draw(theSpriteBatch, Vector2.Zero, this.Position, Color.White, 0.0f);
        }
    }
}
