using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummonAttack : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private float _frameRate;
    [SerializeField]
    private Sprite[] _sprites;

    private float _frame;
    
    private void FixedUpdate()
    {
        _frame += Time.fixedDeltaTime * _frameRate;

        var sprite = _sprites[Mathf.FloorToInt(_frame) % _sprites.Length];
        
        _spriteRenderer.sprite = sprite;
    }
}
