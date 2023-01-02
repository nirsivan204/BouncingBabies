using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkyManager : AbstractSpawner<Cloud>
{

    [SerializeField] GameObject[] skies;
    WaitForSeconds cachedWait = new WaitForSeconds(3);

    private void OnEnable()
    {
        Init();
        Instantiate(skies[Random.Range(0, skies.Length)]);
        StartSpawn();
    }

    private void OnDisable()
    {
        StopSpawn();
    }

    protected override IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            if (_poolManager.Pool.CountActive < _maxSize)
            {
                _poolManager.Pool?.Get();
            }
            yield return cachedWait;
        }
    }
    
}

