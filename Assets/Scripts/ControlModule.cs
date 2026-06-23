using System;
using Event_Channels;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlModule : MechModule
{
    private InputSystem_Actions _inputSystemActions;
    private Vector2 _inputVector;

    [SerializeField] private ControlModuleEventChannel controlModuleEventChannel;
    private bool _updateCockpitPanelTrigger;

    [Header("Mech Attributes")] [SerializeField]
    private float controlResponsiveness = 10;


    private Action _accelerationLeverAction;
    private float _accelerationActualValue;
    private float _accelerationLastValue;
    private readonly float _accelerationMaxValue = 1;
    private float _accelerationNextValue;
    private readonly float _accelerationMinValue = 0;

    private Action _rotationAction;
    private float _rotationActualValue;
    private float _rotationLastValue;
    private readonly float _rotationMaxValue = 1;
    private float _rotationNextValue;
    private float _rotationMinValue = 0;


    private void Awake()
    {
        _inputSystemActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _inputSystemActions.Enable();
        _inputSystemActions.Player.Move.performed += MovementInputAction;
        _inputSystemActions.Player.Move.canceled += MovementInputAction;
    }

    private void OnDisable()
    {
        _inputSystemActions.Player.Move.performed -= MovementInputAction;
        _inputSystemActions.Player.Move.canceled -= MovementInputAction;
        _inputSystemActions.Disable();
    }

    private void Update()
    {
        _accelerationLeverAction?.Invoke();
        _rotationAction?.Invoke();
        PanelDataFeed();
    }

    private void MovementInputAction(InputAction.CallbackContext ctx)
    {
        _inputVector = ctx.ReadValue<Vector2>();

        if (_inputVector.x > 0)
        {
            _rotationAction = IncreaseRotationLeverHandle;
        }
        else if (_inputVector.x < 0)
        {
            _rotationAction = DecreaseRotationLeverHandle;
        }
        else
        {
            _rotationAction = Idle;
        }

        if (_inputVector.y > 0)
        {
            _accelerationLeverAction = IncreaseAccelerationLeverHandle;
        }
        else if (_inputVector.y < 0)
        {
            _accelerationLeverAction = DecreaseAccelerationLeverHandle;
        }
        else
        {
            _accelerationLeverAction = Idle;
        }
    }

    private void IncreaseRotationLeverHandle()
    {
        _rotationNextValue += controlResponsiveness * Time.deltaTime;
        _rotationActualValue = _rotationNextValue <= _rotationMaxValue
                ? _rotationNextValue
                : _rotationMaxValue;
        _updateCockpitPanelTrigger = true;
    }

    private void DecreaseRotationLeverHandle()
    {
        _rotationNextValue -= controlResponsiveness * Time.deltaTime;
        _rotationActualValue = _rotationNextValue >= _rotationMinValue
            ? _rotationNextValue
            : _rotationMinValue;
        _updateCockpitPanelTrigger = true;
    }

    private void IncreaseAccelerationLeverHandle()
    {
        _accelerationActualValue += controlResponsiveness * Time.deltaTime;
        _accelerationActualValue = _accelerationNextValue <= _accelerationMaxValue
            ? _accelerationNextValue
            : _accelerationMaxValue;
        _updateCockpitPanelTrigger = true;
    }

    private void DecreaseAccelerationLeverHandle()
    {
        _accelerationActualValue -= controlResponsiveness * Time.deltaTime;
        _accelerationActualValue = _accelerationNextValue >= _accelerationMinValue
            ? _accelerationNextValue
            : _accelerationMinValue;
        _updateCockpitPanelTrigger = true;
    }

    private void Idle()
    {
    }


    private void PanelDataFeed()
    {
        if (_updateCockpitPanelTrigger)
        {
            controlModuleEventChannel.RaiseEvent(ControlModuleEventActions.AccelerationUI, _accelerationActualValue);
            controlModuleEventChannel.RaiseEvent(ControlModuleEventActions.RotationUI, _rotationActualValue);
        }
        else
        {
            _updateCockpitPanelTrigger = false;
        }
    }
}