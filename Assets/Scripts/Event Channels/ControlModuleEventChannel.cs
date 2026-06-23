namespace Event_Channels
{
    public enum ControlModuleEventActions
    {
        AccelerationUI,
        RotationUI
    }

    public class ControlModuleEventChannel : EventChannelType<ControlModuleEventActions, float>
    {
         
    }
}