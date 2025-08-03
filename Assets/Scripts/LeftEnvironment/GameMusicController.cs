using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameMusicController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _gameMusic;
    [SerializeField]
    private AnimationCurve _fadeCurve;
    [SerializeField]
    private float _fadeTime = 1;

    private void Awake()
    {
        _gameMusic.volume = 0;
        _gameMusic.Play();
        AwakeAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    private async UniTaskVoid AwakeAsync(CancellationToken cancellationToken)
    {
        var startTime = Time.time;

        while (Time.time - startTime < _fadeTime && !cancellationToken.IsCancellationRequested)
        {
            var volume = (Time.time - startTime) / _fadeTime;
            _gameMusic.volume = _fadeCurve.Evaluate(volume);
            await UniTask.WaitForEndOfFrame(cancellationToken);
        }
        _gameMusic.volume = 1;
    }

    private void OnDestroy()
    {
        _gameMusic.Stop();
    }
}
