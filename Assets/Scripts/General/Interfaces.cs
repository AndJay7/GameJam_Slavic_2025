using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IHealth
{
    void TakeDamage(float damage);
    float DealDamage();
}

public interface ISpeed
{
    void Slow(float fraction, float duration);


}