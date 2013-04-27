using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LD26_minimalism
{
    class CollisionHandler
    {
        Rectangle playerRectangle;
        Rectangle itemRectangle;
        Rectangle bulletRectangle;

        public void HandleItemCollision(Player player, ItemHandler items)
        {
            playerRectangle = new Rectangle((int)player.X - player.W / 2, (int)player.Y - player.H / 2, player.W, player.H);
            foreach (Item item in items.items)
                if (item.Visible)
                {
                    itemRectangle = new Rectangle((int)item.X - item.W / 4, (int)item.Y - item.H / 4, item.W / 2, item.H / 2);
                    if (playerRectangle.Intersects(itemRectangle))
                    {
                        if(item.Color == Color.Red)
                        {
                            player.Score += 5;
                        }
                        else if (item.Color == Color.Green)
                        {
                            player.Score += 10;
                            if (item.Type != 0)
                            {
                                player.Boost = true;
                                player.BoostQuantity = item.Type * 10;
                            }
                        }
                        else if (item.Color == Color.Blue)
                        {
                            player.Score += 15;
                            if (item.Type != 0)
                            {
                                player.Shield = true;
                                player.ShieldTime = item.Type * 5f; 
                            }
                        }
                        item.Visible = false;
                    }
                }
        }

        public void HandleBulletCollisions(Player player, EnemyHandler enemies)
        {
            playerRectangle = new Rectangle((int)player.X, (int)player.Y, player.W, player.H);
            foreach (Enemy enemy in enemies.enemies)
                if (enemy.bullets.Count > 0)
                    foreach (Bullet b in enemy.bullets)
                        if (b.Visible)
                        {
                            bulletRectangle = new Rectangle((int)b.X, (int)b.Y, 3, 3);
                            if (bulletRectangle.Intersects(playerRectangle))
                            {
                                b.Visible = false;
                                if (!player.Shield)
                                {
                                    player.Hitpoints -= 10;
                                }
                                else
                                {
                                    player.ShieldTime /= 2;
                                }
                            }
                        }
        }
    }
}
