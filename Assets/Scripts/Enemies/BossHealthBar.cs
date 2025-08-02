using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    public static BossHealthBar Instance { get; set; }
    
    [SerializeField]
    private Transform _bar;

    private void Awake()
    {
        Instance = this;
        
        gameObject.SetActive(false);
    }

    public void SetScale(float scale)
    {
        gameObject.SetActive(true);
        
        _bar.localScale = new Vector3(scale, 1f, 1f);
    }
}
