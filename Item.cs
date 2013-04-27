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
    class Item : Sprite
    {
        const string assetName = "images/spriteSheet";

        const int w = 16;
        const int h = 16;

        int src;
        int clr;

        Random random = new Random();

        Rectangle[] sources = new Rectangle[] {
            new Rectangle(0, h, w, h),
            new Rectangle(w, h, w, h),
            new Rectangle(2 * w, h, w, h)
        };

        Color[] colors = new Color[] {
            Color.Red,
            Color.Green,
            Color.Blue
        };

        public Color Color { get; private set; }

        public int W { get { return w; } }

        public int H { get { return h; } }

        public bool Visible { get; set; }

        public int Type { get; private set; }
        
        public Item()
        {
            this.Position = new Vector2(random.Next(1, 49) * 16, random.Next(1, 36) * 16);
            this.Visible = true;
            src = random.Next(0, sources.Length + 1) % sources.Length;
            clr = random.Next(0, colors.Length + 1) % colors.Length;
            this.Type = src;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            base.LoadContent(theContentManager, assetName);
            this.Source = sources[src];
            this.Color = colors[clr];
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            base.Draw(theSpriteBatch, new Vector2(w / 2, h / 2), this.Position, this.Color, 0.0f);
        }
    }
}
