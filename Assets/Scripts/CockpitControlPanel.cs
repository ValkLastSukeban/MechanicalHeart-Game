using Event_Channels;
using UnityEngine;
using UnityEngine.UI;

public class CockpitControlPanel : MechModule
{
    [SerializeField] private ControlModuleEventChannel controlModuleEventChannel;
    [SerializeField] private Slider accelerationLeverHandle;
    [SerializeField] private Slider rotationLeverHandle;

    private void OnEnable()
    {
        controlModuleEventChannel.RegisterListener(ControlModuleEventActions.AccelerationUI, ChangeAccelerationLeverHandleValue);
        controlModuleEventChannel.RegisterListener(ControlModuleEventActions.RotationUI, ChangeRotationLeverHandleValue);
    }
    
    private void OnDisable()
    {
        
    }

    private void ChangeAccelerationLeverHandleValue(float amount)
    {
        accelerationLeverHandle.value = accelerationLeverHandle.maxValue / 100 * amount;
    }

    private void ChangeRotationLeverHandleValue(float amount)
    {
        rotationLeverHandle.value = rotationLeverHandle.maxValue / 100 * amount;
    }


}
