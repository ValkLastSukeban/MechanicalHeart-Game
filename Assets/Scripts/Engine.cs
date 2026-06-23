using System;
using UnityEngine;

public enum EngineState
{
    On,
    Off,
    Idle
}
public class Engine : MechModule
{
    private ControlModule _controlModule;
    private float _enginePower;
    private Action _actualEngineAction;
    private EngineState _actualEngineState;
    private void Start()
    {
        _actualEngineState = EngineState.Off;
        _actualEngineAction = EmptyEngineAction;
    }
    
    private void Update()
    {
        _actualEngineAction();
    }

    private void StartEngine()
    {
        _actualEngineState = EngineState.On;
        _actualEngineAction = IdleEngineAction;
    }

    private void StopEngine()
    {
        _actualEngineState = EngineState.Off;
    }

    private void EmptyEngineAction()
    {
        
    }

    private void IdleEngineAction()
    {
        
    }

    private void AccelerateEngineAction()
    {
        
    }

    private void ReverseEngineAction()
    {
        
    }
}