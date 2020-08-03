// Decompiled with JetBrains decompiler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A8D8CAFB-CD16-4CDA-9F47-7D36796BFC75
// Assembly location: E:\Games\OxygenNotIncluded\OxygenNotIncluded_Data\Managed\Assembly-CSharp.dll

using Creature_Motion_Sensor;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

public class LogicCreatureSensorConfig : IBuildingConfig
{
  public const string ID = "LogicCreatureSensor";
  private int RANGE = 4;

  public override BuildingDef CreateBuildingDef()
  {
    BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("LogicCreatureSensor", 1, 1, "presence_sensor_kanim", 30, 30f, TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0, MATERIALS.REFINED_METALS, 1600f, BuildLocationRule.OnFoundationRotatable, TUNING.BUILDINGS.DECOR.PENALTY.TIER0, TUNING.NOISE_POLLUTION.NOISY.TIER0, 0.2f);
    buildingDef.Floodable = false;
    buildingDef.Overheatable = false;
    buildingDef.Entombable = false;
    buildingDef.AudioCategory = "Metal";
    buildingDef.ViewMode = OverlayModes.Logic.ID;
    buildingDef.SceneLayer = Grid.SceneLayer.Building;
    buildingDef.PermittedRotations = PermittedRotations.R360;
    buildingDef.AlwaysOperational = true;
    buildingDef.LogicOutputPorts = new List<LogicPorts.Port>()
    {
      LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), (string) TechAndPlanPatches.LogicCreatureSensorBuildingPlanPatch.CSENSOROUTPUT_NAME, (string) TechAndPlanPatches.LogicCreatureSensorBuildingPlanPatch.CSENSOROUTPUT_ACTIVE, (string) TechAndPlanPatches.LogicCreatureSensorBuildingPlanPatch.CSENSOROUTPUT_INACTIVE, true, false)
    };
    GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, "LogicCreatureSensor");
    return buildingDef;
  }

  public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
  {
        LogicCreatureSensorConfig.AddVisualizer(go, true);
  }

  public override void DoPostConfigureComplete(GameObject go)
  {
    LogicCreatureSensor LogicCreatureSensor = go.AddOrGet<LogicCreatureSensor>();
    LogicCreatureSensor.defaultState = false;
    LogicCreatureSensor.manuallyControlled = false;
    LogicCreatureSensor.pickupRange = 4;
    LogicCreatureSensorConfig.AddVisualizer(go, false);
    go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
  }

  private static void AddVisualizer(GameObject prefab, bool movable)
  {
    StationaryChoreRangeVisualizer choreRangeVisualizer = prefab.AddOrGet<StationaryChoreRangeVisualizer>();
    /*LogicCreatureSensor CurrentLogicCreatureSensor = (LogicCreatureSensor)prefab.GetComponent<LogicCreatureSensor>();
    bool notValid = ((Object)CurrentLogicCreatureSensor == (Object)null);
    if (notValid)
        return;
    int pickuprange = CurrentLogicCreatureSensor.pickupRange;
    choreRangeVisualizer.x = -pickuprange/2;
    choreRangeVisualizer.y = 0;
    choreRangeVisualizer.width = pickuprange + 1;
    choreRangeVisualizer.height = pickuprange + 1;*/
    choreRangeVisualizer.x = -2;
    choreRangeVisualizer.y = 0;
    choreRangeVisualizer.width = 5;
    choreRangeVisualizer.height = 5;
    choreRangeVisualizer.movable = movable;
  }
}
