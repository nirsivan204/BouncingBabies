using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkyManager : MonoBehaviour
{
    [SerializeField] CloudPool _cloudPool;
    [SerializeField] int _defaultCapacity;
    [SerializeField] int _maxSize;
    [SerializeField] GameObject[] skies;
    WaitForSeconds cachedWait = new WaitForSeconds(3);

    private void Awake()
    {
        _cloudPool.InitPool(_defaultCapacity, _maxSize);
        Instantiate(skies[Random.Range(0, skies.Length)]);
        StartCoroutine(SpawnCoroutine());
    }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                if (_cloudPool.Pool.CountActive < _maxSize)
                {
                    _cloudPool.Pool?.Get();
                }
                yield return cachedWait;
            }
        }
    
}

