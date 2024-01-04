﻿using BepInEx.Configuration;
using GameNetcodeStuff;
using HarmonyLib;
using MiNET.Plugins;
using ModLethalCompany.Patches;
using System;

namespace BetterLadders
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    [Serializable]
    public class Config<T>
    {   



        {


        [NonSerialized]
        protected static int IntSize = 4;

        public static T Default { get; private set; }
        public static T Instance { get; private set; }

        public static bool Synced { get; internal set; }

        protected void InitInstance(T instance)
        {
            Default = instance;
            Instance = instance;

            // Makes sure the size of an integer is correct for the current system.
            // We use 4 by default as that's the size of an int on 32 and 64 bit systems.
            IntSize = sizeof(int);
        }

        internal static void SyncInstance(byte[] data)
        {
            Instance = DeserializeFromBytes(data);
            Synced = true;
        }



        //General (synced)
        public float climbSpeedMultiplier { get; internal set; }
        public float sprintingClimbSpeedMultiplier { get; internal set; }
        //public float transitionSpeedMultiplier { get; internal set; }
        public bool allowTwoHanded { get; internal set; }
        //General (not synced)
        public bool scaleAnimationSpeed { get; internal set; }
        public bool hideOneHanded { get; internal set; }
        public bool hideTwoHanded { get; internal set; }
        //Extension Ladders (synced)
        public float timeMultiplier { get; internal set; }
        //public float lengthMultiplier { get; internal set; }
        //public bool enableKillTrigger { get; internal set; }
        //Extension Ladders (not synced)
        public bool holdToPickup { get; internal set; }
        public float holdTime { get; internal set; }
        //Not in config
        public bool defaultsSet { get; internal set; }
        public Config(ConfigFile cfg)
        {
            InitInstance(this);
            //General (synced)
            climbSpeedMultiplier = cfg.Bind("General", "climbSpeedMultipler", 1.0f, "Ladder climb speed multiplier").Value;
            sprintingClimbSpeedMultiplier = cfg.Bind("General", "sprintingClimbSpeedMultiplier", 1.5f, "Ladder climb speed multiplier while sprinting, stacks with climbSpeedMultiplier").Value;
            //transitionSpeedMultiplier = cfg.Bind("General", "transitionSpeedMultiplier", 1.0f, "Ladder enter/exit animation speed multiplier").Value;
            allowTwoHanded = cfg.Bind("General", "allowTwoHanded", true, "Whether to allow using ladders while carrying a two-handed object").Value;
            //General (not synced)
            scaleAnimationSpeed = cfg.Bind("General", "scaleAnimationSpeed", true, "Whether to scale the speed of the climbing animation to the climbing speed").Value;
            hideOneHanded = cfg.Bind("General", "hideOneHanded", true, "Whether to hide one-handed items while climbing a ladder - false in vanilla").Value;
            hideTwoHanded = cfg.Bind("General", "hideTwoHanded", true, "Whether to hide two-handed items while climbing a ladder").Value;
            //Extension Ladders (synced)
            timeMultiplier = cfg.Bind("Extension Ladders", "timeMultiplier", 0f, "Extension ladder time multiplier (0 for permanent) - lasts 20 seconds in vanilla").Value;
            //lengthMultiplier = cfg.Bind("Extension Ladders", "lengthMultiplier", 1f, "Extension ladder length multiplier").Value;
            //enableKillTrigger = cfg.Bind("Extension Ladders", "enableKillTrigger", true, "Whether ladders should kill players they land on").Value;
            //Extension Ladders (not synced)
            holdToPickup = cfg.Bind("Extension Ladders", "holdToPickup", true, "Whether the interact key needs to be held to pick up an activated extension ladder").Value;
            holdTime = cfg.Bind("Extension Ladders", "holdTime", 0.5f, "How long, in seconds, the interact key must be held if holdToPickup is true").Value;
            //Not in config
            defaultsSet = false;
        }
        public void SetVanillaDefaults()
        {
            Config.Instance.climbSpeedMultiplier = 1.0f;
            Config.Instance.sprintingClimbSpeedMultiplier = 1.0f;
            //Config.Instance.transitionSpeedMultiplier = 1.0f;
            Config.Instance.allowTwoHanded = false;
            Config.Instance.timeMultiplier = 1.0f;
            //Config.Instance.lengthMultiplier = 1.0f;
            //Config.Instance.enableKillTrigger = true;
            defaultsSet = true;
        }

    }
}