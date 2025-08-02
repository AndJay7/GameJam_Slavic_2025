using System;
using System.Collections;
using System.Collections.Generic;
using Survivor;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySummonAttack : MonoBehaviour
{
    private enum ActionType
    {
        AttackStart,
        AttackNext,
    }
    
    [SerializeField]
    private float2 _cooldown;
    [SerializeField]
    private int2 _attackCount;
    [SerializeField]
    private float2 _attackDelay;
    [SerializeField]
    private float _attackRandomRadius;
    [SerializeField]
    private float _attackPredictionTime;
    [SerializeField]
    private GameObject _instancePrefab;

    private float _actionTime;
    private ActionType _actionType;
    private int _attacksLeft;

    private void FixedUpdate()
    {
        if(Time.time < _actionTime)
            return;

        switch (_actionType)
        {
            case ActionType.AttackStart:
                _attacksLeft = Random.Range(_attackCount.x, _attackCount.y);

                if (_attacksLeft > 0)
                {
                    SpawnAttackInstance();
                    _attacksLeft--;
                }

                if (_attacksLeft > 0)
                {
                    _actionTime = Time.time + Random.Range(_attackDelay.x, _attackDelay.y);
                    _actionType = ActionType.AttackNext;
                }
                else
                {
                    _actionTime = Time.time + Random.Range(_cooldown.x, _cooldown.y);
                    _actionType = ActionType.AttackStart;
                }
                break;
            case ActionType.AttackNext:
                if (_attacksLeft > 0)
                {
                    SpawnAttackInstance();
                    _attacksLeft--;
                }

                if (_attacksLeft > 0)
                {
                    _actionTime = Time.time + Random.Range(_attackDelay.x, _attackDelay.y);
                    _actionType = ActionType.AttackNext;
                }
                else
                {
                    _actionTime = Time.time + Random.Range(_cooldown.x, _cooldown.y);
                    _actionType = ActionType.AttackStart;
                }
                break;
        }
    }

    private void SpawnAttackInstance()
    {
        var position = (Vector2)PlayerMovement.Instance.transform.position;
        position += PlayerMovement.Instance.gameObject.GetComponent<Rigidbody2D>().velocity * _attackPredictionTime;
        position += Random.insideUnitCircle * _attackRandomRadius;
        
        Instantiate(_instancePrefab, new Vector3(position.x, position.y, position.y * 0.01f), Quaternion.identity);
    }
}
