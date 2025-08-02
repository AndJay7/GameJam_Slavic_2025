using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossTimer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;
    [SerializeField]
    private float _countdownMinutes;
    [SerializeField]
    private GameObject _bossPrefab;
}
