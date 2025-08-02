using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _counterText;
    [SerializeField]
    private TextMeshProUGUI _leftPlayerText;
    [SerializeField]
    private TextMeshProUGUI _rightPlayerText;
    [SerializeField]
    private string _gameSceneName;

    private void Start()
    {
        _counterText.text = string.Empty;
        _leftPlayerText.text = "Click anything...";
        _rightPlayerText.text = string.Empty;

        InputManager.Instance.OnLeftPlayerConnected += LeftPlayerConnected;
        InputManager.Instance.OnRightPlayerConnected += RightPlayerConnected;

    }

    private void RightPlayerConnected()
    {
        _rightPlayerText.text = "Connected";
        _rightPlayerText.color = new Color(0, 0.8f, 0, 1f);
        InputManager.Instance.OnLeftPlayerConnected -= RightPlayerConnected;
        StartCounting().Forget();
    }

    private void LeftPlayerConnected()
    {
        _leftPlayerText.text = "Connected";
        _leftPlayerText.color = new Color(0,0.8f,0,1f);
        _rightPlayerText.text = "Click anything...";
        InputManager.Instance.OnLeftPlayerConnected -= LeftPlayerConnected;
    }

    private async UniTaskVoid StartCounting()
    {
        for(int i = 5; i > 0;i--)
        {
            _counterText.text = i.ToString();
            await UniTask.WaitForSeconds(1);
        }
        gameObject.SetActive(false);
        SceneManager.LoadScene(_gameSceneName, LoadSceneMode.Additive);
    }
}
