using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummonAttackInstance : MonoBehaviour
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

        var spriteIndex = Mathf.FloorToInt(_frame);

        if (spriteIndex >= _sprites.Length)
        {
            Destroy(gameObject);
            return;
        }
        
        var sprite = _sprites[spriteIndex];
        
        _spriteRenderer.sprite = sprite;
    }
}
