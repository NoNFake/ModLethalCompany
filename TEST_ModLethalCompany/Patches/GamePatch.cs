// /Patches/Sprint.cs

using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using MiNET.Plugins;
using ModLethalCompany;

namespace ModLethalCompany.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]


    internal class GamePatch
    {

        /*[HarmonyPatch(nameof(PlayerControllerB.))]*/
        [HarmonyPostfix, HarmonyPatch("Update")]
        static void sprintPatch(ref bool ___takingFallDamage, ref float ___sprintMeter, ref float ___carryWeight, ref float ___climbSpeed)
        {
            // float
            ___sprintMeter = Plugin.sprint.Value;
            ___carryWeight = 0.5f;
            ___climbSpeed = Plugin.climb_speed.Value;

            // bool
            ___takingFallDamage = Plugin.fall_damage.Value;
        } // Update


        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        static void getNightVision(ref PlayerControllerB __instance)
        {

            Plugin.playerRef = __instance;
            bool nightVisionEnabled = Plugin.nightVision.Value;
            float nightVisionIntensity = Plugin.nightVisionIntensity.Value;
            float nightVisionRange = Plugin.nightVisionRange.Value;

            Plugin.playerRef.nightVision.color = UnityEngine.Color.white;
            Plugin.playerRef.nightVision.intensity = nightVisionIntensity;
            Plugin.playerRef.nightVision.range = nightVisionRange;

            Plugin.playerRef.nightVision.enabled = nightVisionEnabled;
        }
    }
}
