using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class BabiesSpawner : AbstractSpawner<BabyScript>
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
    private int _activeWindows;

    public void Init(int activeWindows, float minBabiesPerSecond, float maxBabiesPerSecond)
    {
        base.Init();
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
        BabyScript baby = _poolManager.Pool?.Get();
        baby.transform.position = windows[windowID].transform.position;
        baby.GetComponent<Rigidbody2D>().AddForce(Vector2.right * windows[windowID].throwForce, ForceMode2D.Impulse);
    }


    protected override IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            int randomWindow = Random.Range(0, _activeWindows);
            int randomWaitPeriod = Random.Range(0, cachedAwaits.Length);
            yield return cachedAwaits[randomWaitPeriod];
            ThrowBaby(randomWindow);
        }
    }
}
