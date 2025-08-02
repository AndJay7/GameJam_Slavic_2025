using System;
using System.Collections;
using System.Collections.Generic;
using Survivor;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossTimer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;
    [SerializeField]
    private float _countdownMinutes;
    [SerializeField]
    private GameObject _bossPrefab;

    private float _endTime;

    private void Start()
    {
        _endTime = Time.time + _countdownMinutes * 60f;
    }

    private void Update()
    {
        var secondsLeft = Mathf.CeilToInt(_endTime - Time.time);
        var minutesLeft = secondsLeft / 60;
        
        _text.text = $"{Mathf.FloorToInt(minutesLeft):0}:{(secondsLeft % 60):00}";

        if (secondsLeft <= 0)
        {
            var angle = Random.Range(0f, Mathf.PI * 2f);
            
            var sin = Mathf.Sin(angle);
            var cos = Mathf.Cos(angle);
            
            gameObject.SetActive(false);
            Instantiate(_bossPrefab, PlayerMovement.Instance.transform.position + new Vector3(sin, cos, 0f) * 20f, Quaternion.identity);
        }
    }
}
