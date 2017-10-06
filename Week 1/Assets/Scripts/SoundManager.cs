using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Audio;

namespace Assets.Scripts
{
    public static class SoundManager
    {
        private static AudioMixerSnapshot menu;
        private static AudioMixerSnapshot game;

        public static void SetManagers(AudioMixer audioMixer)
        {
            game = audioMixer.FindSnapshot("Game");
            menu = audioMixer.FindSnapshot("Menu");
        }

        public static void MenuMode(float transitionTime = 0.5f)
        {
            menu.TransitionTo(transitionTime);
        }

        public static void GameMode(float transitionTime = 0.5f)
        {
            game.TransitionTo(transitionTime);
        }
    }
}
