using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Floor : MonoBehaviour
{
    IObjectPool<GameObject> pool;

    public void Start()
    {
        pool = FindObjectOfType<ObjectPool>()._babyPool;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        pool.Release(collision.gameObject);
    }
}
