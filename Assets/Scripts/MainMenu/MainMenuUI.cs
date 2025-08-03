using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioPlayerSelect1;
    [SerializeField]
    private AudioSource _audioPlayerSelect2;
    [SerializeField]
    private AudioSource _music;
    [SerializeField]
    private Image _counterImage;
    [SerializeField]
    private TextMeshProUGUI _leftPlayerText;
    [SerializeField]
    private TextMeshProUGUI _rightPlayerText;
    [SerializeField]
    private Sprite[] _counter;
    [SerializeField]
    private string _gameSceneName;

    private void Start()
    {
        _counterImage.gameObject.SetActive(false);
        _leftPlayerText.text = "Click anything...";
        _rightPlayerText.text = string.Empty;

        InputManager.Instance.OnLeftPlayerConnected += LeftPlayerConnected;
        InputManager.Instance.OnRightPlayerConnected += RightPlayerConnected;

    }

    private void RightPlayerConnected()
    {
        _rightPlayerText.text = "Connected";
        _rightPlayerText.color = new Color(0, 0.8f, 0, 1f);
        InputManager.Instance.OnRightPlayerConnected -= RightPlayerConnected;
        _audioPlayerSelect1.Play();
        StartCounting().Forget();
    }

    private void LeftPlayerConnected()
    {
        _leftPlayerText.text = "Connected";
        _leftPlayerText.color = new Color(0,0.8f,0,1f);
        _rightPlayerText.text = "Click anything...";
        _audioPlayerSelect2.Play();
        InputManager.Instance.OnLeftPlayerConnected -= LeftPlayerConnected;
    }

    private async UniTaskVoid StartCounting()
    {
        _counterImage.gameObject.SetActive(true);
        for (int i = 5; i > 0;i--)
        {
            _counterImage.sprite = _counter[i - 1];
            await UniTask.WaitForSeconds(1);
            _audioPlayerSelect1.Play();
        }
        gameObject.SetActive(false);
        _music.Stop();
        SceneManager.LoadScene(_gameSceneName, LoadSceneMode.Single);
    }
}
