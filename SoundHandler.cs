using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace LD26_minimalism
{
    class SoundHandler
    {

        SoundEffect pickUp1;
        SoundEffect pickUp2;
        SoundEffect pickUp3;
        SoundEffect levelUp;
        SoundEffect boost;
        SoundEffect shiled;
        SoundEffect hurt;

        float volume = 0.25f;

        List<SoundEffect> pickUpSounds;

        public SoundHandler()
            {
            pickUpSounds = new List<SoundEffect>(3);
            pickUpSounds.Add(pickUp1);
            pickUpSounds.Add(pickUp2);
            pickUpSounds.Add(pickUp3);
        }

        public void LoadContent(ContentManager theContentManager)
        {
            for(int i = 1; i < 4; i++)
                pickUpSounds[i - 1] = theContentManager.Load<SoundEffect>(string.Format("sounds/pick_{0}", i));
            levelUp = theContentManager.Load<SoundEffect>("sounds/levelup");
            boost = theContentManager.Load<SoundEffect>("sounds/boost");
            shiled = theContentManager.Load<SoundEffect>("sounds/shield");
            hurt = theContentManager.Load<SoundEffect>("sounds/hurt");
        }

        public void PlayPickUp(int i)
        {
            pickUpSounds[i].Play(volume, 0.0f, 0.0f);
        }

        public void PlayLevelUp()
        {
            levelUp.Play(volume, 0.0f, 0.0f);
        }

        public void PlayBoost()
        {
            boost.Play(volume, 0.0f, 0.0f);
        }

        public void PlayShiled()
        {
            shiled.Play(volume, 0.0f, 0.0f);
        }

        public void PlayHurt()
        {
            hurt.Play(volume, 0.0f, 0.0f);
        }
    }
}
