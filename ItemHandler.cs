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
    class ItemHandler
    {

        public List<Item> items;
        Item newItem;

        ContentManager contentManager;

        public int Create { get; set; }
                
        public ItemHandler(ContentManager theContentManager)
        {
            contentManager = theContentManager;
            items = new List<Item>(50);
        }

        public void Update()
        {
            if (Create > 0)
            {
                this.AddItem();
                Create--;
            }

                this.RemoveItems();
        }

        private void RemoveItems()
        {
            foreach(Item i in items)
                if(!i.Visible)
                {
                    items.Remove(i);
                    break;
                }
        }

        private void AddItem()
        {
            newItem = new Item();
            newItem.LoadContent(contentManager);
            items.Add(newItem);
            newItem = null;
        }

        public void DrawItems(SpriteBatch theSpriteBatch)
        {
            if (items.Count > 0)
                foreach (Item i in items)
                    if (i.Visible)
                        i.Draw(theSpriteBatch);
        }
    }
}
