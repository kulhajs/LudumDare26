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
    class Sprite
    {
        public Texture2D texture;
        private Rectangle source;
        private Rectangle size;
        private float scale = 1.0f;
        public Vector2 Position = Vector2.Zero;
        public ContentManager contentManager;

        public float Rotation { get; set; }

        public float X
        {
            get { return this.Position.X; }
            set { this.Position.X = value; }
        }

        public float Y
        {
            get { return this.Position.Y; }
            set { this.Position.Y = value; }
        }
        
        public float Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                size = new Rectangle(0, 0, (int)(source.Width * scale), (int)(source.Height * scale));
            }
        }

        public Rectangle Source
        {
            get { return source; }
            set
            {
                source = value;
                size = new Rectangle(0, 0, source.Width, source.Height);
            }
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            texture = theContentManager.Load<Texture2D>(theAssetName);
            Source = new Rectangle(0, 0, (int)(texture.Width * scale), (int)(texture.Height * scale));
            size = new Rectangle(0, 0, (int)(texture.Width * scale), (int)(texture.Height * scale));
        }

        public virtual void Draw(SpriteBatch theSpriteBatch, Vector2 origin, Vector2 position, Color color, float rotation)
        {
            theSpriteBatch.Draw(texture, position, source, color, rotation, origin, scale, SpriteEffects.None, 0);
        }


        public float Fsqrt(float x)
        {
            return (float)Math.Sqrt((double)x);
        }

        public float FAtan(float x)
        {
            return (float)Math.Atan((double)x);
        }

        public float FSin(float x)
        {
            return (float)Math.Sin((double)x);
        }

        public float FCos(float x)
        {
            return (float)Math.Cos((double)x);
        }

        public float FPI { get { return (float)Math.PI; } }

    }
}
