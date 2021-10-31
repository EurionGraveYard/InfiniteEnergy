using Harmony;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

namespace TestMod  // This is usually the name of your mod.
{
    [HarmonyPatch(typeof(PlayerComponent))]  // We're patching the PlayerComponent class.
    [HarmonyPatch("Update")]        // The PlayerComponent class's CheckEnergy method specifically.
    internal class PlayerComponent_Update_Patch
    {
        public static bool infiniteEnabled = true;
        
        

        [HarmonyPrefix]      // Run this after the default game's PlayerComponent Update method runs.
        public static bool Prefix(PlayerComponent __instance)
        {
            if (Input.GetKeyUp(KeyCode.L))
            {
                infiniteEnabled = !infiniteEnabled;
                //lets test shit.
                EffectBubblesManager.ShowImmediately(MainGame.me.player.pos3, infiniteEnabled ? "Infinite Energy Activated" : "Infinite Energy De-Activated", EffectBubblesManager.BubbleColor.Green);
            }

            if (infiniteEnabled)
            {
                MainGame.me.player.energy = (float)MainGame.me.save.max_energy - 1;
            }

            return true;
        }       
    }
    public class Config
    {
        public KeyCode ToggleButton { get; set; }
        public KeyCode InputButton { get; set; }

    }
}