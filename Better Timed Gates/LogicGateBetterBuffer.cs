using KSerialization;
using STRINGS;
using UnityEngine;

namespace Better_Timed_Gates
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class LogicGateBetterBuffer : LogicGate, ISingleSliderControl, ISliderControl, ISim200ms
    {
      private static readonly EventSystem.IntraObjectHandler<LogicGateBetterBuffer> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicGateBetterBuffer>((System.Action<LogicGateBetterBuffer, object>) ((component, data) => component.OnCopySettings(data)));
      [Serialize]
      private float delayAmount = 15f;
      [Serialize]
      private float delayRemaining = 0f;
      [Serialize]
      private bool wasOn;
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
          return "STRINGS.UI.UISIDESCREENS.LOGIC_BUFFER_SIDE_SCREEN.TITLE";
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
        return 15f;
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
        return "STRINGS.UI.UISIDESCREENS.LOGIC_BUFFER_SIDE_SCREEN.TOOLTIP";
      }

      string ISliderControl.GetSliderTooltip()
      {
        return string.Format((string) Strings.Get("STRINGS.UI.UISIDESCREENS.LOGIC_BUFFER_SIDE_SCREEN.TOOLTIP"), (object) this.DelayAmount);
      }

      protected override void OnPrefabInit()
      {
        base.OnPrefabInit();
        this.Subscribe<LogicGateBetterBuffer>(-905833192, LogicGateBetterBuffer.OnCopySettingsDelegate);
      }

      private void OnCopySettings(object data)
      {
        LogicGateBetterBuffer component = ((GameObject) data).GetComponent<LogicGateBetterBuffer>();
        if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
          return;
        this.DelayAmount = component.DelayAmount;
      }

      protected override void OnSpawn()
      {
        base.OnSpawn();
        this.meter = new MeterController((KAnimControllerBase) this.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.UserSpecified, Grid.SceneLayer.LogicGatesFront, Vector3.zero, (string[]) null);
        this.meter.SetPositionPercent(1f);
      }

      private void Update()
      {
        this.meter.SetPositionPercent(!this.wasOn ? (this.delayRemaining <= 0 ? 1f : (this.delayAmount - this.delayRemaining) / this.delayAmount) : 0.0f);
      }

      public void Sim200ms(float dt)
      {
        if (this.wasOn || this.delayRemaining <= 0)
          return;
        this.delayRemaining -= dt;
        if (this.delayRemaining > 0)
          return;
        this.OnDelay();
      }

      protected override int GetCustomValue(int val1, int val2)
      {
        if (val1 != 0)
        {
          this.wasOn = true;
          this.delayRemaining = 0f;
          this.meter.SetPositionPercent(0.0f);
        }
        else if (this.delayRemaining <= 0)
        {
          if (this.wasOn)
            this.delayRemaining = this.DelayAmount;
          this.wasOn = false;
        }
        return val1 == 0 && this.delayRemaining <= 0 ? 0 : 1;
      }

      private void OnDelay()
      {
        if (this.cleaningUp)
          return;
        this.delayRemaining = 0;
        this.meter.SetPositionPercent(1f);
        if (this.outputValueOne == 0 || !(Game.Instance.logicCircuitSystem.GetNetworkForCell(this.OutputCellOne) is LogicCircuitNetwork))
          return;
        this.outputValueOne = 0;
        this.RefreshAnimation();
      }
    }
}
