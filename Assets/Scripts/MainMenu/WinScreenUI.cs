using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class WinScreenUI : MonoBehaviour
{
    [SerializeField]
    private string _gameSceneName = "Game";

    private IDisposable _checkDeviceCall;
    private float _startTime;

    private void Start()
    {
        _checkDeviceCall = InputSystem.onAnyButtonPress.Call(OnButtonPress);
        _startTime = Time.time;
    }

    private void OnButtonPress(InputControl obj)
    {
        if (Time.time - _startTime < 1)
            return;

        if (obj.device is Mouse)
            return;

        _checkDeviceCall.Dispose();
        _checkDeviceCall = null;

        SceneManager.LoadScene(_gameSceneName, LoadSceneMode.Single);
    }
}
