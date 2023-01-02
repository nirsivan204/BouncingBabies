using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class Cloud : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    Vector3 _vel;
    ObjectPool<Cloud> _pool;

    public void SetPool(ObjectPool<Cloud> pool)
    {
        _pool = pool;
    }

    public void Init(Sprite sprite, float speed)
    {
        _vel = new Vector3(speed, 0 , 0);
        _spriteRenderer.sprite = sprite;
    }

    public void Update()
    {
        transform.position -= _vel * Time.deltaTime;
    }

    public void OnBecameInvisible()
    {
        _pool.Release(this);
    }
}
