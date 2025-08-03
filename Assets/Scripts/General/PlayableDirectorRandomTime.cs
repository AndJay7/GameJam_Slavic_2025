using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

public class PlayableDirectorRandomTime : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector _director;

    private void Start()
    {
        _director.time = Random.Range(0f, (float)_director.duration);
    }
}
