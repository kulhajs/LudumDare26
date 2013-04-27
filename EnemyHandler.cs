using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LD26_minimalism
{
    class EnemyHandler
    {
        public List<Enemy> enemies;
        Enemy newEnemy;

        ContentManager contentManager;

        public int Create { get; set; }

        public EnemyHandler(ContentManager theContentManager)
        {
            contentManager = theContentManager;
            enemies = new List<Enemy>(20);
            this.Create = 0;
        }

        public void Update(GameTime theGameTime, Player player)
        {
            if (Create > 0)
            {
                this.AddEnemy();
                Create--;
            }

            foreach (Enemy enemy in enemies)
                enemy.Update(theGameTime, player);
        }

        public void AddEnemy()
        {
                newEnemy = new Enemy();
                newEnemy.LoadContent(this.contentManager);
                enemies.Add(newEnemy);
                newEnemy = null;
        }

        public void DrawEnemies(SpriteBatch theSpriteBatch)
        {
            if (enemies.Count > 0)
                foreach (Enemy e in enemies)
                    e.Draw(theSpriteBatch);
        }
    }
}
