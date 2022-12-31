using UnityEngine.Pool;
using UnityEngine;
using System;

public abstract class AbstractCollector : MonoBehaviour
{

    protected IObjectPool<GameObject> _pool;
    protected Action _babyCollectedEvent;
    public Action BabyCollectedEvent { get => _babyCollectedEvent; set { _babyCollectedEvent = value; } }

    public void Start()
    {
        _pool = FindObjectOfType<ObjectPool>()._babyPool;
    }

    public void Init(IObjectPool<GameObject> pool)
    {
        _pool = pool;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Baby")
        {
            _pool.Release(collision.gameObject);
            _babyCollectedEvent?.Invoke();
            BabyCollectedAffect();
        }
    }

    protected abstract void BabyCollectedAffect();

}