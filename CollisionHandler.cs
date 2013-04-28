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

        public void HandleItemCollision(Player player, ItemHandler items, SoundHandler sounds)
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
                            sounds.PlayPickUp(0);
                            player.Score += 5;
                        }
                        else if (item.Color == Color.Green)
                        {
                            sounds.PlayPickUp(1);
                            player.Score += 10;
                            if (item.Type != 0)
                            {
                                player.Boost = true;
                                player.BoostQuantity = item.Type * 10;
                                sounds.PlayBoost();
                            }
                        }
                        else if (item.Color == Color.Blue)
                        {
                            sounds.PlayPickUp(2);
                            player.Score += 15;
                            if (item.Type != 0)
                            {
                                player.Shield = true;
                                player.ShieldTime = item.Type * 5f;
                                sounds.PlayShiled();
                            }
                        }
                        item.Visible = false;
                    }
                }
        }

        public void HandleBulletCollisions(Player player, EnemyHandler enemies, SoundHandler sounds)
        {
            playerRectangle = new Rectangle((int)player.X, (int)player.Y, player.W, player.H);
            foreach (Enemy enemy in enemies.enemies)
                    foreach (Bullet b in enemy.bullets)
                        if (b.Visible)
                        {
                            bulletRectangle = new Rectangle((int)b.X, (int)b.Y, 3, 3);
                            if (bulletRectangle.Intersects(playerRectangle))
                            {
                                b.Visible = false;
                                sounds.PlayHurt();
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
