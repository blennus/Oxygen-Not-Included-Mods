using Harmony;
using System.Collections.Generic;
using Database;
using STRINGS;

namespace Creature_Motion_Sensor
{
    public class TechAndPlanPatches
    {
        /*public static class Mod_OnLoad
        {
            public static void OnLoad()
            {
                Debug.Log("WE ARE LOADED 2.7182818284590452353602874713526624977572470936999...");
            }
        }*/

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        public class LogicCreatureSensorBuildingPlanPatch
        {
            public static LocString CSENSORNAME = new LocString("Creature Motion Sensor", "STRINGS.BUILDINGS.PREFABS." +
                                                                LogicCreatureSensorConfig.ID.ToUpper() + ".NAME");
            public static LocString CSENSORDESC = new LocString("Motion sensors can be used for special cases in ranching by sensing when critters are nearby.",
                                                                "STRINGS.BUILDINGS.PREFABS." + LogicCreatureSensorConfig.ID.ToUpper() + ".DESC");
            public static LocString CSENSOREFFECT = new LocString(("Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active)
                                                                   + " or a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " based on whether a Critter is in the sensor's range."),
                                                                   "STRINGS.BUILDINGS.PREFABS." + LogicCreatureSensorConfig.ID.ToUpper() + ".EFFECT");
            public static LocString CSENSOROUTPUT_NAME = new LocString("Creature Motion Sensor", "STRINGS.BUILDINGS.PREFABS." + LogicCreatureSensorConfig.ID.ToUpper() + ".OUTPUT_NAME");
            public static LocString CSENSOROUTPUT_ACTIVE = new LocString(("Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " while a Critter is in the sensor's tile range"), "STRINGS.BUILDINGS.PREFABS." + LogicCreatureSensorConfig.ID.ToUpper() + ".OUTPUT_ACTIVE");
            public static LocString CSENSOROUTPUT_INACTIVE = new LocString(("Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby)), "STRINGS.BUILDINGS.PREFABS." + LogicCreatureSensorConfig.ID.ToUpper() + ".OUTPUT_INACTIVE");
            public static LocString CSENSOROUTPUT_TOOLTIP = new LocString(("Will send a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if there is a critter withing <b>{0}</b> meters."), "STRINGS.UI.UISIDESCREENS." + LogicCreatureSensorConfig.ID.ToUpper() + "SIDESCREEN.TOOLTIP");


            public static void Prefix()
            {
                Strings.Add(CSENSORNAME.key.String, CSENSORNAME.text);
                Strings.Add(CSENSORDESC.key.String, CSENSORDESC.text);
                Strings.Add(CSENSOREFFECT.key.String, CSENSOREFFECT.text);
                Strings.Add(CSENSOROUTPUT_NAME.key.String, CSENSOROUTPUT_NAME.text);
                Strings.Add(CSENSOROUTPUT_ACTIVE.key.String, CSENSOROUTPUT_ACTIVE.text);
                Strings.Add(CSENSOROUTPUT_INACTIVE.key.String, CSENSOROUTPUT_INACTIVE.text);
                Strings.Add(CSENSOROUTPUT_TOOLTIP.key.String, CSENSOROUTPUT_TOOLTIP.text);
                ModUtil.AddBuildingToPlanScreen("Automation", LogicCreatureSensorConfig.ID);

                Debug.Log("Creature Motion Detector Loaded into Automation Building Planning Pane");
            }
        }
    }

    [HarmonyPatch(typeof(Db), "Initialize")]
    public class LogicCreatureSensorDBPatch
    {
        public static void Prefix()
        {
            List<string> techgroupinglist = new List<string>(Techs.TECH_GROUPING["AnimalControl"]) { LogicCreatureSensorConfig.ID };
            Techs.TECH_GROUPING["AnimalControl"] = techgroupinglist.ToArray();
            Debug.Log("Creature Motion Detector Loaded into Tech Tree");
        }
    }
}
