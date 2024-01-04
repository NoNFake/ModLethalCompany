// Plugin.cs

using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Runtime.CompilerServices;
using ModLethalCompany.Patches;
using MiNET.Effects;

namespace ModLethalCompany
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class Plugin : BaseUnityPlugin
    {






        private const string modGUID = "UiX.LC";
        private const string modName = "UiX";
        private const string modVersion = "1.0.0.4";

        private readonly Harmony harmony = new Harmony(modGUID);


        internal static PlayerControllerB? playerRef;
        private static Plugin? Instance;
        internal ManualLogSource? mls;


        // bool
        internal static bool nightVision = false;
        internal static bool SetNightVisionEnabled = false;
        internal static bool isPlayerControlled = false;
        internal static bool isPlayerDead = false;
        internal static bool IsInspectingItem = false;
        internal static bool twoHanded = false;
        internal static bool twoHandedAnimation = false;
        internal static bool isHoldingObject = true;
        /*
         
         public bool twoHanded;

	        public bool twoHandedAnimation;
         */

        // float
        internal static float nightVisionIntensity = 0f;
        internal static float nightVisionRange = 0f;
        internal static float carryWeight = 1f; // 
        internal static float carryRate = 1f;
        internal static float carryRateMultiplier = 1f;
        internal static float targetFOV = 66f;
        internal static float climbSpeed = 4f;

        // 
        internal static UnityEngine.Color nightVisionColor = UnityEngine.Color.black;


        void Awake()
        {
            if (Instance == null) { Instance = this; }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("The test mod has started");

            harmony.PatchAll(typeof(Plugin));
            harmony.PatchAll(typeof(PlayerControllerBPatch));
        }

        internal static void WaitForSeconds(float v)
        {
            throw new NotImplementedException();
        }
    }
}