using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Utilities;

public class InputManager : Singleton<InputManager>
{
    private LeftPlayer _leftPlayerActions;
    private RightPlayer _rightPlayerActions;

    private IDisposable _checkDeviceCall;

    private InputUser _leftUser;
    private InputUser _rightUser;

    public event Action OnLeftPlayerConnected;
    public event Action OnRightPlayerConnected;
    public LeftPlayer LeftPlayerActions => _leftPlayerActions;
    public RightPlayer RightPlayerActions => _rightPlayerActions;

    protected override void Awake()
    {
        base.Awake();
        _leftUser = InputUser.CreateUserWithoutPairedDevices();
        _rightUser = InputUser.CreateUserWithoutPairedDevices();

        _leftPlayerActions = new LeftPlayer();
        _leftUser.AssociateActionsWithUser(_leftPlayerActions);

        _rightPlayerActions = new RightPlayer();
        _rightUser.AssociateActionsWithUser(_rightPlayerActions);

        _checkDeviceCall = InputSystem.onAnyButtonPress.Call(OnButtonPress);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _checkDeviceCall?.Dispose();
    }

    private void OnButtonPress(InputControl obj)
    {
        var device = obj.device;

        if (device is Mouse)
            return;

        if (_leftUser.pairedDevices.Count == 0)
        {
            InputUser.PerformPairingWithDevice(device, _leftUser);
            _leftPlayerActions.Enable();
            OnLeftPlayerConnected?.Invoke();
        }
        else if (_rightUser.pairedDevices.Count == 0)
        {
            if (_leftUser.pairedDevices.Any(d => d.deviceId == device.deviceId))
                return;

            InputUser.PerformPairingWithDevice(device, _rightUser);
            _rightPlayerActions.Enable();
            OnRightPlayerConnected?.Invoke();
            _checkDeviceCall.Dispose();
            _checkDeviceCall = null;
        }
    }
}
