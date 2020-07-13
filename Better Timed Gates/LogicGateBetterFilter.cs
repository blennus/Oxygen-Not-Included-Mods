// Decompiled with JetBrains decompiler
// Type: LogicGateBetterFilter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DFDEE8B9-A425-463E-AF51-00480BB21E0A
// Assembly location: D:\Games\OxygenNotIncluded\OxygenNotIncluded_Data\Managed\Assembly-CSharp.dll

using KSerialization;
using STRINGS;
using UnityEngine;

[SerializationConfig(MemberSerialization.OptIn)]
public class LogicGateBetterFilter : LogicGate, ISingleSliderControl, ISliderControl, ISim200ms
{
  private static readonly EventSystem.IntraObjectHandler<LogicGateBetterFilter> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicGateBetterFilter>((System.Action<LogicGateBetterFilter, object>) ((component, data) => component.OnCopySettings(data)));
  [Serialize]
  private float delayAmount = 15f;
  [Serialize]
  private float delayRemaining = 0f;
  [Serialize]
  private bool wasOff;
  private MeterController meter;
  [MyCmpAdd]
  private CopyBuildingSettings copyBuildingSettings;

  public float DelayAmount
  {
    get
    {
      return this.delayAmount;
    }
    set
    {
      this.delayAmount = value;
      if (this.delayRemaining <= delayAmount)
        return;
      this.delayRemaining = delayAmount;
    }
  }

  public string SliderTitleKey
  {
    get
    {
      return "STRINGS.UI.UISIDESCREENS.LOGIC_FILTER_SIDE_SCREEN.TITLE";
    }
  }

  public string SliderUnits
  {
    get
    {
      return (string) UI.UNITSUFFIXES.SECOND;
    }
  }

  public int SliderDecimalPlaces(int index)
  {
    return 1;
  }

  public float GetSliderMin(int index)
  {
    return 0.1f;
  }

  public float GetSliderMax(int index)
  {
    return 600f;
  }

  public float GetSliderValue(int index)
  {
    return this.DelayAmount;
  }

  public void SetSliderValue(float value, int index)
  {
    this.DelayAmount = value;
  }

  public string GetSliderTooltipKey(int index)
  {
    return "STRINGS.UI.UISIDESCREENS.LOGIC_FILTER_SIDE_SCREEN.TOOLTIP";
  }

  string ISliderControl.GetSliderTooltip()
  {
    return string.Format((string) Strings.Get("STRINGS.UI.UISIDESCREENS.LOGIC_FILTER_SIDE_SCREEN.TOOLTIP"), (object) this.DelayAmount);
  }

  protected override void OnPrefabInit()
  {
    base.OnPrefabInit();
    this.Subscribe<LogicGateBetterFilter>(-905833192, LogicGateBetterFilter.OnCopySettingsDelegate);
  }

  private void OnCopySettings(object data)
  {
    LogicGateBetterFilter component = ((GameObject) data).GetComponent<LogicGateBetterFilter>();
    if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
      return;
    this.DelayAmount = component.DelayAmount;
  }

  protected override void OnSpawn()
  {
    base.OnSpawn();
    this.meter = new MeterController((KAnimControllerBase) this.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.UserSpecified, Grid.SceneLayer.LogicGatesFront, Vector3.zero, (string[]) null);
    this.meter.SetPositionPercent(0.0f);
  }

  private void Update()
  {
    this.meter.SetPositionPercent(!this.wasOff ? (this.delayRemaining <= 0 ? 1f : (float) (this.delayAmount - this.delayRemaining) / (float) this.delayAmount) : 0.0f);
  }

  public void Sim200ms(float dt)
    {
    if (this.wasOff || this.delayRemaining <= 0)
      return;
    this.delayRemaining -= dt;
    if (this.delayRemaining > 0)
      return;
    this.OnDelay();
  }

  protected override int GetCustomValue(int val1, int val2)
  {
    if (val1 == 0)
    {
      this.wasOff = true;
      this.delayRemaining = 0;
      this.meter.SetPositionPercent(1f);
    }
    else if (this.delayRemaining <= 0)
    {
      if (this.wasOff)
        this.delayRemaining = this.delayAmount;
      this.wasOff = false;
    }
    return val1 != 0 && this.delayRemaining <= 0 ? 1 : 0;
  }

  private void OnDelay()
  {
    if (this.cleaningUp)
      return;
    this.delayRemaining = 0;
    this.meter.SetPositionPercent(0.0f);
    if (this.outputValueOne == 1 || !(Game.Instance.logicCircuitSystem.GetNetworkForCell(this.OutputCellOne) is LogicCircuitNetwork))
      return;
    this.outputValueOne = 1;
    this.RefreshAnimation();
  }
}
