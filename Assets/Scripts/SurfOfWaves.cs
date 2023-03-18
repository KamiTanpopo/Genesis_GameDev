using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SurfOfWaves : MonoBehaviour
{
    [SerializeField] private float _surfMovement;
    [SerializeField] private bool _waveAbove;

    private float _previousWavePosition;
    private Transform _waveTransform;

    void Start()
    {
        _waveTransform = GetComponent<Transform>();
        _previousWavePosition = _waveTransform.position.y;

        InvokeRepeating("surfOfWave", _surfMovement, _surfMovement);
    }
    void surfOfWave()
    {
        if (_waveAbove)
        {
            Vector2 position = _waveTransform.position;
            position.y = _previousWavePosition - _surfMovement;
            _waveTransform.position = position;

            _waveAbove = false;
        }
        else
        {
            Vector2 position = _waveTransform.position;
            position.y = _previousWavePosition;
            _waveTransform.position = position;

            _waveAbove = true;
        }
    }
}
