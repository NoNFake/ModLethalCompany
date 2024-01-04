// PlayerControlerBPatch.cs

using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using MiNET.Plugins;
using ModLethalCompany;
namespace ModLethalCompany.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
   

        internal class PlayerControllerBPatch
        { 

        /*[HarmonyPatch(nameof(PlayerControllerB.))]*/
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void infiniteSprintPatch(ref float ___sprintMeter, ref bool ___takingFallDamage, ref float ___targetFOV, ref float ___carryWeight, ref float ___climbSpeed)
        {
            ___sprintMeter = 1f;                  // *1f Stamina
            ___takingFallDamage = false;            // Fall Damage
            ___targetFOV = 12f;                     // *Test* FOW
            ___carryWeight = 0.6f;                  // Weight
            ___climbSpeed = 15f;                    // Climb 
            
        }


        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        static void getNightVision(ref PlayerControllerB __instance)
        {
            Plugin.playerRef = __instance;
            Plugin.nightVision = Plugin.playerRef.nightVision.enabled;

            Plugin.playerRef.nightVision.color = UnityEngine.Color.white; // Night vision
            Plugin.playerRef.nightVision.intensity = 9000f;
            Plugin.playerRef.nightVision.range = 90000f;

            // Ensure that night vision is always enabled
            Plugin.playerRef.nightVision.enabled = true;
        }

        [HarmonyPatch("KillPlayer")]
        [HarmonyPrefix]
        static void KillControl()
        {
            Plugin.playerRef = null;
            Plugin.isPlayerControlled = true;
            Plugin.isPlayerDead = true;
            Plugin.IsInspectingItem = true;
        }

        // BeginGrabObject()

        [HarmonyPatch("SwitchToItemSlot")]
        [HarmonyPrefix]
        static void NoWeigtItem()
        {
            Plugin.twoHanded = true;
            Plugin.isHoldingObject = false;
        }


       
    }
}
