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
    class Enemy : Sprite
    {
        const string assetName = "images/spriteSheet";

        Random random = new Random();

        Color[] colors = new Color[] {
             Color.Red,
             Color.Green,
             Color.Blue
        };

        public List<Bullet> bullets;
        Bullet newBullet;

        const int w = 16;
        const int h = 16;

        float dx;
        float dy;

        float dist;

        Vector2 dir;

        int clr;

        float initReloadTime;
        float reloadTime;

        public Color Color { get; private set; }

        public int Type { get; private set; }

        int radius;

        public Enemy()
        {
            bullets = new List<Bullet>(20);
            this.Position = new Vector2(random.Next(1, 49) * 16, random.Next(1, 36) * 16);
            clr = random.Next(0, colors.Length + 1) % colors.Length;
            this.Type = clr;

            if (this.Type < 2)
                initReloadTime = 1.5f;
            else
                initReloadTime = 2f;

            if (this.Type == 0)
                this.radius = 200;
            else if (this.Type == 1)
                this.radius = 300;
            else
                this.radius = 250;

            reloadTime = 0.0f;
        }

        public void Update(GameTime theGameTime, Player player)
        {
            if (reloadTime < initReloadTime)
                reloadTime += (float)theGameTime.ElapsedGameTime.TotalSeconds;
            else
                reloadTime = initReloadTime;

             dx = (player.X - this.X);
             dy = (player.Y - this.Y);
             dist = dx * dx + dy * dy;

            if (reloadTime == initReloadTime && dist <= radius * radius)
                this.UpdateAttack();

            if (bullets.Count > 0)
                foreach (Bullet b in bullets)
                    if (b.Visible)
                        b.Update(theGameTime);

            this.RemoveBullets();
        }

        private void RemoveBullets()
        {
            foreach (Bullet b in bullets)
                if (!b.Visible)
                {
                    bullets.Remove(b);
                    break;
                }
        }

        private void UpdateAttack()
        {
            if (this.Type < 2)
            {
                dir = new Vector2(dx, dy);
                dir.Normalize();
                newBullet = new Bullet(this.Position, dir, this.Type == 0 ? false : true, this.Type == 1 ? 15f : 500f, 300f);
                newBullet.LoadContent(contentManager);
                bullets.Add(newBullet);
                newBullet = null;
                reloadTime = 0.0f;
            }
            else
            {
                for (int i = 0; i < 13; i++)
                {
                    float dirr = i * 0.25f * (i % 2 * 2 - 1);
                    dir = new Vector2(FSin(dirr), FCos(dirr));
                    dir.Normalize();
                    newBullet = new Bullet(this.Position, dir, false, 7.5f, 200f);
                    newBullet.LoadContent(contentManager);
                    bullets.Add(newBullet);
                    newBullet = null;
                }
                reloadTime = 0.0f;
            }
        }

        public void LoadContent(ContentManager theContentManager)
        {
            contentManager = theContentManager;
            base.LoadContent(contentManager, assetName);
            this.Source = new Rectangle(0, 2 * h, w, h);
            this.Color = colors[clr];
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            base.Draw(theSpriteBatch, new Vector2(w / 2, h / 2), this.Position, this.Color, 0.0f);

            if (bullets.Count > 0)
                foreach (Bullet b in bullets)
                    if (b.Visible)
                        b.Draw(theSpriteBatch);
        }
    }
}
