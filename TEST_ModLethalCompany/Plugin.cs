// Plugin.cs

using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;
// using TEST_ModLethalCompany.Patches;
using BepInEx.Configuration;
using LethalConfig;
using ModLethalCompany.Patches;
using LethalConfig.ConfigItems.Options;
using LethalConfig.ConfigItems;

namespace ModLethalCompany
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("ainavt.lc.lethalconfig")]
    public class Plugin : BaseUnityPlugin
    {
        private const string modGUID = "nonfake.LC_Modify";
        private const string modVersion = "1.0.1.4";
        private const string modName = $"{modVersion}r_UiX";
        

        private readonly Harmony harmony = new Harmony(modGUID);


        internal static PlayerControllerB? playerRef;
        private static Plugin? Instance;

        internal ManualLogSource? mls;


        // Any | floats
        internal static ConfigEntry<float> sprint;
        internal static ConfigEntry<float> climb_speed;

        // Any | bool
        internal static ConfigEntry<bool> fall_damage;


        // night vision
        // bool
        internal static ConfigEntry<bool> nightVision;

        // float 

        internal static ConfigEntry<float> nightVisionIntensity;
        internal static ConfigEntry<float> nightVisionRange;


        void Awake()
        {

            if (Instance == null ) { Instance = this; }

            // Any | Float
            sprint = Config.Bind("Sprint", "Infinity sprint", 0f, "");
            climb_speed = Config.Bind("Sprint", "Climb speed example 15", 0f, "This may cause major amounts of lag. use at your own risk");

            fall_damage = Config.Bind("Fall Damage", "No Damage when falling", true, "This may cause major amounts of lag. use at your own risk");


            // Night vision | bools
            nightVision = Config.Bind("Night Vision", "Enable night vision", false, "This may cause major amounts of lag. use at your own risk");

            // fliat
            nightVisionIntensity = Config.Bind("Night Vision", "Intensity of night vision", 0f, "This may cause major amounts of lag. use at your own risk");
            var VisionIntensity_Slider = new FloatSliderConfigItem(nightVisionIntensity, new FloatSliderOptions {  Min = 1f, Max = 9000f });
            LethalConfigManager.AddConfigItem(VisionIntensity_Slider);

            nightVisionRange = Config.Bind("Night Vision", "Range of night vision", 0f, "This may cause major amounts of lag. use at your own risk");
            var nightVisionRange_Slider = new FloatSliderConfigItem(nightVisionRange, new FloatSliderOptions {  Min = 1f, Max = 9000f });
            LethalConfigManager.AddConfigItem(nightVisionRange_Slider);




            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo($"<================================================>\n\n\t|The {modName} has started|\n\n<================================================>");

            harmony.PatchAll(typeof(Plugin));
            harmony.PatchAll(typeof(GamePatch));

        }

        internal static void WaitForSeconds(float v)
        {
            throw new NotImplementedException();
        }
    } // Plugin
}