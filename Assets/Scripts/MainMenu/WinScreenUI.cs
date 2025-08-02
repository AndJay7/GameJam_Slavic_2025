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


    private void Start()
    {
        _checkDeviceCall = InputSystem.onAnyButtonPress.Call(OnButtonPress);
    }

    private void OnButtonPress(InputControl obj)
    {
        _checkDeviceCall.Dispose();
        _checkDeviceCall = null;
        SceneManager.LoadScene(_gameSceneName, LoadSceneMode.Single);
    }
}
