using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public enum ActiveWindows
{
    First = 1,
    Second = 2,
    Third = 4,
}

public class Spawner : MonoBehaviour
{
    [Serializable]
    private struct Window
    {
        public SpriteRenderer spriteRenderer;
        public float throwForce;
        public Transform transform;
    }


    [SerializeField] Sprite _openedWindowSprite;
    [SerializeField] Sprite _closedWindowSprite;

    WaitForSeconds[] cachedAwaits = new WaitForSeconds[2]; 

    [SerializeField] Window[] windows;
    protected IObjectPool<GameObject> _pool;
    private int _activeWindows;
    private Coroutine _spawnCoroutine;

    public void Init(int activeWindows, float minBabiesPerSecond, float maxBabiesPerSecond)
    {
        _pool = PoolManager._babyPool;
        _activeWindows = activeWindows;
        for (int i = 0; i < activeWindows; i++)
        {
            windows[i].spriteRenderer.sprite = _openedWindowSprite;
        }
        cachedAwaits[0] = new WaitForSeconds(1/minBabiesPerSecond);
        cachedAwaits[1] = new WaitForSeconds(1/maxBabiesPerSecond);
    }

    private void ThrowBaby(int windowID)
    {
        GameObject baby = _pool?.Get();
        baby.transform.position = windows[windowID].transform.position;
        baby.GetComponent<Rigidbody2D>().AddForce(Vector2.right * windows[windowID].throwForce, ForceMode2D.Impulse);
    }

    public void StartSpawn()
    {
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());

    }

    public void StopSpawn()
    {
        StopCoroutine(_spawnCoroutine);
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            int randomWindow = Random.Range(0, _activeWindows);
            int randomWaitPeriod = Random.Range(0, cachedAwaits.Length);
            yield return cachedAwaits[randomWaitPeriod];
            ThrowBaby(randomWindow);
        }
    }

    int i = 0;
/*    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowBaby(i++%3);
        }
    }*/
}
