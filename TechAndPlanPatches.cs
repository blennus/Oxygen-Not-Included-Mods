using System.Collections.Generic;
using Database;
using Harmony;

namespace Better_Timed_Gates
{
    public class TechAndPlanPatches
    {
        //public static class Mod_OnLoad
        //{
        //    public static void OnLoad()
        //    {
        //        Debug.Log("WE ARE LOADED 2.7182818284590452353602874713526624977572470936999...");
        //    }
        //}

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        public class BetterTimerGatesBuildingPlanPatch
        {
            public static LocString BBUFFERNAME = new LocString("TIMER BUFFER Gate", "STRINGS.BUILDINGS.PREFABS." +
                                                                LogicGateBetterBufferConfig.ID.ToUpper() + ".NAME");
            public static LocString BBUFFERDESC = new LocString("This gate continues outputting a Green Signal for a long time after the gate stops receiving a Green Signal.",
                                                                "STRINGS.BUILDINGS.PREFABS." + LogicGateBetterBufferConfig.ID.ToUpper() + ".DESC");
            public static LocString BBUFFEREFFECT = new LocString(STRINGS.BUILDINGS.PREFABS.LOGICGATEBUFFER.EFFECT.text, "STRINGS.BUILDINGS.PREFABS." + LogicGateBetterBufferConfig.ID.ToUpper() + ".EFFECT");
            public static LocString BBUFFEROUTPUT_NAME = new LocString(STRINGS.BUILDINGS.PREFABS.LOGICGATEBUFFER.OUTPUT_NAME.text, "STRINGS.BUILDINGS.PREFABS." + LogicGateBetterBufferConfig.ID.ToUpper() + ".OUTPUT_NAME");
            public static LocString BBUFFEROUTPUT_ACTIVE = new LocString(STRINGS.BUILDINGS.PREFABS.LOGICGATEBUFFER.OUTPUT_ACTIVE.text, "STRINGS.BUILDINGS.PREFABS." + LogicGateBetterBufferConfig.ID.ToUpper() + ".OUTPUT_ACTIVE");
            public static LocString BBUFFEROUTPUT_INACTIVE = new LocString(STRINGS.BUILDINGS.PREFABS.LOGICGATEBUFFER.OUTPUT_INACTIVE.text, "STRINGS.BUILDINGS.PREFABS." + LogicGateBetterBufferConfig.ID.ToUpper() + ".OUTPUT_INACTIVE");

            public static LocString BFILTERNAME = new LocString("TIMER FILTER Gate", "STRINGS.BUILDINGS.PREFABS." +
                                                    LogicGateBetterFilterConfig.ID.ToUpper() + ".NAME");
            public static LocString BFILTERDESC = new LocString(STRINGS.BUILDINGS.PREFABS.LOGICGATEFILTER.DESC.text, "STRINGS.BUILDINGS.PREFABS." + LogicGateBetterFilterConfig.ID.ToUpper() + ".DESC");
            public static LocString BFILTEREFFECT = new LocString(STRINGS.BUILDINGS.PREFABS.LOGICGATEFILTER.EFFECT.text, "STRINGS.BUILDINGS.PREFABS." + LogicGateBetterFilterConfig.ID.ToUpper() + ".EFFECT");
            public static LocString BFILTEROUTPUT_NAME = new LocString(STRINGS.BUILDINGS.PREFABS.LOGICGATEFILTER.OUTPUT_NAME.text, "STRINGS.BUILDINGS.PREFABS." + LogicGateBetterFilterConfig.ID.ToUpper() + ".OUTPUT_NAME");
            public static LocString BFILTEROUTPUT_ACTIVE = new LocString(STRINGS.BUILDINGS.PREFABS.LOGICGATEFILTER.OUTPUT_ACTIVE.text, "STRINGS.BUILDINGS.PREFABS." + LogicGateBetterFilterConfig.ID.ToUpper() + ".OUTPUT_ACTIVE");
            public static LocString BFILTEROUTPUT_INACTIVE = new LocString(STRINGS.BUILDINGS.PREFABS.LOGICGATEFILTER.OUTPUT_INACTIVE.text, "STRINGS.BUILDINGS.PREFABS." + LogicGateBetterFilterConfig.ID.ToUpper() + ".OUTPUT_INACTIVE");

            public static void Prefix()
            {
                Strings.Add(BBUFFERNAME.key.String, BBUFFERNAME.text);
                Strings.Add(BBUFFERDESC.key.String, BBUFFERDESC.text);
                Strings.Add(BBUFFEREFFECT.key.String, BBUFFEREFFECT.text);
                Strings.Add(BBUFFEROUTPUT_NAME.key.String, BBUFFEROUTPUT_NAME.text);
                Strings.Add(BBUFFEROUTPUT_ACTIVE.key.String, BBUFFEROUTPUT_ACTIVE.text);
                Strings.Add(BBUFFEROUTPUT_INACTIVE.key.String, BBUFFEROUTPUT_INACTIVE.text);
                ModUtil.AddBuildingToPlanScreen("Automation", LogicGateBetterBufferConfig.ID);
                
                Strings.Add(BFILTERNAME.key.String, BFILTERNAME.text);
                Strings.Add(BFILTERDESC.key.String, BFILTERDESC.text);
                Strings.Add(BFILTEREFFECT.key.String, BFILTEREFFECT.text);
                Strings.Add(BFILTEROUTPUT_NAME.key.String, BFILTEROUTPUT_NAME.text);
                Strings.Add(BFILTEROUTPUT_ACTIVE.key.String, BFILTEROUTPUT_ACTIVE.text);
                Strings.Add(BFILTEROUTPUT_INACTIVE.key.String, BFILTEROUTPUT_INACTIVE.text);
                ModUtil.AddBuildingToPlanScreen("Automation", LogicGateBetterFilterConfig.ID);

                Debug.Log("Timer Buffer and Timer Filter Loaded into Automation Building Planning Pane");
            }
        }

        [HarmonyPatch(typeof(Db), "Initialize")]
        public class BetterTimerGatesDBPatch
        {
            public static void Prefix()
            {
                List<string> techgroupinglist = new List<string> (Techs.TECH_GROUPING["LogicCircuits"]) { LogicGateBetterBufferConfig.ID , LogicGateBetterFilterConfig.ID };
                Techs.TECH_GROUPING["LogicCircuits"] = techgroupinglist.ToArray();
                Debug.Log("Timer Buffer and Timer Filter Loaded into Tech Tree");
            }
        }
    }
}
