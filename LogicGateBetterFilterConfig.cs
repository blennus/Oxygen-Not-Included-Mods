// Decompiled with JetBrains decompiler
// Type: LogicGateBetterFilterConfig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DFDEE8B9-A425-463E-AF51-00480BB21E0A
// Assembly location: D:\Games\OxygenNotIncluded\OxygenNotIncluded_Data\Managed\Assembly-CSharp.dll

using STRINGS;
using UnityEngine;

public class LogicGateBetterFilterConfig : LogicGateBaseConfig
{
  public const string ID = "LogicGateBetterFilter";

  protected override LogicGateBase.Op GetLogicOp()
  {
    return LogicGateBase.Op.CustomSingle;
  }

  protected override CellOffset[] InputPortOffsets
  {
    get
    {
      return new CellOffset[1]{ CellOffset.none };
    }
  }

  protected override CellOffset[] OutputPortOffsets
  {
    get
    {
      return new CellOffset[1]{ new CellOffset(1, 0) };
    }
  }

  protected override CellOffset[] ControlPortOffsets
  {
    get
    {
      return (CellOffset[]) null;
    }
  }

  protected override LogicGate.LogicGateDescriptions GetDescriptions()
  {
    return new LogicGate.LogicGateDescriptions()
    {
      outputOne = new LogicGate.LogicGateDescriptions.Description()
      {
        name = (string) BUILDINGS.PREFABS.LOGICGATEFILTER.OUTPUT_NAME,
        active = (string) BUILDINGS.PREFABS.LOGICGATEFILTER.OUTPUT_ACTIVE,
        inactive = (string) BUILDINGS.PREFABS.LOGICGATEFILTER.OUTPUT_INACTIVE
      }
    };
  }

  public override BuildingDef CreateBuildingDef()
  {
    return this.CreateBuildingDef("LogicGateBetterFilter", "logic_bfilter_kanim", 2, 1);
  }

  public override void DoPostConfigureComplete(GameObject go)
  {
    LogicGateBetterFilter LogicGateBetterFilter = go.AddComponent<LogicGateBetterFilter>();
    LogicGateBetterFilter.op = this.GetLogicOp();
    LogicGateBetterFilter.inputPortOffsets = this.InputPortOffsets;
    LogicGateBetterFilter.outputPortOffsets = this.OutputPortOffsets;
    LogicGateBetterFilter.controlPortOffsets = this.ControlPortOffsets;
    go.GetComponent<KPrefabID>().prefabInitFn += (KPrefabID.PrefabFn) (game_object => game_object.GetComponent<LogicGateBetterFilter>().SetPortDescriptions(this.GetDescriptions()));
    go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits, false);
  }
}
