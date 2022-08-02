using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkFPSCount : MonoBehaviour
{

    private float[] _deltas = new float[100];
    private int _index;
    private float _lowest;
    private float _highest;
    private float _average;
    private List<float> _totalDeltas = new List<float>();
    [SerializeField] private bool _measure;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if(!_measure)
            return;

        _deltas[_index] = Time.deltaTime;
        if (_index < _deltas.Length - 1)
        {
            _index++;
        }
        else
        {
            _index = 0;
            float sum = 0;
            for (int i = 0; i < _deltas.Length; i++)
            {
                sum += _deltas[i];
            }
            float average = 1/(sum / _deltas.Length);
            _totalDeltas.Add(average);
            print(average);
        }
    }

    private void OnDisable()
    {
        if (_totalDeltas.Count == 0)
            return;
        
        _lowest = _totalDeltas.Min();
        _highest = _totalDeltas.Max();
        _average = _totalDeltas.Average();
        Debug.LogWarning($"Lowest FPS was {_lowest}");
        Debug.LogWarning($"Highest FPS was {_highest}");
        Debug.LogWarning($"Average FPS was {_average}");
    }
}
